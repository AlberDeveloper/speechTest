using System;
using System.Collections.Generic;
using System.Data;
using TMPro;
using static SpeechRecognizerPlugin;
using UnityEngine;

public class SpeechRecognizer : MonoBehaviour, ISpeechRecognizerPlugin
{
    private SpeechRecognizerPlugin plugin;
    private string[] result;
    [SerializeField] 
    private TextMeshProUGUI logs;
    
    private Note note;

    public Note Note
    {
        set => note = value;
    }


    public GameObject correctImage;
    public GameObject wrongImage;
    
    void Start()
    {
        plugin = GetPlatformPluginVersion(gameObject.name);
        plugin.StartListening(true, "es-ES");

    }

    public void OnResult(string recognizedResult)
    {
        char[] delimiterChars = { '~' };
        result = recognizedResult.Split(delimiterChars);
        logs.text = "";
        bool correct = false;
        
        if (result.Length > 0)
        {
            foreach (string text in result)
            {
                logs.text += text + '\n';
                if (String.Equals(text,note.noteName.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    correct = true;
                    break;
                }
            }

            correctImage.SetActive(correct);
            wrongImage.SetActive(!correct);

        }
    }

    public void OnError(string recognizedError)
    {
        ERROR error = (ERROR)int.Parse(recognizedError);
        switch (error)
        {
            case ERROR.UNKNOWN:
                Debug.Log("<b>ERROR: </b> Unknown");
                break;
            case ERROR.INVALID_LANGUAGE_FORMAT:
                Debug.Log("<b>ERROR: </b> Language format is not valid");
                break;
            default:
                Debug.Log("ERROR: " + recognizedError);
                break;
        }
    }
}
