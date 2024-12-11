using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMap : MonoBehaviour
{
    public GameObject otherRoad;
    private Vector3 distance;
    private float distanceX;

    private void Start()
    {
        distance = transform.parent.position - otherRoad.transform.position;
        
        // distance.x'ın mutlak değerini alıyoruz, böylece her zaman pozitif olur
        distanceX = Mathf.Abs(distance.x);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Yolun hareketini gerçek boyutla ayarlıyoruz
            transform.parent.position += new Vector3(2 * -distanceX, 0, 0);
        }
    }
}