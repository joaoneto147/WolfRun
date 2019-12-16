using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericController : MonoBehaviour
{

    [SerializeField] protected int direction = 1;
    [SerializeField] protected int movimentSpeed;
    [SerializeField] protected int health = 3;
    [SerializeField] protected float timeAtack;
    [SerializeField] protected bool invunerable = false;

    [SerializeField] protected AudioClip soundAtack;
    [SerializeField] protected AudioClip soundDamageTaken;
    protected AudioSource audioSource;



    //Control variables
    protected bool isMoving = false;
    protected bool isAtack = false;
    protected bool isAlive = false;
    protected bool isJumping = false;
    protected float timeLastAtack;

    protected Vector3 scaleOriginal;
    protected Animator anim;

    protected virtual void Start()
    {
        anim.SetLayerWeight(0, 1);
        audioSource = GetComponent<AudioSource>();

        //Iniciando já podendo atacar
        timeLastAtack = timeAtack + 1;
    }

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        scaleOriginal = transform.localScale;
    }

    protected void Flip()
    {
        direction *= -1;
        Vector3 scale = transform.localScale;
        scale.x = scaleOriginal.x * direction;
        transform.localScale = scale;
    }

    protected void ControleAnimacao(string descricao, bool valor)
    {
        anim.SetBool(descricao, valor);
    }

    protected virtual void Update()
    {
        timeLastAtack += 1 * Time.deltaTime;
    }
}
