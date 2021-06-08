using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class House_Talk : MonoBehaviour
{
    private int positionInArray;
    private bool canSkipDialogue;
    private bool xKeyPressed;

    public PlayableDirector scene;

    public TextMeshProUGUI textGO;
    public Image character;
    public GameObject majorDialogueGO;
    public TextVars[] text;
    [SerializeField] private bool playOnAwake;
    [SerializeField] private bool destroyOnEnd;

    [SerializeField] private bool cutscene;

    void Start()
    {
        if (playOnAwake)
            StartCoroutine(DisplayText());
        else
            return;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canSkipDialogue)
            {
                ShowNextSentence();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
            xKeyPressed = true;
    }

    public IEnumerator DisplayText()
    {
        //Debug.Log(positionInArray);
        if (scene != null)
            scene.playableGraph.GetRootPlayable(0).SetSpeed(0);

        majorDialogueGO.SetActive(true);
        canSkipDialogue = false;
        xKeyPressed = false;
        string v = "";

        foreach (char c in text[positionInArray].sentence)
        {
            v += c.ToString();
            textGO.text = v;
            textGO.fontSize = text[positionInArray].textSize;
            textGO.color = text[positionInArray].textColor;
            AudioManager.Instance.PlaySoundEffect("Dialogue");
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
    }

    private void ShowNextSentence()
    {
        if (positionInArray < text.Length)
            StartCoroutine(DisplayText());

        else
        {
            positionInArray = 0;
            this.enabled = false;
            majorDialogueGO.SetActive(false);

            if (scene != null)
                scene.playableGraph.GetRootPlayable(0).SetSpeed(1);

            if (!cutscene)
            {
                //var fp = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonAIO>();
            }
        }
    }
}


[Serializable]
public struct TextVars
{
    [TextArea(3, 20)]
    public string sentence;
    public float textSpeed, delayToNext, textSize;
    public Sprite imageCharacter;
    public Color textColor;
}
