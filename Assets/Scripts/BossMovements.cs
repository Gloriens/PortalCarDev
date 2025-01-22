using System;
using System.Collections;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    public GameObject player;
    public RocketLauncher rocketLauncher1;
    public RocketLauncher rocketLauncher2;
    public Rocket rocket;
    public Camera mainCamera;
    
    private bool startFire;
    private static int fireDelay;
    
    private Rigidbody rb;
    public float waitTime = 5f;
    private Vector3 offset = new Vector3(-50, 30, 0);
    private Animator anim;

    private bool timeHasCome = false;
    private bool planeArrived = false;
    private bool startLanding = false;
    private bool  goLanding = false;
    private bool planeonLine = false;
    public bool rocketControl = false;
    

    private float descendSpeed = 2f;
    private float targetHeight = 10f;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TeleportToPlayerOffset());
        StartCoroutine(landingControl(20f));
    }

    private void Update()
    {
        if (timeHasCome)
        {
            transform.position = new Vector3(player.transform.position.x + -65, transform.position.y, 0f);
            if (!startLanding) 
            {
                StartCoroutine(DramaticEntrance());
                startLanding = true;
                
            }
        }

        if (goLanding)
        {
            
            StartCoroutine(LandtoLine());
            StartCoroutine(landingControl(32f));

        }
        
        

        if (startFire)
        {
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
        float targetY = 15f; 
        float threshold = 0.1f;  

        while (!planeArrived)
        {
            if (Mathf.Abs(transform.position.y - targetY) < threshold)
            {
                StartCoroutine(changeViewStartFire(54, 65, 2));
                planeArrived = true;
                rb.velocity = Vector3.zero;
                
                
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, -descendSpeed, rb.velocity.z); 
                yield return null;
            }
        }
    }
    
    

   
    
    
    
    
    IEnumerator changeViewStartFire(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        startFire = true;
        
        mainCamera.fieldOfView = endValue;
    }

    IEnumerator LandtoLine()
    {
        goLanding = false; 
        StopRockets();
        
        yield return StartCoroutine(changePos(5, 0.1f)); 
        // Hedefte bekleme

        StartCoroutine(StartRocketsAfterTime(0));
        rocketControl = true;
        yield return new WaitForSeconds(12f);
        
        StopRockets();
        rocketControl = false;
        yield return StartCoroutine(changePos(15, 0.1f));

        StartCoroutine(StartRocketsAfterTime(1));
        
    }

    

    IEnumerator landingControl(float time)
    {
        yield return new WaitForSeconds(time);
        goLanding = true;
    }

    IEnumerator changePos(float targetY, float threshold)
    {
        
        planeonLine = false;

        while (!planeonLine)
        {
            if (Mathf.Abs(transform.position.y - targetY) < threshold)
            {
                planeonLine = true; // Hedef pozisyona ulaşıldı.
                rb.velocity = Vector3.zero;
                
            }
            else
            {
                float direction = targetY > transform.position.y ? 1 : -1;
                rb.velocity = new Vector3(rb.velocity.x, direction * descendSpeed, rb.velocity.z);
                yield return null;
            }
        }
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
    
    

