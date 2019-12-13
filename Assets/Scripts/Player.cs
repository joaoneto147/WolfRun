using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : GenericController
{
    private bool jumping = false;
    
    private float move;
    private Rigidbody2D body;

    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    public float sizeRadius = 0.2f;
    public LayerMask whatIsGround;

    [SerializeField] private float jumpForce = 25;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();       
        body = GetComponent<Rigidbody2D>();
    }

    public bool isInvunerable()
    {
        return invunerable;
    }

    //Update is called once per frame
    void Update()
    {
        //Reconhecer o chão
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, sizeRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");

        //Caso precise girar o personagem
        if (move * direction > 0)
            Flip();

        //Se apertou para pular
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumping = true;
        }

        //Controlar animações
        AtualizarInfoAnimacao();

    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(move * movimentSpeed, body.velocity.y);

        //Pulo do personagem
        if (jumping)
        {
            body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            //body.velocity = Vector2.up * jumpForce; --Apenas para questões de conhecimento podemos manipular o pulo desta forma tbm;
            jumping = false;
        }

        if (transform.position.y < -9)
        {
            SceneManager.LoadScene("Fase1");
        }

    }

    private void AtualizarInfoAnimacao()
    {
        ControleAnimacao("Walking", move != 0);
        ControleAnimacao("Jumping", jumping);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPosition.position, sizeRadius);
    }

    IEnumerator Damage()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();

        for (float i = 0f; i < 1; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invunerable = false;
    }

    public void DamagePlayer()
    {
        health--;
        invunerable = true;

        StartCoroutine(Damage());

        if (health < 1)
        {
            SceneManager.LoadScene("Fase1");
        }
    }

}
