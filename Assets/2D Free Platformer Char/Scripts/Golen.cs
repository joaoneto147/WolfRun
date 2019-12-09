using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golen : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        distanceAttack = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = PlayerDistance();
        isMoving = (distance > distanceAttack);
       
        TratarDirecao();
        AtualizarInfoAnimacao();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            body.velocity = new Vector2(speed, body.velocity.y);
    }
}
