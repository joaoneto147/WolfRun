using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golen : EnemyController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            body.velocity = new Vector2(direction * movimentSpeed, body.velocity.y);

        if (isAtack)
            AtackPlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            DamageEnemy();
        }
    }
}
