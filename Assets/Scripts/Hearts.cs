using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    private Stack<GameObject> hearts = new Stack<GameObject>(5);
    private bool alive = true;
    public ParticleSystem boooom;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    // Start is called before the first frame update
    void Start()
    {
        hearts.Push(heart1);
        hearts.Push(heart2);
        hearts.Push(heart3);
        hearts.Push(heart4);
        hearts.Push(heart5);
    }


    public bool amIDead()
    {
        if (hearts.Count == 0)
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
