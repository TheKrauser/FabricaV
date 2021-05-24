using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Slider sliderGeral, sliderEfeitos, sliderMusica;
    [SerializeField] private TextMeshProUGUI txtGeral, txtEfeitos, txtMusica;
    [SerializeField] private bool semSom;
    [SerializeField] private TMP_Dropdown dropResolucao, dropQualidade;
    [SerializeField] private bool telaCheia;



    void Start()
    {

    }
    void Update()
    {
        txtGeral.text = sliderGeral.value.ToString() + "%";
        txtEfeitos.text = sliderEfeitos.value.ToString() + "%";
        txtMusica.text = sliderMusica.value.ToString() + "%";
    }
    public void Options()
    {

    }
    public void BackOptions()
    {

    }
    public void SaveOptions()
    {

    }
    public void Credits()
    {

    }
    public void Quit()
    {
        Application.Quit();
    }
}
