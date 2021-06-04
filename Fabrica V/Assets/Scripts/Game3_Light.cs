using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Game3_Light : MonoBehaviour
{
    private AIDestinationSetter destinationSetter;
    private AIPath path;
    private Seeker seeker;
    private Patrol patrol;
    private Transform target;

    private Animator anim;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        path = GetComponent<AIPath>();
        patrol = GetComponent<Patrol>();

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (patrol.switchTime == float.PositiveInfinity)
        {
            anim.SetBool("isWalking", true);
        }
        else
            anim.SetBool("isWalking", false);

        Vector3 value = patrol.targets[patrol.index].transform.position - transform.position;
        if (value.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
