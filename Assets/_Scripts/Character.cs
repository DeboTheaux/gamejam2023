using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Partes del cuerpo
    [SerializeField] private BodyPart[] _bodyParts;
    [SerializeField] private CameraController _camera;
    //Parte del cuerpo que estoy enfocando
    private BodyPart _bodyPartFocused;

    private void Start()
    {
        _bodyPartFocused = _bodyParts[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectBodyPart();
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
