using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Game3_Spider : MonoBehaviour
{
    //[Header("A* Pathfinding")]
    private AIDestinationSetter destinationSetter;
    private AIPath path;
    private Seeker seeker;
    private Patrol patrol;
    private Transform target;

    [Header("Barra de Vida")]
    [SerializeField] private Image healthBar;
    [SerializeField] private float health;
    private bool takeDamage = false;

    [Header("Pontos de Patrulha")]
    [SerializeField] private Transform[] patrolPoints;
    private int currentPoint = 0;

    private bool lightStun = false;

    private State state;
    [SerializeField] private State initialState;
    public enum State
    {
        PATROL,
        ATTACK,
        HORDE
    }

    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        path = GetComponent<AIPath>();
        patrol = GetComponent<Patrol>();

        state = initialState;

        switch (state)
        {
            case State.ATTACK:
                break;

            case State.PATROL:
                target = GameObject.FindGameObjectWithTag("Player").transform;
                break;

            case State.HORDE:
                healthBar.fillAmount = health / 100;

                target = GameObject.FindGameObjectWithTag("Player").transform;
                destinationSetter.target = target;
                path.maxSpeed = Random.Range(1, 1.5f);
                break;
        }
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, target.position) < 5f && !lightStun)
        {
            ChangeState(State.ATTACK);
        }
        else if (Vector3.Distance(transform.position, target.position) < 5f || lightStun)
        {
            ChangeState(State.PATROL);
        }

        switch (state)
        {
            case State.ATTACK:
                Chase();
                break;

            case State.PATROL:
                Patrol();
                break;

            case State.HORDE:
                if (!takeDamage)
                {
                    if (health < 100)
                    {
                        health += 3f;
                        healthBar.fillAmount = health / 100;
                    }
                    if (health > 100)
                    {
                        health = 100;
                        healthBar.fillAmount = health / 100;
                    }
                }

                if (health <= 0)
                {
                    health = 0;
                    Destroy(gameObject);
                }
                break;
        }
    }

    void ChangeState(State stateS)
    {
        state = stateS;

        switch (state)
        {
            case State.ATTACK:
                break;

            case State.PATROL:
                break;

            case State.HORDE:
                break;
        }
    }

    private void Patrol()
    {
        destinationSetter.enabled = false;
        patrol.enabled = true;
    }

    private void Chase()
    {
        destinationSetter.enabled = true;
        patrol.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            lightStun = true;
            StartCoroutine(ResetStun());
            ChangeState(State.PATROL);
        }
    }

    private IEnumerator ResetStun()
    {
        yield return new WaitForSeconds(8);
        lightStun = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight"))
        {
            takeDamage = true;
            health -= 5;
            healthBar.fillAmount = health / 100;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight"))
        {
            takeDamage = false;
        }
    }
}
