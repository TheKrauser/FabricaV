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
    private Animator anim;

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
        anim = GetComponentInChildren<Animator>();

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
        switch (state)
        {
            case State.ATTACK:

                if (Vector3.Distance(transform.position, target.position) < 4f || lightStun)
                {
                    ChangeState(State.PATROL);
                }

                Chase();
                break;

            case State.PATROL:
                if (Vector3.Distance(transform.position, target.position) < 4f && !lightStun)
                {
                    ChangeState(State.ATTACK);
                }

                Patrol();
                break;

            case State.HORDE:
                anim.SetBool("isWalking", true);

                if (!takeDamage)
                {
                    if (health < 100 && health > 0)
                    {
                        health += 10 * Time.deltaTime;
                        healthBar.fillAmount = health / 100;
                    }
                    if (health > 100)
                    {
                        health = 100;
                        healthBar.fillAmount = health / 100;
                    }
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

        if (patrol.switchTime == float.PositiveInfinity)
        {
            anim.SetBool("isWalking", true);
        }
        else
            anim.SetBool("isWalking", false);
    }

    private void Chase()
    {
        destinationSetter.enabled = true;
        patrol.enabled = false;

        anim.SetBool("isWalking", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            lightStun = true;
            StartCoroutine(ResetStun());
            ChangeState(State.PATROL);
        }

        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<SanityManager>();
            if (player != null) player.LoseSanity();

            Application.Quit();
            Destroy(gameObject);
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
            health -= 10 * Time.deltaTime;
            healthBar.fillAmount = health / 100;


            if (health <= 0)
            {
                Destroy(gameObject);
            }
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
