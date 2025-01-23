using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private Stack<GameObject> hearts = new Stack<GameObject>(5);
    private bool alive = true;
    public ParticleSystem boooom;
    // Start is called before the first frame update
    void Start()
    {
        
        foreach(Transform child in transform)
        {
            hearts.Push(child.gameObject);
        }
    }


    public bool amIDead()
    {
        if (hearts.Count == 1)
        {
            alive = false;
        }
        else
        {
            GameObject destroyedHeart = hearts.Pop();
            ParticleSystem particle = Instantiate(boooom, transform.position, Quaternion.identity);
            Destroy(destroyedHeart);
            Destroy(particle.gameObject, particle.main.duration);
            
            
        }
        return alive;
    }
}
