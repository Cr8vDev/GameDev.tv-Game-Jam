using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [HideInInspector]
    public List<Transform> waypointList = new List<Transform>();



    void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            waypointList.Add(transform.GetChild(i));
        }
    }
}
