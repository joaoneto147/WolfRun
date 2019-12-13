using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golen : EnemyController
{
    // Start is called before the first frame update
    protected override void Start()
    {        
        health = 2;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = PlayerDistance();
        isMoving = (distance < distanceAttack) && (distance > 0.5f);
       
        TratarDirecao();
        AtualizarInfoAnimacao();
    }

    private void FixedUpdate()
    {
        if (isMoving)
            body.velocity = new Vector2(direction * movimentSpeed, body.velocity.y);


        Debug.Log(movimentSpeed);
    }
}
