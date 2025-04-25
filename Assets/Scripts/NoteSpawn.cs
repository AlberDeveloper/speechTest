
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteSpawn : MonoBehaviour
{
    public List<Note> notes;
    public SpeechRecognizer speechRecognizer;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            speechRecognizer.correctImage.SetActive(false);
            speechRecognizer.wrongImage.SetActive(false);
            var random = Random.Range(0, notes.Count);
            foreach (var note in notes)
            {
                note.gameObject.SetActive(false);
            }

            notes[random].gameObject.SetActive(true);
            speechRecognizer.Note = notes[random]; 
            yield return new WaitForSeconds(5);
        }
    }
}
