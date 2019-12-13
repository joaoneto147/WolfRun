using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GenericController
{
    [SerializeField] public float distanceAttack;
    protected bool isMoving = false;

    protected Rigidbody2D body;
    protected Transform player;
    protected SpriteRenderer sprite;

    protected override void Awake()
    {       
        body = GetComponent<Rigidbody2D>();       
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        base.Awake();
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

    protected void AtualizarInfoAnimacao()
    {
        ControleAnimacao("Walking", isMoving);
        ControleAnimacao("Walking", isMoving);
        ControleAnimacao("Alive", health >= 0);

    }
}
