using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogueSystem : MonoBehaviour
{
    private int positionInArray;
    private bool canSkipDialogue;
    private bool xKeyPressed;

    public PlayableDirector scene;

    public TextMeshProUGUI textGO;
    public Image character;
    public TextVariables[] text;

    void Start()
    {
        StartCoroutine(DisplayText());
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (canSkipDialogue)
            {
                ShowNextSentence();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
            xKeyPressed = true;
    }

    private IEnumerator DisplayText()
    {
        scene.playableGraph.GetRootPlayable(0).SetSpeed(0);
        canSkipDialogue = false;
        xKeyPressed = false;
        string v = "";
        character.sprite = text[positionInArray].imageCharacter;

        foreach (char c in text[positionInArray].sentence)
        {
            v += c.ToString();
            textGO.text = v;
            textGO.fontSize = text[positionInArray].textSize;
            textGO.color = text[positionInArray].textColor;
            if (!xKeyPressed)
                yield return new WaitForSeconds(text[positionInArray].textSpeed / 60);
            else
            {
                v = text[positionInArray].sentence.Substring(0, text[positionInArray].sentence.Length - 1);
                //v = v.Substring(0, v.Length - 1);
            }
        }

        positionInArray++;
        canSkipDialogue = true;

        /*foreach (TextVariables t in text)
        {
            character.sprite = t.imageCharacter;
            string v = "";
            foreach (char c in t.sentence)
            {
                v += c.ToString();
                textGO.text = v;
                textGO.fontSize = t.textSize;
                textGO.color = t.textColor;
                yield return new WaitForSeconds(t.textSpeed / 60);
            }
            yield return new WaitForSeconds(t.delayToNext);
        }*/
    }

    private void ShowNextSentence()
    {
        if (positionInArray < text.Length)
            StartCoroutine(DisplayText());

        else
        {
            Destroy(gameObject);
            scene.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }
}

[Serializable]
public struct TextVariables
{
    [TextArea(3, 20)]
    public string sentence;
    public float textSpeed, delayToNext, textSize;
    public Sprite imageCharacter;
    public Color textColor;
}
