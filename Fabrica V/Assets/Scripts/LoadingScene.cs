using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;

public class LoadingScene : MonoBehaviour
{
    private AsyncOperation operation;
    public GameObject canvas;
    public Transform blackScreen;

    public static LoadingScene Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(BeginLoad(sceneName));
    }

    private IEnumerator BeginLoad(string sceneName)
    {
        Time.timeScale = 0f;
        var bg = blackScreen.GetComponent<CanvasGroup>();
        bg.DOFade(1, 2f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(3f);
        canvas.gameObject.SetActive(true);

        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }

        Time.timeScale = 0f;
        operation = null;
        yield return new WaitForSecondsRealtime(2f);
        canvas.gameObject.SetActive(false);

        bg.DOFade(0, 4f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
    }

    public IEnumerator FadeCutscene(float time, PlayableDirector director)
    {
        //Time.timeScale = 0f;
        var bg = blackScreen.GetComponent<CanvasGroup>();
        bg.DOFade(1, 2f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(2f);
        canvas.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        bg.DOFade(0, 1f).SetUpdate(true);
        canvas.gameObject.SetActive(false);
        director.Play();
    }
}
