using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {
    public Camera playerCamera;
    public Camera npcCamera;

    private bool isUsingPlayerCamera = false;

    private void Start() {
        SwitchCamera();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            SwitchCamera();
        }
    }

    void SwitchCamera() {
        if (isUsingPlayerCamera) {
            playerCamera.enabled = false;
            npcCamera.enabled = true;
        } else {
            playerCamera.enabled = true;
            npcCamera.enabled = false;
        }
        isUsingPlayerCamera = !isUsingPlayerCamera;
    }
}