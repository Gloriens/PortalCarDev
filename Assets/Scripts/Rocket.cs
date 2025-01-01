using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Rocket : MonoBehaviour
{
    public ParticleSystem explosion;
    private GameObject player;  
    private Rigidbody rb;
    public bool portalMode;
    private float forceMultiplier = 5f;
    private int targetLane;
    private Vector3 target;// Fırlatma kuvveti çarpanı


    public GameObject LinesObject;
    private GameObject[] lines = new GameObject[4];

    private GameObject orangePortal;
    private GameObject bluePortal;
    private Transform bluePortalTransform;
    
    public float ForceMultiplier
    {
        get { return forceMultiplier; }  // Getter: forceMultiplier'ı döndürüyor
        set { forceMultiplier = value; } // Setter: forceMultiplier'a değer atıyor
    }

    private void Start()
    {
        lines[0] = LinesObject.transform.GetChild(0).gameObject; // line1
        lines[1] = LinesObject.transform.GetChild(1).gameObject; // line2
        lines[2] = LinesObject.transform.GetChild(2).gameObject; // line3
        lines[3] = LinesObject.transform.GetChild(3).gameObject; // line4
        
        portalMode = false;
        if (orangePortal == null && bluePortal == null)
        {
            orangePortal = GameObject.FindWithTag("orangePortal");
            bluePortal = GameObject.FindWithTag("bluePortal");
        } 
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); // Oyuncuyu tag'ine göre buluyoruz
        }
        bluePortalTransform = bluePortal.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        
        targetLane = getRandomInt(0, 4);
       
        
    }

    private void Update()
    {
        if (targetLane == 0)
        {
            target = lines[0].transform.position;
            
        }else if (targetLane == 1)
        {
            target = lines[1].transform.position;
        }else if (targetLane == 2)
        {
            target = lines[2].transform.position;
        }else if (targetLane == 3)
        {
            target = lines[3].transform.position;
        }
        else
        {
            target = lines[0].transform.position;
        }
        FireRocket();
    }

    private void FireRocket()
    {

        if (portalMode)
        {
            
            transform.LookAt(target);
            transform.Rotate(90,0,0);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * forceMultiplier);
        }
        else
        {
            Debug.Log(target.x);
            transform.LookAt(target);                                                                                    
            transform.Rotate(90,0,0);                                                                                    
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * forceMultiplier);      

        }
        
    }
    

    


    private int randomLane()
    {
        int randomInt = getRandomInt(1, 5);
        switch (randomInt)
        {
            case 1: return 0;
            case 2: return 1;
            case 3: return 2;
            case 4: return 3;
            default: return 0;
        }
    }

    
    private int getRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (portalMode)
        {
            rb.useGravity = false;
            if (CompareTag("orangePortal"))
            {
                Quaternion rot = Quaternion.Euler(0, 180f, 90f); // Portaldaki dönüşü uygulama

                
                Vector3 newDirection = new Vector3(-1f, 0, 0); // Yönün tersini alıyoruz
                
                rb.AddForce(newDirection * 15f, ForceMode.Impulse);
                
            }
        }else if(!portalMode) {
            rb.useGravity = true;
        }
        // Patlama efektini başlat
        ParticleSystem instantiatedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        
        
        // Roketi yok et
        
        
        // Patlama efektini yok et
        Destroy(instantiatedExplosion.gameObject, instantiatedExplosion.main.duration);
    }

    private void findLines()
    {
        
    }
    
}