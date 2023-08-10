using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
    public float moveSpeed = 5.0f;       // �ƶ��ٶ�
    public float mouseSensitivity = 2.0f; // ���������

    // Start is called before the first frame update
    void Start() {
        // ���ز��������ָ��
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        // ��ȡ�ƶ�����
        float x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // ִ���ƶ�
        transform.Translate(new Vector3(x, 0, z));

        // ����ӽǿ���
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
    }
}
