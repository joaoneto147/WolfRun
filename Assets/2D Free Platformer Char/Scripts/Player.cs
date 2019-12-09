using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumping = false;
    private int direction = 1;

    private float move;
    private Rigidbody2D body;
    public Animator anim;

    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    public float sizeRadius = 0.2f;
    public LayerMask whatIsGround;

    public float movimentSpeed = 5;

    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private bool crouching = false;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetLayerWeight(0, 1);
        body = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        //Reconhecer o chão
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, sizeRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");

        //Caso precise girar o personagem
        if (move * direction < 0)
            FlipPlayer();

        //Se apertou para pular
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumping = true;
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            crouching = !crouching;
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
        ControleAnimacao("Crouching", crouching);

    }

    private void ControleAnimacao(string descricao, bool valor)
    {
        anim.SetBool(descricao, valor);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPosition.position, sizeRadius);
    }

    private void FlipPlayer()
    {
        //Turn the character by flipping the direction
        direction *= -1;

        //Record the current scale
        Vector3 scale = transform.localScale;

        //Set the X scale to be the original times the direction
        scale.x = 1 * direction;

        //Apply the new scale
        transform.localScale = scale;

    }

}
