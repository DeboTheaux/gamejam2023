using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Partes del cuerpo
    [SerializeField] private BodyPart[] _bodyParts;
    [SerializeField] private CameraController _camera;
    //Parte del cuerpo que estoy enfocando
    private BodyPart _bodyPartFocused;

    //Decirle a la cámara que enfoque dicha parte
    private void SelectBodyPart()
    {
        //next _bodyParts
        var index = _bodyParts.ToList().IndexOf(_bodyPartFocused);
        _bodyPartFocused = _bodyParts[index++];
        CameraFocusBodyPart(_bodyPartFocused);
    }

    //Decirle a la cámara que enfoque dicha parte
    private void CameraFocusBodyPart(BodyPart bodyPart)
    {
        _camera.Focus(bodyPart.gameObject);
    }

}

public class BodyPart : MonoBehaviour
{
    public bool isEnabled;
}

public class RightHand : BodyPart
{

}

public class LefttHand : BodyPart
{

}

public class RightFoot : BodyPart
{

}

public class LeftFoot : BodyPart
{

}

public class Chest : BodyPart
{

}