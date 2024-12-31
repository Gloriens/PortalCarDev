using System;
using System.Collections;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    public GameObject player;
    public RocketLauncher rocketLauncher1;
    public RocketLauncher rocketLauncher2;
    private bool startFire;
    private static int fireDelay;
    
    private Rigidbody rb;
    public float waitTime = 5f;
    private Vector3 offset = new Vector3(-50, 30, 0);
    private Animator anim;

    private bool timeHasCome = false;
    private bool planeArrived = false;
    private bool startLanding = false;

    private float descendSpeed = 2f;
    private float targetHeight = 10f;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TeleportToPlayerOffset());
    }

    private void Update()
    {
        if (timeHasCome)
        {
            transform.position = new Vector3(player.transform.position.x + -40, transform.position.y, 0f);
            if (!startLanding) 
            {
                StartCoroutine(DramaticEntrance());
                startLanding = true;
                Debug.Log("inişe geçiyom");
                
            }
        }

        if (startFire)
        {
            Debug.Log("Roket ateşlenmeye başlanıyor");
            startFire = false;
            StartCoroutine(StartRocketsAfterTime(fireDelay));

        }
        
    }

    IEnumerator TeleportToPlayerOffset()
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = player.transform.position + offset;
        timeHasCome = true;
    }

    IEnumerator DramaticEntrance()
    {
        planeArrived = false;
        float targetY = 12f; 
        float threshold = 0.1f;  

        while (!planeArrived)
        {
            if (Mathf.Abs(transform.position.y - targetY) < threshold)
            {
                planeArrived = true;
                rb.velocity = Vector3.zero;
                startFire = true;
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, -descendSpeed, rb.velocity.z); 
                yield return null;
            }
        }
    }

    IEnumerator flip()
    {
        anim.SetBool("isFlipping", true);
        yield return new WaitForSeconds(8f);
        anim.SetBool("isFlipping", false);
    }
    

    private IEnumerator StartRocketsAfterTime(int time)
    {
        yield return new WaitForSeconds(time); 
        rocketLauncher1.StartRocket();        
        rocketLauncher2.StartRocket();        
    }

    private void StopRockets()
    {
        rocketLauncher1.StopRocket();
        rocketLauncher2.StopRocket();
    }

    






}