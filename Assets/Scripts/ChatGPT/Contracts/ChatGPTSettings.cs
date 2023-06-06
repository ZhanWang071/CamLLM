using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChatGPTSettings", menuName = "ChatGPT/ChatGPTSettings")]

public class ChatGPTSettings : ScriptableObject
{
	public string apiURL;

	public string apiKey;

	public bool debug;

	public string[] reminders;
}