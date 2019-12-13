using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericController : MonoBehaviour
{

    [SerializeField] protected int direction = 1;
    [SerializeField] protected int movimentSpeed;
    [SerializeField] protected int health = 3;
    [SerializeField] protected bool invunerable = false;

    protected Vector3 scaleOriginal;
    protected Animator anim;

    protected virtual void Start()
    {
        anim.SetLayerWeight(0, 1);
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
}
