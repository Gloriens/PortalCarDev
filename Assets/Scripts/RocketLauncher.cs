using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocket;  
    public GameObject player;  // Roketin hedefi olan oyuncu
    public float launchInterval = 2f;
    private int min = 3;
    private int max = 5;

    private Coroutine rocketCoroutine;
    
    private IEnumerator RocketLaunch()
    {
        while (true)
        {
            
            
            Instantiate(rocket, transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(getRandomInt(min, max));  // Farklı aralıklarla roket atılacak
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
