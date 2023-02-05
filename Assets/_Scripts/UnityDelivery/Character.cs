using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Partes del cuerpo
    public float speed;
    public ProceduralPath proceduralPath;
    [SerializeField] private BodyPart[] _bodyParts;
    [SerializeField] private CameraController _camera;
    //Parte del cuerpo que estoy enfocando
    private BodyPart _bodyPartFocused;

    private void Start()
    {
        //_bodyPartFocused = _bodyParts[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectBodyPart();
        }



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

        _camera.LookToMouse();
    }

    //Decirle a la cámara que enfoque dicha parte
    private void SelectBodyPart()
    {
        //next _bodyParts
        var index = _bodyParts.ToList().IndexOf(_bodyPartFocused);
        var next = ++index % _bodyParts.Length;
        _bodyPartFocused = _bodyParts[next];
        CameraFocusBodyPart(_bodyPartFocused);
    }

    //Decirle a la cámara que enfoque dicha parte
    private void CameraFocusBodyPart(BodyPart bodyPart)
    {
        _camera.Focus(bodyPart.transform);
    }

}
