using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rocket : MonoBehaviour
{
    private GameObject player;  
    private Rigidbody rb;       
    private float playerX;     
    private float randomZ;
    private float targetY;
    public float rocketSpeed = 5f;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); // Oyuncuyu tag'ine göre buluyoruz
        }
        rb = gameObject.GetComponent<Rigidbody>();
        randomZ = randomLane();
        targetY = -1;
    }

    void Update()
    {
        playerX = player.transform.position.x; 
        
    }

    void FixedUpdate()
    {
        MoveTowardsTarget(); 
    }
    
   
    private void MoveTowardsTarget()
    {
        float currentX = Mathf.MoveTowards(transform.position.x, playerX, rocketSpeed * Time.fixedDeltaTime);
        float currentZ = randomZ;
        float currentY = Mathf.MoveTowards(transform.position.y, -1f, rocketSpeed * Time.fixedDeltaTime);
    
        Vector3 newPosition = new Vector3(currentX, currentY, currentZ);
        rb.MovePosition(newPosition);
    }
    
    private float randomLane()
    {
        int randomInt = getRandomInt(1, 5);
        switch (randomInt)
        {
            case 1: return -10f;
            case 2: return -3.5f;
            case 3: return 3.5f;
            case 4: return 10f;
            default: return 10f;
        }
    }
    
    private int getRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }
}
