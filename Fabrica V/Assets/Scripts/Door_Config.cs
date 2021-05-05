using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Config : MonoBehaviour
{
    private Animator anim;

    private AudioSource audioS;
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;

    private bool door;

    [SerializeField] private bool flip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        anim.SetBool("Flip", flip);
    }

    public void OpenDoor()
    {
        door = anim.GetBool("Door");
        audioS.PlayOneShot(openDoor);
        anim.SetBool("Door", !door);
        if (!anim.GetBool("Interacted"))
            anim.SetBool("Interacted", true);
    }

    public void CloseDoor()
    {
        door = anim.GetBool("Door");
        audioS.PlayOneShot(closeDoor);
        anim.SetBool("Door", !door);
        if (!anim.GetBool("Interacted"))
            anim.SetBool("Interacted", true);
    }
}
