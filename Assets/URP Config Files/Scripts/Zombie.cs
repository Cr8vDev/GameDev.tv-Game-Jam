using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 targetPosition;

    [SerializeField]
    float inputDelay;
    float pathDelay;

    NavMeshHit hit;
    NavMeshPath path;

    NavMeshAgent agent;
    NavMeshObstacle obstacle;

    private void Start()
    {
        path = new NavMeshPath();

        pathDelay = inputDelay;

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > pathDelay)
        {
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);

            pathDelay = Time.realtimeSinceStartup + inputDelay;
        }

        if (agent.isActiveAndEnabled)
        {
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                targetPosition = path.corners[path.corners.Length - 1];
            }
            else
            {
                targetPosition = target.position;
            }

            if (Vector3.Distance(transform.position, targetPosition) < 2.0f)
            {
                agent.enabled = false;
                obstacle.enabled = true;
            }
            else
            {
                if (agent.isActiveAndEnabled)
                {
                    agent.destination = targetPosition;
                }
            }
        }
    }
}
