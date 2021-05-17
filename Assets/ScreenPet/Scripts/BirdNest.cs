using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenPet
{
  public class BirdNest : MonoBehaviour
  {
    public GameObject birdPrefab;

    public Transform target;
    public Transform[] eatPoints;

    public List<BirdAI> birds;

    public int maxBirdCount = 9;

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.B))
        SpawnBird();
      if (Input.GetKeyDown(KeyCode.R))
        RemoveBird();
    }

    public void SpawnBird()
    {
      if (birds.Count < maxBirdCount)
      {
        BirdAI bird = GameObject.Instantiate(birdPrefab, transform).GetComponent<BirdAI>();

        bird.target = target;
        bird.eatPoints = eatPoints;

        birds.Add(bird);
      }
    }

    public void RemoveBird()
    {
      Destroy(birds[0].gameObject);
      birds.RemoveAt(0);
    }
  }

}