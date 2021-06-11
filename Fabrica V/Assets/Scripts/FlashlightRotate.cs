using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightRotate : MonoBehaviour
{
    private Camera cam;

    private Transform aimTransform;

    public bool shootDirection;

    void Start()
    {
        cam = Camera.main;

        aimTransform = transform.Find("Rotate");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Debug.Log(mousePosition);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle + 90);

        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
    }

    public Vector3 GetMouseWorldPosition()
    {
        //Screen to World Point para converter de Posição na Tela para Posição na Cena;

        //Armazenar a posição em um Vector3
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //Setar o eixo Z em 0f pois a Câmera principal está posicionada na posição Z = -10f;
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
