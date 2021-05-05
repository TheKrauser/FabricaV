using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public bool quit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !quit)
        {
            AudioManager.Instance.PlaySoundEffect("Balloon");
            Destroy(gameObject);
        }
        else if (quit)
            Application.Quit();
    }
}
