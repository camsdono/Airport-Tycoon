using System;
using UnityEngine;



public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public int sensitvity = 10;
    public int targetFrameRate;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * sensitvity * Time.deltaTime);
        }
    }
}
