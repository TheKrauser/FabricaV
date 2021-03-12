using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarefaVector3 : MonoBehaviour
{
    public State initialState;
    private State state;
    public Transform target;
    public List<Transform> targets = new List<Transform>();
    private int point = 0;
    bool reverse = false;

    public enum State
    {
        PERSEGUICAO,
        CONTINUO,
        GRUPO,
        UNIFORME,
    }

    void Start()
    {
        state = initialState;

        switch (state)
        {
            case State.PERSEGUICAO:
                break;

            case State.CONTINUO:
                target = targets[point];
                break;

            case State.GRUPO:
                break;

            case State.UNIFORME:
                break;

        }
    }

    void Update()
    {
        switch(state)
        {
            case State.PERSEGUICAO:
                transform.position += (target.position - transform.position).normalized * 3f * Time.deltaTime;
                break;

            case State.CONTINUO:
                if (Vector3.Distance(transform.position, target.position) > 0.2f)
                    transform.position += (target.position - transform.position).normalized * 8f * Time.deltaTime;
                else
                    ChangePatrolPoint();
                break;

            case State.GRUPO:
                if (Vector3.Distance(transform.position, target.position) > 0.2f)
                    transform.position += (target.position - transform.position).normalized * 3f * Time.deltaTime;
                else
                    return;
                break;

            case State.UNIFORME:
                if (Vector3.Distance(transform.position, target.position) > 0.2f)
                    transform.position += (target.position - transform.position).normalized * 3f * Time.deltaTime;
                break;

        }
    }

    void ChangePatrolPoint()
    {
        if (!reverse)
            point++;
        else
            point--;

        if (point >= targets.Count)
        {
            point = (targets.Count - 1);
            reverse = true;
        }
        else if (point < 0)
        {
            point = 0;
            reverse = false;
        }

        target = targets[point];
    }
}
