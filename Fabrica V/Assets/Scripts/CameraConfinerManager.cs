using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraConfinerManager : MonoBehaviour
{
    [SerializeField] private Transform groundToEnable;
    [SerializeField] private Transform cameraToDisable;
    [SerializeField] private Transform cameraToEnable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerTrigger"))
        {
            cameraToDisable.gameObject.SetActive(false);
            cameraToEnable.gameObject.SetActive(true);
            groundToEnable.gameObject.SetActive(true);
        }
    }
}
