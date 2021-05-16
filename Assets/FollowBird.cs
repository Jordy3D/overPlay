using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
public class FollowBird : MonoBehaviour
{
  public Transform target;

  public float followSpeed = 100;
  public float waypointDistance = 3;

  Path path;
  int currentWaypoint;
  bool reachedTarget;

  Seeker seeker;
  Rigidbody2D biRB;

  void Start()
  {
    seeker = GetComponent<Seeker>();
    biRB = GetComponent<Rigidbody2D>();

    InvokeRepeating(nameof(UpdatePath), 0, .5f);
  }

  void UpdatePath()
  {
    if (seeker.IsDone())
        seeker.StartPath(biRB.position, target.position, OnReachedTarget);
  }

  void OnReachedTarget(Path p)
  {
    if (!p.error)
    {
      path = p;
      currentWaypoint = 0;
    }
  }

  void FixedUpdate()
  {
    if (path == null) return;

    var vectorPath = path.vectorPath;

    reachedTarget = currentWaypoint >= vectorPath.Count;
    if (reachedTarget) return;


    Vector3 dir = transform.position.Direction(vectorPath[currentWaypoint]);
    //Vector2 dir = ((Vector2)vectorPath[currentWaypoint] - biRB.position).normalized;
    Vector2 force = dir * followSpeed;// * Time.deltaTime;

    biRB.AddForce(force);

    float distance = Vector2.Distance(biRB.position, vectorPath[currentWaypoint]);
    if (distance < waypointDistance)
      currentWaypoint++;
  }
}
