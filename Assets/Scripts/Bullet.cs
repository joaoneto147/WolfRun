using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public ParticleSystem exposionParticle;    
    private Vector3 skillDirection;
    public float destroyTime = 150f;
    private int direction;
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void Awake()
    {
        direction = GameObject.Find("Player").GetComponent<Player>().getDirection();
        skillDirection = new Vector3(direction, 0, 0);
        transform.localScale = GameObject.Find("Player").GetComponent<Player>().transform.localScale;
    }


    void Update()
    {      
        this.transform.Translate(skillDirection * 8 * Time.deltaTime);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //Instantiate(this.exposionParticle, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    

    
}
