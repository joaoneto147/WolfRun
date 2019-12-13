using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Mover();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Mover();
        }
    }

    private void Mover()
    {

    }
}
