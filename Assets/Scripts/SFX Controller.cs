using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXController : MonoBehaviour
{
    public AudioClip[] carPassSound = new AudioClip[5];
    public AudioClip[] hornSound = new AudioClip[4];
    public Camera mainCamera;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = mainCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.name.Contains("NPC Car 1"))
        {
            Debug.Log("Kamyon geçti");
        }
        int chance = Random.Range(0, 100);
        if (other.CompareTag("enemy"))
        {
            if (chance < 40)
            {
                chance = Random.Range(0, 100);
                if (chance < 70)
                {
                    audioSource.PlayOneShot(carPassSound[Random.Range(0, carPassSound.Length)]);
                }
                else
                {
                    if (other.gameObject.name.Contains("NPC Car 1"))
                    {
                        audioSource.PlayOneShot(hornSound[3]);
                    }
                    else
                    {
                        audioSource.PlayOneShot(carPassSound[Random.Range(0, hornSound.Length-1)]);
                    }
                        
                }
            }
        }

        
    }

    private int randomInteger(int min, int max)
    {
        int random = Random.Range(min, max);
        return random;
    }
}
