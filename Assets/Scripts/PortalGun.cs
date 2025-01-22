using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PortalGun : MonoBehaviour
{
    private GameObject[] portals = new GameObject[2];
    public GameObject bluePortal;
    public GameObject orangePortal;
    GameObject chosenPortal;
    private int portalIndexCheck;
    
    private BoxCollider portalRenderer;
    private float portalHeight;
    
    private AudioClip[] portalSounds = new AudioClip[2];
    public AudioClip enterSound; // Portala girildiğinde çalacak ses
    public AudioClip exitSound;
    public AudioSource portalAudioSource;
    private int soundCheck;
    
    
   

    // Start is called before the first frame update
    private void Start()
    {
        portalAudioSource = GetComponent<AudioSource>();
        portalIndexCheck = 0;
        portals[0] = bluePortal;
        portals[1] = orangePortal;
        
        portalSounds[0] = enterSound;
        portalSounds[1] = exitSound;
        
     
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            int layerMask = ~LayerMask.GetMask("trigger");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                
                if (hit.collider.gameObject.tag == "Road" )
                {
                    if(hit.collider.gameObject.tag != "Kaldırım")
                    {
                        chosenPortal = bluePortal;
                        portalRenderer = chosenPortal.GetComponent<BoxCollider>();
                        portalHeight = portalRenderer.bounds.size.y;
                           
                       
                        portalHeight = portalRenderer.bounds.size.y;
                        Debug.Log(portalHeight);
                    
                        Vector3 portalPosition = new Vector3(hit.point.x, hit.point.y + portalHeight/2, hit.point.z);
                    
                        chosenPortal.transform.position = portalPosition;
                        portalAudioSource.clip = portalSounds[0];
                        portalAudioSource.Play();
                    }
                }
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            int layerMask = ~LayerMask.GetMask("trigger");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                
                if (hit.collider.gameObject.tag == "Road")
                {
                    if(hit.collider.gameObject.tag != "Kaldırım")
                    {
                        chosenPortal = orangePortal;
                        portalRenderer = chosenPortal.GetComponent<BoxCollider>();
                        
                        portalHeight = portalRenderer.bounds.size.y;
                       
                        portalHeight = portalRenderer.bounds.size.y;
                    
                        Vector3 portalPosition = new Vector3(hit.point.x, hit.point.y +  portalHeight/2, hit.point.z);
                    
                        chosenPortal.transform.position = portalPosition;
                        portalAudioSource.clip = portalSounds[1];
                        portalAudioSource.Play();
                    }
                    
                }
            }
        }
    }
    
}
