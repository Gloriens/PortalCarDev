using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocket;  
    public GameObject player;  // Roketin hedefi olan oyuncu
    public float launchInterval = 2f;
    private int min = 4;
    private int max = 8;

    private Coroutine rocketCoroutine;
    public float rocketSpeed = 15f; 
    
    private IEnumerator RocketLaunch()
    {
        while (true)
        {
            
            
            Quaternion rocketRotation = Quaternion.Euler(0f,0, 250f);
            
            Instantiate(rocket, transform.position, rocketRotation);

            Debug.Log("Rocket Launched");
            
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
            Debug.Log("Roket ateşleme korotinindeyim");
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
