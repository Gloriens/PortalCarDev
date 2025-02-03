using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RocketLauncher : MonoBehaviour
{
    [FormerlySerializedAs("rocket")] public GameObject littleBall;
    public GameObject bigBall;
    public float launchInterval = 2f;
    private int min = 3;
    private int max = 5;
    private BossMovements bossMovements;

    private Coroutine rocketCoroutine;

    private void Start()
    {
        GameObject bossObject = GameObject.FindGameObjectWithTag("Boss");
        bossMovements = bossObject.GetComponent<BossMovements>();
    }

    private IEnumerator RocketLaunch()
    {
        while (true)
        {
            if (!bossMovements.rocketControl)
            {
                GameObject newRocket = Instantiate(bigBall, transform.position, Quaternion.Euler(0, 0, 240));
            }
            else
            {
                GameObject newRocket = Instantiate(littleBall, transform.position, Quaternion.Euler(0, 0, 240));
            }
            
            
            
            yield return new WaitForSeconds(getRandomInt(min, max));  
        }
    }

    private int getRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }

    public void StartRocket()
    {
        if (rocketCoroutine == null)
        {
            rocketCoroutine = StartCoroutine(RocketLaunch());
        }
    }

    public void StopRocket()
    {
        if (rocketCoroutine != null)
        {
            StopCoroutine(rocketCoroutine);
            rocketCoroutine = null; 
        }
    }

   
}
