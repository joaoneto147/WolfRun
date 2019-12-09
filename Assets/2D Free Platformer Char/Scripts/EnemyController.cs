using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    protected int direction = 1;
    [SerializeField] protected int health;
    [SerializeField] protected int speed;

    protected bool isMoving = false;
    [SerializeField] public float distanceAttack;

    private Vector3 scaleOriginal;

    protected Rigidbody2D body;
    protected Animator anim;
    protected Transform player;
    protected SpriteRenderer sprite;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();

        scaleOriginal = transform.localScale;


    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(player.position, transform.position);
    }

    protected void TratarDirecao()
    {
        if ((player.position.x > transform.position.x && direction < 0) || (player.position.x < transform.position.x && direction > 0))
            Flip();
    }

    protected void Flip()
    {

        direction *= -1;
        speed *= -1;

        //Turn the character by flipping the direction


        //Record the current scale
        Vector3 scale = transform.localScale;

        //Set the X scale to be the original times the direction
        scale.x = scaleOriginal.x * direction;

        //Apply the new scale
        transform.localScale = scale;

    }

    protected void AtualizarInfoAnimacao()
    {
        ControleAnimacao("Walking", isMoving);
        ControleAnimacao("Walking", isMoving);
        ControleAnimacao("Alive", health >= 0);

    }

    private void ControleAnimacao(string descricao, bool valor)
    {
        anim.SetBool(descricao, valor);
    }
}
