using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;

    public float minX = -60f;
    public float maxX = 60f;

    public float sensitivity;
    public Camera cam;

    public Transform originalPosition;
    public Transform lookPosition;

    float rotY = 0f;
    float rotX = 0f;

    [SerializeField] private Transform[] _;
    [SerializeField] private float _rotationSpeed;

    private Transform _bodyPartToFocus;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(_bodyPartToFocus != null)
        {
            if(Vector3.Distance(cam.transform.localPosition, lookPosition.localPosition) > .1f)
            {
                Vector3 position = Vector3.Lerp(cam.transform.localPosition, lookPosition.localPosition, _rotationSpeed);
                cam.transform.localPosition = position;

                Vector3 rotation = Vector3.Lerp(cam.transform.localRotation.eulerAngles, lookPosition.localRotation.eulerAngles, _rotationSpeed);
                cam.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, 0);

            } else {

                var finalRotation = Quaternion.LookRotation(_bodyPartToFocus.position - transform.position, transform.up);

                Vector3 rotation = Vector3.Lerp(cam.transform.localRotation.eulerAngles, finalRotation.eulerAngles, _rotationSpeed);
                //cam.transform.localEulerAngles = new Vector3(0, rotation.y, 0);
                //cam.transform.localEulerAngles = new Vector3(rotation.x, 0, 0);
            }
        } else
        {

            Vector3 position = Vector3.Lerp(cam.transform.localPosition, originalPosition.localPosition, _rotationSpeed);
            cam.transform.localPosition = position;

            Vector3 rotation = Vector3.Lerp(cam.transform.localRotation.eulerAngles, originalPosition.localRotation.eulerAngles, _rotationSpeed);
            cam.transform.localEulerAngles = new Vector3(rotation.x, rotation.y, 0);
        }
    }

    public void Focus(Transform bodyPartToFocus)
    {
        _bodyPartToFocus = bodyPartToFocus;
    }


    public void LookToMouse()
    {
        rotY += Input.GetAxis("Mouse X") * sensitivity;
        rotX += Input.GetAxis("Mouse Y") * sensitivity;

        rotX = Mathf.Clamp(rotX, minX, maxX);

        transform.localEulerAngles = new Vector3(0, rotY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotX, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Cursor.visible && Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
