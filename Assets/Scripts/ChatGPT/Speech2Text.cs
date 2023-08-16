using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OpenAI
{

    public class Speech2Text : MonoBehaviour
    {
        [SerializeField] private ChatGPTTester chatTest;

        private readonly string fileName = "output.wav";
        private readonly int duration = 5;
        private readonly int micro_ind = 2;

        private AudioClip clip;
        private OpenAIApi openai = new OpenAIApi("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", "org-DHVLkr1qbe0yGswiNoV5zi4G");

        private async void StartRecording()
        {
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                Debug.Log("Stop recording...");

                Microphone.End(null);
                byte[] data = SaveWav.Save(fileName, clip);

                var req = new CreateAudioTranscriptionsRequest
                {
                    FileData = new FileData() { Data = data, Name = "audio.wav" },
                    // File = Application.persistentDataPath + "/" + fileName,
                    Model = "whisper-1",
                    Language = "en"
                };
                var res = await openai.CreateAudioTranscription(req);

                chatTest.Execute(res.Text);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Start recording...");
                chatTest.ShowChatbox();

                Debug.Log("Choose microphone: " + Microphone.devices[micro_ind]);
                clip = Microphone.Start(Microphone.devices[micro_ind], false, duration, 44100);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            StartRecording();
        }
    }
}
