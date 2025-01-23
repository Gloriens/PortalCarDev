using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCoordinates : MonoBehaviour
{
    public GameObject player;
    

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x-4,transform.position.y,transform.position.z); 
        }
       
    }
}
