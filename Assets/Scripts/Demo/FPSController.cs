using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
    public float moveSpeed = 5.0f;       // 移动速度
    public float mouseSensitivity = 2.0f; // 鼠标灵敏度

    // Start is called before the first frame update
    void Start() {
        // 隐藏并锁定鼠标指针
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        // 获取移动输入
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // 执行移动
        transform.Translate(new Vector3(x, 0, z));

        // 鼠标视角控制
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
    }
}
