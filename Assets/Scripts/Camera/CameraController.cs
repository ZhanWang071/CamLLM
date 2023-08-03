using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform cameraTransform;

    public CameraRecorder cameraRecorder;

    private Vector3 initalPosition;
    private Quaternion initalRotation;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private static Vector3 targetPosition;
    private static Quaternion targetRotation;

    [System.Serializable]
    public class TourResponse
    {
        public string Introduction;
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
        initalPosition = cameraTransform.position;
        initalRotation = cameraTransform.rotation;

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

    private void Update() 
    {
        // Test camera movement
        Test();
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string chatGPTContent = "{\n    \"Reasoning\": \"The visitor has a special interest in Chinese art, show him more related paintings.\",\n    \"Tour\": [\n        \"Guernica\",\n        \"The Birth of Venus\",\n        \"The Scream\",\n        \"The Great Wave off Kanagawa\",\n        \"The Persistence of Memory\",\n        \"The Last Judgment\",\n        \"The Creation of Adam\",\n        \"The Starry Night\"\n    ],\n    \"TourID\": [\n        \"painting 015\",\n        \"painting 013\",\n        \"painting 014\",\n        \"painting 008\",\n        \"painting 010\",\n        \"painting 012\",\n        \"painting 011\",\n        \"painting 009\"\n    ]\n}";
            ProcessChatGPTResponse(chatGPTContent);
        }
    }

    public string GetCurrentLandmark()
    {
        return Landmark;
    }

    public void ProcessChatGPTResponse(string chatGPTContent)
    {
        Debug.Log("Process ChatGPT Response");

        if (!string.IsNullOrEmpty(chatGPTContent))
        {
            try
            {
                // Parse the response and extract camera control commands
                TourResponse tourResponse = JsonUtility.FromJson<TourResponse>(chatGPTContent);
                tourIDs = tourResponse.TourID;
                //string reasoning = tourResponse.Reasoning;
                string[] tours = tourResponse.Tour;

                Debug.Log("Start navigation");
                StartCoroutine(NavigationTour(tourIDs));
            }
            catch (System.Exception){  }

        }
    }

    // detect whether the response string is a json format
    public string ResponseJsonOrNot(string response)
    {
        try
        {
            // Try to deserialize the JSON string into a dummy C# object
            TourResponse tourResponse = JsonUtility.FromJson<TourResponse>(response);
            // If deserialization succeeds, it means the string is in valid JSON format
            return tourResponse.Introduction;
        }
        catch (System.Exception)
        {
            // If deserialization fails, it means the string is not in valid JSON format
            return response;
        }
    }

    private void PrintLandmarks(string[] tourIDs)
    {
        for (int i = 0; i < tourIDs.Length; i++)
        {
            Debug.Log(tourIDs[i]);
        }
    }

    private void ReorderLandmarks(string[] tourIDs)
    {
        // If there are no landmarks to visit, return
        if (tourIDs.Length <= 1)
            //yield break;
            return;

        // Create a new list to store the reordered IDs
        List<string> reorderedTourIDs = new List<string>();

        // Start from the camera's current position as the initial position
        Vector3 currentPosition = cameraTransform.position;

        // Find the closest landmark and add it to the reordered list
        while (tourIDs.Length > 0)
        {
            string closestLandmarkID = FindClosestLandmark(currentPosition);
            reorderedTourIDs.Add(closestLandmarkID);

            // Remove the selected landmark from the original array
            tourIDs = RemoveElementFromArray(tourIDs, closestLandmarkID);

            // Update the current position to the selected landmark's position
            currentPosition = GetPositionFromTourID(closestLandmarkID);

            //yield return null;
        }

        // Update the tourIDs with the reordered list
        tourIDs = reorderedTourIDs.ToArray();

        Debug.Log("Reorder finished");
        PrintLandmarks(tourIDs);
    }

    // Helper function to find the closest landmark to a given position
    private string FindClosestLandmark(Vector3 position)
    {
        string closestID = "";
        float closestDistance = float.MaxValue;

        foreach (string tourID in tourIDs)
        {
            Vector3 landmarkPosition = GetPositionFromTourID(tourID);
            float distance = Vector3.Distance(position, landmarkPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestID = tourID;
            }
        }

        return closestID;
    }

    // Helper function to remove an element from an array
    private T[] RemoveElementFromArray<T>(T[] array, T element)
    {
        List<T> list = new List<T>(array);
        list.Remove(element);
        return list.ToArray();
    }

    public void PlayButtonClicked()
    {
        Debug.Log("Replay the camera");
        cameraTransform.position = initalPosition;
        cameraTransform.rotation = initalRotation;

        StartCoroutine(NavigationTour(tourIDs));
    }

    private void UpdateTargetCamera(string tourID)
    {
        string cameraID = tourID.Replace("painting ", "Camera.");

        targetPosition = cameraRecorder.GetCameraPosition(cameraID);
        targetRotation = cameraRecorder.GetCameraOrientation(cameraID);
    }

    public Vector3 GetPositionFromTourID(string tourID)
    {
        string cameraID = tourID.Replace("painting ", "Camera.");
        return cameraRecorder.GetCameraPosition(cameraID);
    }

    private float waitTime = 1f;
    private float rotationSpeed = 30f;

    private string Landmark = "";

    private IEnumerator NavigationTour(string[] tourIDs)
    {

        for (int i = 0; i < tourIDs.Length; i++)
        {
            Landmark = tourIDs[i];
            Debug.Log(tourIDs[i]);
            // update camera target positions and orientations
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

    private IEnumerator NavigationSingle(string tourID)
    {
        Landmark = tourID;
        UpdateTargetCamera(tourID);
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

        yield return null;
    }
}