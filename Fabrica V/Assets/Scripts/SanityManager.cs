using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityManager : MonoBehaviour
{
    [SerializeField] private float sanity;
    [SerializeField] private float sanityMax = 100f;
    [SerializeField] private float fear;
    [SerializeField] private float fearMax = 100f;

    [SerializeField] private Image sanityBar;
    [SerializeField] private Image fearBar;
    [SerializeField] private Transform respawn;

    public bool outsideLight;
    private bool scared;

    void Start()
    {
        sanity = sanityMax;
        fear = 0;

        sanityBar.fillAmount = sanity;
        fearBar.fillAmount = fear;
    }

    void Update()
    {
        UpdateBars();

        if (outsideLight)
        {
            if (fear < 100)
            {
                fear += 1.0f;
                if (fear > 100)
                    fear = 100;
            }
            else
            {
                scared = true;
            }
        }

        if (!outsideLight)
        {
            scared = false;
            fear -= 0.8f;
            if (fear < 0)
                fear = 0;
        }
        
        if (scared)
        {
            sanity -= 0.2f;

            if (sanity <= 0)
                Respawn();
        }
    }

    public void UpdateBars()
    {
        sanityBar.fillAmount = sanity / sanityMax;
        fearBar.fillAmount = fear / fearMax;
    }
    public void LoseSanity()
    {
        sanity -= 30f;
    }

    public void GainSanity()
    {
        sanity += 30f;
    }

    private void Respawn()
    {
        transform.position = respawn.position;
        sanity = sanityMax;
        fear = 0;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Light"))
        {
            outsideLight = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Light"))
        {
            outsideLight = false;
        }
        else
            outsideLight = true;
    }
}
