using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Hearts hearts;
    private bool amIAlive;
    public ParticleSystem shipExplosion;
    void Start()
    {
        hearts = transform.Find("Heart Bar").GetComponent<Hearts>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left") || other.CompareTag("right")) //I know I know but im too lazy to change rocketlauncher left and right system
        {
            RIP(hearts.amIDead());
        }
        
        
    }


    private void RIP(bool amIAlive)
    {
        if (amIAlive == false)
        {
            ParticleSystem temp = Instantiate(shipExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(temp.gameObject, temp.main.duration);
        }
    }
}
