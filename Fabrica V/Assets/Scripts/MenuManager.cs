using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider sliderGeral, sliderEfeitos, sliderMusica;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TextMeshProUGUI txtGeral, txtEfeitos, txtMusica;
    [SerializeField] private Toggle togSemSom;
    public bool semSom;


    [SerializeField] private TMP_Dropdown dropResolucao, dropQualidade;
    [SerializeField] private bool telaCheia;
    Resolution[] resolucao;

    [SerializeField] public Transform menu, opcoes, optAudio, optVideo, optControles;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        /*txtGeral.text = (sliderGeral.value * 100).ToString() + "%";
        txtEfeitos.text = (sliderEfeitos.value * 100).ToString() + "%";
        txtMusica.text = (sliderMusica.value * 100).ToString() + "%";*/

        UpdateVolumeEffects(sliderEfeitos.value);
        UpdateVolumeGeral(sliderGeral.value);
        UpdateVolumeMusic(sliderMusica.value);

        resolucao = Screen.resolutions;
        dropResolucao.ClearOptions();

        List<string> opcoesResolucao = new List<string>();

        int resolucaoAtual = 0;
        for(int i=0; i<resolucao.Length; i++)
        {
            string res = resolucao[i].width + "x" + resolucao[i].height;
            opcoesResolucao.Add(res);

            if (resolucao[i].width == Screen.currentResolution.width &&
            resolucao[i].height == Screen.currentResolution.height)
            {
                resolucaoAtual = i;
            }
        }

        dropResolucao.AddOptions(opcoesResolucao);
        dropResolucao.value = resolucaoAtual;
        dropResolucao.RefreshShownValue();
    }

    public void UpdateVolumeGeral(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10 (volume) * 20);
        float value = sliderGeral.value * 100;
        txtGeral.text = Mathf.RoundToInt(value).ToString() + "%";
    }

    public void UpdateVolumeEffects(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10 (volume) * 20);
        float value = sliderEfeitos.value * 100;
        txtEfeitos.text = Mathf.RoundToInt(value).ToString() + "%";
    }

    public void UpdateVolumeMusic(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10 (volume) * 20);
        float value = sliderMusica.value * 100;
        txtMusica.text = Mathf.RoundToInt(value).ToString() + "%";
    }

    public void MuteVolume(bool isMuted)
    {
        semSom = isMuted;
        if (isMuted)
        {
            //PlayerPrefs.SetInt("isMuted", 0);
            AudioListener.volume = 0;
        }
        else
        {
            //PlayerPrefs.SetInt("isMuted", 1);
            AudioListener.volume = 1;
        }

    }

    public  void UpdateQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void UpdateFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void UpdateResolution(int resolutionIndex)
    {
        Resolution resolution = resolucao[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void Options()
    {
        MenuTransitions.Instance.Transition(menu, opcoes);
    }
    public void BackOptions()
    {
        MenuTransitions.Instance.Transition(opcoes, menu);
    }

    public void TransitionOptions(Transform active)
    {
        Transform[] options = {optAudio, optControles, optVideo};

        for (int i=0; i<options.Length; i++)
        {
            options[i].gameObject.SetActive(false);
        }

        active.gameObject.SetActive(true);
    }

    public void SaveOptions()
    {

    }

    public void StartGame()
    {
        LoadingScene.Instance.LoadScene("Casa");
    }

    public void Credits()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
