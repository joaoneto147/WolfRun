using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : GenericController
{


    private float moveDirection;
    private Rigidbody2D body;

    [SerializeField] private bool isGrounded;
    public Transform feetPosition;
    public float sizeRadius = 0.2f;
    public LayerMask whatIsGround;


    [SerializeField] private GameObject skillPrefab = null;
    [SerializeField] private Transform skillPoint = null;
    [SerializeField] private float jumpForce = 25;

    protected override void Awake()
    {
        base.Awake();
    }
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
    protected override void Update()
    {
        base.Update();
        moveDirection = Input.GetAxis("Horizontal");

        //Caso precise girar o personagem
        if (moveDirection * direction < 0)
            Flip();

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, sizeRadius, whatIsGround);
        isJumping = (Input.GetButtonDown("Jump") && isGrounded);
        isAtack = Input.GetButtonDown("Fire1") && (timeAtack <= timeLastAtack);
        isMoving = moveDirection != 0;

        //Pulo do personagem
        if (isJumping)
        {
            Jump();
        }

        if (isAtack)
            StartCoroutine(Atack());

        //Controlar animações
        AtualizarInfoAnimacao();        
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(moveDirection * movimentSpeed, body.velocity.y);

        if (transform.position.y < -9)
        {
            SceneManager.LoadScene("Fase1");
        }

    }

    IEnumerator Atack()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(skillPrefab, skillPoint.position, Quaternion.identity);
        timeLastAtack = 0;
    }

    private void Jump()
    {
        body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        //body.velocity = Vector2.up * jumpForce; --Apenas para questões de conhecimento podemos manipular o pulo desta forma tbm;
    }

    private void AtualizarInfoAnimacao()
    {
        ControleAnimacao("Walking", isMoving);
        ControleAnimacao("Jumping", isJumping);
        ControleAnimacao("Atack", isAtack);
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
        if (invunerable)
            return;

        health--;
        invunerable = true;

        audioSource.clip = soundDamageTaken;
        audioSource.Play();

        StartCoroutine(Damage());

        if (health < 1)
        {
            SceneManager.LoadScene("Fase1");
        }
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public int getDirection()
    {
        return direction;
    }

}
