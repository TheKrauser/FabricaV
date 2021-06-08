using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider sliderGeral, sliderEfeitos, sliderMusica;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TextMeshProUGUI txtGeral, txtEfeitos, txtMusica;
    [SerializeField] private Toggle togSemSom, togTelaCheia;
    public bool semSom;


    [SerializeField] private TMP_Dropdown dropResolucao, dropQualidade;
    [SerializeField] private bool telaCheia;
    Resolution[] resolucao;

    [SerializeField] public Transform menu, opcoes, optAudio, optVideo, optControles;

    private int telaCheiaInt, semSomInt;

    [SerializeField] private bool dontDestroy;

    void Awake()
    {
        if (dontDestroy)
            DontDestroyOnLoad(gameObject);

        telaCheiaInt = PlayerPrefs.GetInt("Fullscreen", 1);
        semSomInt = PlayerPrefs.GetInt("Muted", 0);

        if (telaCheiaInt == 1)
        {
            Screen.fullScreen = true;
            togTelaCheia.isOn = true;
        }
        else
        {
            Screen.fullScreen = false;
            togTelaCheia.isOn = false;
        }

        if (semSomInt == 1)
        {
            togSemSom.isOn = true;
            AudioListener.volume = 0;
        }
        else
        {
            togSemSom.isOn = false;
            AudioListener.volume = 1;
        }

        dropResolucao.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt("Resolution", dropResolucao.value);
            PlayerPrefs.Save();
        }));

        dropQualidade.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt("Quality", dropQualidade.value);
            PlayerPrefs.Save();
        }));
    }

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        /*txtGeral.text = (sliderGeral.value * 100).ToString() + "%";
        txtEfeitos.text = (sliderEfeitos.value * 100).ToString() + "%";
        txtMusica.text = (sliderMusica.value * 100).ToString() + "%";*/

        UpdateSounds();

        dropQualidade.value = PlayerPrefs.GetInt("Quality", 3);

        resolucao = Screen.resolutions;
        dropResolucao.ClearOptions();

        List<string> opcoesResolucao = new List<string>();

        int resolucaoAtual = 0;
        for (int i = 0; i < resolucao.Length; i++)
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
        dropResolucao.value = PlayerPrefs.GetInt("Resolution", resolucaoAtual);
        dropResolucao.RefreshShownValue();
    }

    public void UpdateVolumeGeral(float volume)
    {
        PlayerPrefs.SetFloat("VolumeGeral", volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("VolumeGeral")) * 20);
        float value = sliderGeral.value * 100;
        txtGeral.text = Mathf.RoundToInt(value).ToString() + "%";
    }

    public void UpdateVolumeEffects(float volume)
    {
        PlayerPrefs.SetFloat("VolumeEfeitos", volume);
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat("VolumeEfeitos")) * 20);
        float value = sliderEfeitos.value * 100;
        txtEfeitos.text = Mathf.RoundToInt(value).ToString() + "%";
    }

    public void UpdateVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat("VolumeMusica", volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("VolumeMusica")) * 20);
        float value = sliderMusica.value * 100;
        txtMusica.text = Mathf.RoundToInt(value).ToString() + "%";
    }

    public void UpdateSounds()
    {
        float efeitos = PlayerPrefs.GetFloat("VolumeEfeitos", 0.6f);
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(efeitos) * 20);
        sliderEfeitos.value = efeitos;
        txtEfeitos.text = Mathf.RoundToInt(sliderEfeitos.value * 100).ToString() + "%";

        float musica = PlayerPrefs.GetFloat("VolumeMusica", 0.6f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musica) * 20);
        sliderMusica.value = musica;
        txtMusica.text = Mathf.RoundToInt(sliderMusica.value * 100).ToString() + "%";

        float geral = PlayerPrefs.GetFloat("VolumeGeral", 0.6f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(geral) * 20);
        sliderGeral.value = geral;
        txtGeral.text = Mathf.RoundToInt(sliderGeral.value * 100).ToString() + "%";
    }

    public void MuteVolume(bool isMuted)
    {
        semSom = isMuted;

        if (isMuted)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Muted", 0);
        }
    }

    public void UpdateQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void UpdateFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (isFullscreen)
        {
            isFullscreen = true;
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            isFullscreen = false;
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }

    public void UpdateResolution(int resolutionIndex)
    {
        Resolution resolution = resolucao[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void Options()
    {
        MenuTransitions.Instance.Transition(menu, opcoes);
        audioMixer.SetFloat("EffectMusic", Mathf.Log10(PlayerPrefs.GetFloat("VolumeMusica")) * 20);
        float value = sliderMusica.value * 100;
        txtMusica.text = Mathf.RoundToInt(value).ToString() + "%";
    }
    public void BackOptions()
    {
        MenuTransitions.Instance.Transition(opcoes, menu);
    }

    public void TransitionOptions(Transform active)
    {
        Transform[] options = { optAudio, optControles, optVideo };

        for (int i = 0; i < options.Length; i++)
        {
            options[i].gameObject.SetActive(false);
        }

        active.gameObject.SetActive(true);
    }

    public void SaveOptions()
    {

    }

    public void LoadOptions()
    {

    }

    public void StartGame()
    {
        LoadingScene.Instance.LoadScene("Casa");
    }

    public void Credits()
    {

    }

    public void Reset()
    {
        PlayerPrefs.SetInt("HasSavedGame", 0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
