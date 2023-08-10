using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class AvatarFollowCamera : MonoBehaviour {

    [SerializeField] private Transform mainCamera;
    private Animator animator;
    private Vector3 lastCameraPosition;
    private CharacterController characterController;

    private float xOffset = 4f;
    private const float zOffset = 10f;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        LookAtCamera();
        // transform.position = new Vector3(mainCamera.position.x + 4f, transform.position.y, mainCamera.position.z + 10f);
        lastCameraPosition = mainCamera.position;
    }

    // Update is called once per frame
    void Update() {
        var desiredPosition = new Vector3(mainCamera.position.x + xOffset, transform.position.y, mainCamera.position.z + zOffset);
        // Debug.Log(Vector3.Distance(desiredPosition, transform.position));
        if (Vector3.Distance(desiredPosition, transform.position) < 0.5f) {
            LookAtCamera();
        } else {
            MoveToDesiredPosition();
        }
        if (mainCamera.position == lastCameraPosition) {
            transform.position = desiredPosition;
        } else {
            lastCameraPosition = mainCamera.position;
        }
    }

    void MoveToDesiredPosition() {
        var desiredPosition = new Vector3(mainCamera.position.x + xOffset, transform.position.y, mainCamera.position.z + zOffset);
        transform.rotation = Quaternion.LookRotation(
            new Vector3(transform.position.x - mainCamera.position.x, 0, transform.position.z - mainCamera.position.z)
        );
        animator.SetBool("Walk", true);
        // var newPosition = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime);
        characterController.Move((desiredPosition - transform.position) * 30 * Time.deltaTime);
    }

    void LookAtCamera() {
        animator.SetBool("Walk", false);
        Quaternion rotation = Quaternion.LookRotation(
                new Vector3(mainCamera.position.x - transform.position.x, 0, mainCamera.position.z - transform.position.z)
            );
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.name == "walls.001") {
            // 如果 avatar 在相机的左侧，将 xOffset 更改为正值，否则更改为负值
            if (transform.position.x < mainCamera.position.x) {
                xOffset = Mathf.Abs(xOffset);
            } else {
                xOffset = -Mathf.Abs(xOffset);
            }
            MoveToDesiredPosition();
        }
    }
}
