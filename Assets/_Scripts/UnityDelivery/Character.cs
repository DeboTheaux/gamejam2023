using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour, IObserver<Dimension>
{
    //Partes del cuerpo
    public float speed;
    public ProceduralPath proceduralPath;
    public float hitDistance;
    public LayerMask layerMask;

    [SerializeField] private BodyPart[] _bodyParts;
    [SerializeField] private CameraController _camera;

    private List<Affect> affects = new List<Affect>();
    private float affectSpeed;
    //Parte del cuerpo que estoy enfocando
    private BodyPart _bodyPartFocused;

    private bool reality = true;

    internal bool HasAffect(Affect affect)
    {
        return affects.Contains(affect);
    }

    private Dimension _currentDimension;

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
                _camera.Focus(null);
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
                transform.position += move.normalized * Mathf.Max(speed - affectSpeed, 0) * Time.deltaTime;
                Vector3 shouldGo = Vector3.zero;

                if (!reality)
                {
                    if (proceduralPath.UpdatePosition(transform.position, out shouldGo))
                    {
                        transform.position = shouldGo;
                    }

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
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log($"click {_currentDimension}");
                _currentDimension?.ExecuteAction();
            }
        }    
    }

    //Decirle a la c?mara que enfoque dicha parte
    private void SelectBodyPart()
    {
        //next _bodyParts
        var index = _bodyParts.ToList().IndexOf(_bodyPartFocused);
        var next = ++index % _bodyParts.Length;
        _bodyPartFocused = _bodyParts[next];
        CameraFocusBodyPart(_bodyPartFocused);
    }

    //Decirle a la c?mara que enfoque dicha parte
    private void CameraFocusBodyPart(BodyPart bodyPart)
    {
        _camera.Focus(bodyPart.transform);
    }

    public void AddAffect(Affect affect)
    {
        affects.Add(affect);
        CalculateAffects();
    }

    public void RemoveAffect(Affect affect)
    {
        affects.Remove(affect);
        CalculateAffects();
    }

    private void CalculateAffects()
    {
        affects.ForEach(affect =>
        {
            if (affect.type == Affect.Type.SPEED)
            {
                affectSpeed += affect.value;
            }
            if(affectSpeed != 0)
            {
                _bodyParts[0].ChangeState(BodyPart.State.DAMAGED);
            } else
            {
                _bodyParts[0].ChangeState(BodyPart.State.NORMAL);
            }
        });
    }

    public void OnCompleted()
    {
       
    }

    public void OnError(Exception error)
    {
     
    }

    public void OnNext(Dimension value)
    {
        reality = value is RealityDimension;
        _currentDimension = value;
    }
}
