using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] _;
    [SerializeField] private float _rotationSpeed;

    private Transform _bodyPartToFocus;

    private void Update()
    {
        if(_bodyPartToFocus != null)
        {
            var finalRotation = Quaternion.LookRotation(_bodyPartToFocus.position - transform.position, transform.up);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, finalRotation, _rotationSpeed);
        }
    }

    public void Focus(Transform bodyPartToFocus)
    {
        _bodyPartToFocus = bodyPartToFocus;
    }
}
