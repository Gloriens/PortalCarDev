using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private Stack<GameObject> hearts;
    [SerializeField] private int heartCount;
    private bool alive = true;
    public ParticleSystem boooom;
    // Start is called before the first frame update
    void Start()
    {
        hearts = new Stack<GameObject>(heartCount);
        
        foreach(Transform child in transform)
        {
            hearts.Push(child.gameObject);
        }
    }


    public bool amIDead()
    {
        if (hearts.Count == 1)
        {
            GameObject destroyedHeart = hearts.Pop();
            ParticleSystem particle = Instantiate(boooom, transform.position, Quaternion.identity);
            Destroy(destroyedHeart);
            Destroy(particle.gameObject, particle.main.duration);
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
