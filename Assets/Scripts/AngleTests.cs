using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTests : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    void Update()
    {
        var angle = Vector3.SignedAngle(transform.forward, target.position - transform.position, Vector3.up);

        if(angle < 0)
        {
            angle += 360f;
        }

        Debug.Log(angle);
    }
}
