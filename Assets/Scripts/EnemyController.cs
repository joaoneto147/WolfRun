using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GenericController
{
    [SerializeField] public float distanceAttack;
    [SerializeField] public float distanceMove;

    protected Rigidbody2D body;
    protected Player player;
    protected SpriteRenderer sprite;

    protected override void Awake()
    {       
        body = GetComponent<Rigidbody2D>();       
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        float distance = PlayerDistance();
        isAlive = (health > 0);
        isAtack = (timeAtack <= timeLastAtack) && (distance <= distanceAttack) && isAlive;
        isMoving = (distance >= distanceAttack) && (distance < distanceMove) && !isAtack && isAlive;
        TratarDirecao();
        AtualizarInfoAnimacao();
    }

    protected float PlayerDistance()
    {
        return Vector2.Distance(player.GetTransform().position, transform.position);
    }

    protected void TratarDirecao()
    {
        if ((player.GetTransform().position.x > transform.position.x && direction < 0) || (player.GetTransform().position.x < transform.position.x && direction > 0))
            Flip();
    }

    protected void AtualizarInfoAnimacao()
    {
        ControleAnimacao("Walking", isMoving);
        ControleAnimacao("Atack", isAtack);
        ControleAnimacao("Alive", health > 0);

    }

    protected void AtackPlayer()
    {
        audioSource.clip = soundAtack;
        audioSource.Play();

        timeLastAtack = 0;
        player.DamagePlayer();
    }

    protected void DamageEnemy()
    {
        health--;

        audioSource.clip = soundDamageTaken;
        audioSource.Play();

        if (health <= 0)
        {
            AtualizarInfoAnimacao();
            Destroy(this.gameObject, 0.7f);
        }
    }
}
