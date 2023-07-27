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
    [SerializeField] private Text2Speech textToSpeech;


  private float height;

    private List<ChatMessage> messages = new List<ChatMessage>();

    public CameraController cameraController;

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

        StartCoroutine(ChatGPTClient.Instance.Ask(prompt, (r) => ProcessResponse(r)));
    }

    public void ProcessResponse(ChatGPTResponse response)
    {
        var chatGPTContent = response.Choices.FirstOrDefault()?.Message?.Content;

        if (!string.IsNullOrEmpty(chatGPTContent))
        {
            AppendMessage(chatGPTContent, "ChatGPT");
            Debug.Log("Response in voice...");
            textToSpeech.MakeAudioRequest(chatGPTContent);
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
        item.GetChild(0).GetChild(0).GetComponent<Text>().text = prompt;
        item.anchoredPosition = new Vector2(0, -height);
        LayoutRebuilder.ForceRebuildLayoutImmediate(item);
        height += item.sizeDelta.y;
        scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        scroll.verticalNormalizedPosition = 0;
    }

}
