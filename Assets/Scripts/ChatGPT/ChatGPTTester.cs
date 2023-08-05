using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI;

public class ChatGPTTester : MonoBehaviour
{
    [SerializeField] private Button askButton;
    [SerializeField] private InputField inputField;
    //[SerializeField] private TextMeshProUGUI chatGPTAnswer;

    [SerializeField] private string prompt;

    [SerializeField] private ScrollRect scroll;

    [SerializeField] private RectTransform sent;
    [SerializeField] private RectTransform received;
    public bool Voice = false;
    [SerializeField] private Text2Speech textToSpeech;



  private float height;

    private List<ChatMessage> messages = new List<ChatMessage>();

    public CameraController cameraController;

    public GameObject Model;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AppendMessage("I really interested in chinese art.", "user");
            AppendMessage("That's great! Chinese art has a rich history and is known for its unique styles and techniques.\n In our virtual museum, we have several Chinese paintings that you might find interesting. Here are a few of them:\r\n\r\n1. \"Section of Goddess of Luo River\": This painting depicts a beautiful landscape with mountains, rivers, and trees. It showcases the traditional Chinese ink painting style.\r\n\r\n2. \"Travelers among Mountains and Streams\": This painting portrays a group of travelers navigating through a mountainous landscape. It is a classic example of Chinese landscape painting.\r\n\r\nThese are just a few examples of the Chinese art we have in our museum. If you would like to explore more, I can guide you to these paintings in the virtual space.", "assistant");
        }
    }

    public void Execute(string input = "")
    {
        Debug.Log("Send the message");
        if (string.IsNullOrEmpty(input)) {
            prompt = inputField.text;
        } else {
            prompt = input;
        }
        AppendMessage(prompt, "user");

        askButton.enabled = false;
        inputField.text = "";
        inputField.enabled = false;

        // Transform the main camera position into the local transform of the scene gameobject
        string currentPosition = Model.transform.InverseTransformPoint(Camera.main.transform.position).ToString();
        string landmark = cameraController.GetCurrentLandmark();
        StartCoroutine(ChatGPTClient.Instance.Ask(prompt, currentPosition, landmark, (r) => ProcessResponse(r)));
    }

    public void ProcessResponse(ChatGPTResponse response)
    {
        //var chatGPTContent = response.Choices.FirstOrDefault()?.Message?.Content;
        var chatGPTContent = response.Content;

        if (!string.IsNullOrEmpty(chatGPTContent))
        {
            AppendMessage(chatGPTContent, "assistant");
            Debug.Log("Response in voice...");
            if (Voice) textToSpeech.MakeAudioRequest(chatGPTContent);
            cameraController.ProcessChatGPTResponse(chatGPTContent);
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

        askButton.enabled = true;
        inputField.enabled = true;
    }

    private void AppendMessage(string prompt, string role)
    {
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

        var item = Instantiate(role == "user" ? sent : received, scroll.content);
        item.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = cameraController.ResponseJsonOrNot(prompt); ;
        item.anchoredPosition = new Vector2(0, -height);
        LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        height += item.sizeDelta.y;
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;
    }

}
