using System;
using UnityEngine;

public class Museum : MonoBehaviour
{
    public CameraRecorder cameraRecorder;
    //private Camera mainCamera;

    private void Start()
    {
        //mainCamera = Camera.main;
        //cameraRecorder = GetComponent<CameraRecorder>();

        cameraRecorder.AddCameraData("Camera.000", new Vector3(-45f, 18f, 0f), Quaternion.Euler(0f, -90f, 0f));
        cameraRecorder.AddCameraData("Camera.001", new Vector3(-135f, 18f, 0f), Quaternion.Euler(0f, 90f, 0f));
        cameraRecorder.AddCameraData("Camera.002", new Vector3(-274f, 15.5f, 2f), Quaternion.Euler(0f, -90f, 0f));
        cameraRecorder.AddCameraData("Camera.003", new Vector3(-150f, 19f, -125f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.004", new Vector3(-125f, 18.5f, -110f), Quaternion.Euler(0f, 120f, 0f));
        cameraRecorder.AddCameraData("Camera.005", new Vector3(-175f, 19f, -110f), Quaternion.Euler(0f, -120f, 0f));
        cameraRecorder.AddCameraData("Camera.006", new Vector3(-266f, 18f, -59f), Quaternion.Euler(0f, -120f, 0f));
        cameraRecorder.AddCameraData("Camera.007", new Vector3(-250f, 15f, 72f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.008", new Vector3(-35f, 14.5f, 58f), Quaternion.Euler(0f, 60f, 0f));
        cameraRecorder.AddCameraData("Camera.009", new Vector3(-238.75f, 16.5f, -74f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.010", new Vector3(-61.25f, 16.5f, 77f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.011", new Vector3(-264f, 17f, 59f), Quaternion.Euler(0f, -60f, 0f));
        cameraRecorder.AddCameraData("Camera.012", new Vector3(-223f, 16.5f, 76f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.013", new Vector3(-49f, 16.5f, -74f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.014", new Vector3(-37f, 15f, -58f), Quaternion.Euler(0f, 120f, 0f));
        cameraRecorder.AddCameraData("Camera.015", new Vector3(-74f, 15.5f, -76f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.016", new Vector3(-276f, 15f, 25f), Quaternion.Euler(0f, -90f, 0f));
        cameraRecorder.AddCameraData("Camera.017", new Vector3(-274f, 15f, -26f), Quaternion.Euler(0f, -90f, 0f));
        cameraRecorder.AddCameraData("Camera.018", new Vector3(-25f, 15f, -26f), Quaternion.Euler(0f, 90f, 0f));
        cameraRecorder.AddCameraData("Camera.019", new Vector3(-25f, 15f, 26f), Quaternion.Euler(0f, 90f, 0f));
        cameraRecorder.AddCameraData("Camera.020", new Vector3(-167f, 17f, 0f), Quaternion.Euler(0f, 270f, 0f));
        cameraRecorder.AddCameraData("Camera.021", new Vector3(-104f, 16.5f, 43f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.022", new Vector3(-78f, 16.5f, 42f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.023", new Vector3(-196f, 16.5f, -42f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.024", new Vector3(-222f, 16.5f, -42f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.025", new Vector3(-197f, 16.5f, 42f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.026", new Vector3(-223f, 16.5f, 42f), Quaternion.Euler(0f, 180f, 0f));
        cameraRecorder.AddCameraData("Camera.027", new Vector3(-102.5f, 16.5f, -42f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.028", new Vector3(-77.5f, 17f, -44f), Quaternion.Euler(0f, 0f, 0f));
        cameraRecorder.AddCameraData("Camera.029", new Vector3(-263f, 23f, 0f), Quaternion.Euler(0f, 90f, 0f));
        cameraRecorder.AddCameraData("Camera.030", new Vector3(-141f, 20f, 173f), Quaternion.Euler(0f, 90f, 0f));
        cameraRecorder.AddCameraData("Camera.031", new Vector3(-159f, 19f, 172f), Quaternion.Euler(0f, 270f, 0f));
        cameraRecorder.AddCameraData("Camera.032", new Vector3(-133f, 20f, 241f), Quaternion.Euler(0f, 270f, 0f));
        cameraRecorder.AddCameraData("Camera.033", new Vector3(-166f, 21f, 241f), Quaternion.Euler(0f, 90f, 0f));
  }

    private void Update()
    {
        //Debug.Log(mainCamera.transform.position);

        //Vector3 position = cameraRecorder.GetCameraPosition("Camera.000");
        //Quaternion orientation = cameraRecorder.GetCameraOrientation("Camera.000");
        //mainCamera.transform.position = position;
        //mainCamera.transform.rotation = orientation;

        //Debug.Log(position);
        //Debug.Log(orientation);

    }
}
