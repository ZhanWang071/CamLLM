using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarController : MonoBehaviour {
    public static event Action OnAvatarReached;
    private bool canContinue = false;

    [SerializeField] private CameraRecorder cameraRecorder;

    private Transform avatarTransform;
    private static Vector3 targetPosition;
    private static Quaternion targetRotation;

    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private Animator animator;

    private void Start() {
        avatarTransform = transform;

        targetPosition = avatarTransform.position;

        SetCameraToNearestNavMeshPosition();
    }

    private void SetCameraToNearestNavMeshPosition() {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(avatarTransform.position, out hit, 20f, NavMesh.AllAreas)) {
            avatarTransform.position = hit.position;
            navMeshAgent.SetDestination(hit.position);
        } else {
            Debug.LogWarning("Could not find a valid NavMesh position for the camera starting point.");
        }
    }


    private void OnEnable() {
        CameraController.OnTourIDsReceived += HandleTourIDs;
        CameraController.OnCameraReached += HandleCameraReached;
    }

    private void OnDisable() {
        CameraController.OnTourIDsReceived -= HandleTourIDs;
        CameraController.OnCameraReached -= HandleCameraReached;
    }

    private void HandleCameraReached() {
        canContinue = true;
    }

    private void HandleTourIDs(string[] tourIDs) {
        Debug.Log("Avatar Start Navigation");
        StartCoroutine(NavigationTour(tourIDs));
    }

    private float waitTime = 1f;
    private float rotationSpeed = 200f;

    private IEnumerator NavigationTour(string[] tourIDs) {
        for (int i = 0; i < tourIDs.Length; i++) {
            // update Avatar target positions and orientations
            UpdateTargetAvatar(tourIDs[i]);
            animator.SetBool("Walk", true);
            navMeshAgent.SetDestination(targetPosition);

            // Wait until the camera reaches the painting
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                yield return null;
            }
            animator.SetBool("Walk", false);

            // After the camera reaches the target painting, rotate the camera smoothly
            while (Quaternion.Angle(avatarTransform.rotation, targetRotation) > 0.1f)
            {
                avatarTransform.rotation = Quaternion.RotateTowards(avatarTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }

            OnAvatarReached?.Invoke();
            yield return new WaitUntil(() => canContinue);
            canContinue = false;

            // Wait at the current position
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void UpdateTargetAvatar(string tourID) {
        string cameraID = tourID.Replace("painting ", "Avatar.");

        targetPosition = cameraRecorder.GetCameraPosition(cameraID);
        targetRotation = cameraRecorder.GetCameraOrientation(cameraID);
    }

}
