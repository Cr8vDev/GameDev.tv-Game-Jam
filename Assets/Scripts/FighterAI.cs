using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    Idle,
    Seek,
    Pursue
}

public class FighterAI : MonoBehaviour
{
    [SerializeField]
    private Waypoint waypointParent;

    private AIState aiState = AIState.Idle;

    private Transform target;

    [SerializeField]
    private float aiSpeed;

    private Vector3 angleLerp;

    [SerializeField]
    private Transform fighterChild;



    void Update()
    {
        transform.position += transform.forward * aiSpeed * Time.deltaTime;

        if (aiState == AIState.Idle)
        {
            var randomWaypointIndex = Random.Range(0, waypointParent.waypointList.Count - 1);

            target = waypointParent.waypointList[randomWaypointIndex];

            waypointParent.waypointList.RemoveAt(randomWaypointIndex);

            aiState = AIState.Seek;
        }
        else if (aiState == AIState.Seek)
        {
            if(Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up) < 7f && Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up) > -7f)
            {
                var lerpZRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, 0f, Time.deltaTime);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpZRotation);
            }
            else if(Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up) > 5f)
            {
                var lerpZRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, -60f, Time.deltaTime * 8f);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpZRotation);
            }
            else if (Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up) < 5f)
            {
                var lerpZRotation = Mathf.LerpAngle(fighterChild.localEulerAngles.z, 60f, Time.deltaTime * 8f);

                fighterChild.localEulerAngles = new Vector3(0f, fighterChild.localEulerAngles.y, lerpZRotation);
            }

            if (Vector3.Distance(transform.position, target.position) > 100f)
            {
                Quaternion lerpRotate = Quaternion.LookRotation(target.position - transform.position);

                transform.rotation = Quaternion.Lerp(transform.rotation, lerpRotate, Time.deltaTime / 2f);
            }
            else
            {
                waypointParent.waypointList.Add(target);

                aiState = AIState.Idle;
            }
        }
    }
}
