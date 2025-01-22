using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rocket : MonoBehaviour
{
    private GameObject target;
    private Rigidbody rb;// Hedef objesi

    public BossMovements boss;
    private float moveSpeed = 10f; // Fırlatma hızı

    public ParticleSystem explosion;

    private GameObject bluePortal;
    private GameObject orangePortal;
    private Coroutine portalCoroutine;

    private void Start()
    {
        bluePortal = GameObject.FindWithTag("bluePortal");
        orangePortal = GameObject.FindWithTag("orangePortal");
        StartCoroutine(destroyAfterTime());
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
        boss = bossObject.GetComponent<BossMovements>();
        rb = GetComponent<Rigidbody>();
        

        if (gameObject.CompareTag("left"))
        {
            int targetLane = UnityEngine.Random.Range(0, 2);
            if (targetLane == 0)
            {
                target = GameObject.FindWithTag("line3");
            }
            else if (targetLane == 1)
            {
                target = GameObject.FindWithTag("line4");
            }
        }else {
            int targetLane = UnityEngine.Random.Range(0, 2);
            if (targetLane == 0)
            {
                target = GameObject.FindWithTag("line1");
            }
            else if (targetLane == 1)
            {
                target = GameObject.FindWithTag("line2");
            }
        }

        // Hedefe doğru hareket etmeye başla
        FireRocket();
    }

    private void FireRocket()
    {
        if (target == null) return;

        if (boss.rocketControl)
        {
            moveSpeed = 15f;
            rb.useGravity = false;
            portalCoroutine = StartCoroutine(portalMode());


        }
        else
        {
            moveSpeed = 10f;
            rb.useGravity = true;
            StartCoroutine(MoveToTarget());
        }

        
        
    }

    private IEnumerator MoveToTarget()
    {
        while (target != null && Vector3.Distance(transform.position, target.transform.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
    //I know i can make only 1 coroutine for all movements but im lazy sorry

    private IEnumerator portalMode()
    {
        float xOffset = Random.Range(-0.5f, 0.5f); // Sağ-sol sapma
            
        float z = GetRandomFloat(-0.3f, 0.3f);
        while (true)
        {
            Vector3 targetPosition = new Vector3(transform.position.x + 1 + xOffset, transform.position.y - 0.2f, transform.position.z + z );
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator returntheBAAAALLSS()
    {
        moveSpeed = 60f;
        while (true)
        {
            Vector3 targetPosition = new Vector3(transform.position.x + -1 , transform.position.y, transform.position.z );
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("orangePortal")! || other.gameObject.CompareTag("bluePortal")!|| boss.rocketControl == false)
        {
            ParticleSystem instantiatedExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(instantiatedExplosion.gameObject, instantiatedExplosion.main.duration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("orangePortal"))
        {
            Debug.Log("Portala çarptık amınake");
            StopCoroutine(portalCoroutine);
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(bluePortal.transform.position.x-3,bluePortal.transform.position.y, bluePortal.transform.position.z);
            
            StartCoroutine(returntheBAAAALLSS());
        }
       
    }

    public float GetRandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

    IEnumerator destroyAfterTime()
    {
        yield return new WaitForSeconds(15f);
        ParticleSystem instantiatedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(instantiatedExplosion.gameObject, instantiatedExplosion.main.duration);
    }


}
