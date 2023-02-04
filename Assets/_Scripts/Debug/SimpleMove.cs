using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public CharacterController characterController;
    public ProceduralPath proceduralPath;
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move = new Vector3(1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move = new Vector3(-1f, 0, 0);
        }

        if (move != Vector3.zero)
        {
            transform.position += move.normalized * speed * Time.deltaTime;
            Vector3 shouldGo = Vector3.zero;
            if (proceduralPath.UpdatePosition(transform.position, out shouldGo))
            {
                transform.position = shouldGo;
            }
        }

    }
}
