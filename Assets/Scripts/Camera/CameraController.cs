using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private static Vector3 targetPosition;
    private static Quaternion targetRotation;

    private void Start()
    {
        // Find the main camera GameObject
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        // Perform initial camera setup
        //mainCamera.transform.position = new Vector3(0f, 0f, -10f);
        //mainCamera.transform.rotation = Quaternion.identity;
        startPosition = mainCamera.transform.position;
        startRotation = mainCamera.transform.rotation;
        targetPosition = mainCamera.transform.position;
        targetRotation = mainCamera.transform.rotation;
    }

    private void Update()
    {
        if (mainCamera.transform.position != targetPosition || mainCamera.transform.rotation != targetRotation)
        { 
            StartCoroutine(MoveAndRotateCamera()); 
        }
        else
        {
            startPosition = mainCamera.transform.position;
            startRotation = mainCamera.transform.rotation;
        }
    }

    public static void ProcessChatGPTResponse(string chatGPTContent)
    {
        if (!string.IsNullOrEmpty(chatGPTContent))
        {
            // Example: Parse the response and extract camera control commands
            //bool shouldMoveForward = chatGPTContent.Contains("move forward");

            targetPosition = new Vector3(5f, 0f, 0f);
            targetRotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    private System.Collections.IEnumerator MoveAndRotateCamera()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Smoothly interpolate the camera position and rotation
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            mainCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }
    }
}