using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Partes del cuerpo
    public float speed;
    public ProceduralPath proceduralPath;
    public float hitDistance;
    public LayerMask layerMask;
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
        
        //Si se mira no camina ni mueve la camara con el mouse
        if (_bodyPartFocused != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SelectBodyPart();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _bodyPartFocused = null;
            }
        } else
        {
            Vector3 move = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                move += transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                move += -transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                move += -transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move += transform.right;
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
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SelectBodyPart();
            }

            _camera.LookToMouse();
        }

        RaycastHit hit;
        Debug.DrawLine(_camera.cam.transform.position, _camera.cam.transform.position + _camera.cam.transform.forward * hitDistance);
        if(Physics.Raycast(_camera.cam.transform.position, _camera.cam.transform.forward, out hit, hitDistance, layerMask)){
            //Objeto al que miro
            Debug.Log(hit.collider.gameObject.name);
        }

        
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
