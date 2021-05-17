using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using TW;

namespace ScreenPet
{
  public class BirdAI : MonoBehaviour
  {
    public Transform target;

    public float speed = 10, updateFrequency = .1f;

    Seeker seeker;
    AIPath path;
    AIDestinationSetter destination;

    public SpriteRenderer sprite;
    public Animator animator;

    public float timeUntilHungry = 5;
    public float eatRate = 2f;

    public Transform[] eatPoints;

    public bool isEating;
    float eatTimer = 0;
    float hungerTimer = 0;

    void Start()
    {
      seeker = GetComponent<Seeker>();
      path = GetComponent<AIPath>();
      destination = GetComponent<AIDestinationSetter>();

      destination.target = target;
      path.maxSpeed = speed;
      path.repathRate = updateFrequency;
    }

    void Update()
    {
      hungerTimer += Time.deltaTime;
      if (hungerTimer > timeUntilHungry && !isEating) // If the bird's hungry and isn't eating...
        GoEat(); //... find an eat point to eat at

      if (isEating)
        eatTimer += Time.deltaTime;

      if (path.reachedEndOfPath)
      {
        animator.SetBool("Perched", true);
        if (isEating && eatTimer > eatRate)
        {
          if (Random.Range(0, 10) == 0)
          {
            GoEat(); // Find a new eat point to eat at
            return;
          }
          if (Random.Range(0, 4) == 0)
            sprite.flipX = !sprite.flipX; // Eat on the same spot, but face the other direction

          eatTimer = 0;
          animator.SetTrigger("Eat"); // Flag the animation to play the eating animation
        }
      }
      else
      {
        // Flip sprite based on motion
        sprite.flipX = path.desiredVelocity.x >= 0.01f ? true : false;
        animator.SetBool("Perched", false);
      }

    }

    // Get an eatpoint and set the eating status to true
    void GoEat()
    {
      isEating = true;
      destination.target = eatPoints[Random.Range(0, eatPoints.Length - 1)];
    }

    // Resets the AI target to the cursor, rather than an eatpoint, and defaults everything else
    void ResetTarget()
    {
      hungerTimer = 0;
      isEating = false;
      animator.ResetTrigger("Eat");
      destination.target = target;
    }

    void OnMouseDown()
    {
      if (Input.GetMouseButtonDown(0))
        ResetTarget();
    }
  } 
}

public static class Extensions
{
  public static Vector3 Direction(this Vector3 _pointA, Vector3 _pointB)
  {
    return (_pointB - _pointA).normalized;
  }
}