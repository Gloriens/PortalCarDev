using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Hearts hearts;
    private bool amIAlive;
    public ParticleSystem shipExplosion;
    private UIController uiController;
    void Start()
    {
        uiController = GameObject.Find("EventSystem").GetComponent<UIController>();
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
            StartCoroutine(victory());
        }
    }

    IEnumerator victory()
    {
        ParticleSystem temp = Instantiate(shipExplosion, transform.position, Quaternion.identity);
        deactivateAllChildren();
        Destroy(temp.gameObject, temp.main.duration);
        yield return new WaitForSeconds(2);
        Destroy(temp);
        uiController.victory();
        Time.timeScale = 0;
    }
    
    void deactivateAllChildren()
    {
        
        foreach (Transform child in gameObject.transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
            
        }
    }
    
    
}
