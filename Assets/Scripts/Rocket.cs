using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GameObject target;
    private Rigidbody rb;// Hedef objesi

    public bool portalMode;
    private float moveSpeed = 10f; // Fırlatma hızı

    public ParticleSystem explosion;

    private void Start()
    {
        portalMode = false;
        

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
        if (target == null) return; // Eğer hedef yoksa, işlem yapma.

        // Hedefe doğru sürekli hareket et
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        while (target != null && Vector3.Distance(transform.position, target.transform.position) > 0.1f)
        {
            // Hedefin konumuna doğru her frame'de hareket et
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

            yield return null; // Bir sonraki frame için bekle
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        // Patlama efektini başlat
        ParticleSystem instantiatedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        
        
        // Roketi yok et
        
        
        // Patlama efektini yok et
        Destroy(instantiatedExplosion.gameObject, instantiatedExplosion.main.duration);
    }


}
