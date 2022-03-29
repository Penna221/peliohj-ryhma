using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Transform player;
    private Animator animator;
    Vector3 startPos;
    [SerializeField]
    private float speed;
    private float startspeed;
    private bool shooted = false;
    private float time = 0;

    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        startspeed = agent.speed;
    }

    void Update()
    {
        if (!shooted)
        {
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", 1);
        } else
        {
            if (time > 5f)
            {
                time = 0;
                animator.SetFloat("Speed", 1);
                shooted = false;
                agent.speed = speed;
            } else
            {
                agent.speed = 0;
                animator.SetFloat("Speed", agent.speed);
            }
            time += Time.deltaTime;

        }
    }
    public void takeDamage()
    {
        //transform.position = startPos;
        shooted = true;
    }

    public void upSpeed()
    {
        agent.speed = agent.speed + startspeed;
        speed = agent.speed;
    }
}
