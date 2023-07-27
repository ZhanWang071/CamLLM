using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform cameraTransform;

    public CameraRecorder cameraRecorder;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private static Vector3 targetPosition;
    private static Quaternion targetRotation;

    [System.Serializable]
    public class TourResponse
    {
        public string reasoning;
        public string[] Tour;
        public string[] TourID;
    }

    public string[] tourIDs;

    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        // Find the main camera GameObject and Transform
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        cameraTransform = mainCamera.transform;

        // Perform initial camera setup
        startPosition = cameraTransform.position;
        startRotation = cameraTransform.rotation;
        targetPosition = cameraTransform.position;
        targetRotation = cameraTransform.rotation;

        // Ensure that the camera starts in a valid NavMesh area
        SetCameraToNearestNavMeshPosition();
    }

    private void SetCameraToNearestNavMeshPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(cameraTransform.position, out hit, 20f, NavMesh.AllAreas))
        {
            cameraTransform.position = hit.position;
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("Could not find a valid NavMesh position for the camera starting point.");
        }
    }

    private void Update() { }

    public void ProcessChatGPTResponse(string chatGPTContent)
    {
        if (!string.IsNullOrEmpty(chatGPTContent))
        {
            // Parse the response and extract camera control commands
            TourResponse tourResponse = JsonUtility.FromJson<TourResponse>(chatGPTContent);
            tourIDs = tourResponse.TourID;
            //string reasoning = tourResponse.Reasoning;
            string[] tours = tourResponse.Tour;

            StartCoroutine(MoveCamera(tourIDs));
            Debug.Log(tourIDs.Length);
        }
    }


    public void PlayButtonClicked()
    {
        Debug.Log("Replay the camera");
        StartCoroutine(MoveCamera(tourIDs));
    }

    private void UpdateTargetCamera(string tourID)
    {
        string cameraID = tourID.Replace("painting ", "Camera.");

        targetPosition = cameraRecorder.GetCameraPosition(cameraID);
        targetRotation = cameraRecorder.GetCameraOrientation(cameraID);
    }

    private float waitTime = 1f;
    private float rotationSpeed = 30f;

    private IEnumerator MoveCamera(string[] tourIDs)
    {
        // update camera start and target positions and orientations
        UpdateTargetCamera(tourIDs[0]);

        for (int i = 0; i < tourIDs.Length; i++)
        {
            UpdateTargetCamera(tourIDs[i]);
            navMeshAgent.SetDestination(targetPosition);

            // Wait until the camera reaches the painting
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null;
            }

            // After the camera reaches the target painting, rotate the camera smoothly
            while (Quaternion.Angle(cameraTransform.rotation, targetRotation) > 0.1f)
            {
                cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait at the current position
            yield return new WaitForSeconds(waitTime);
        }

    }

}