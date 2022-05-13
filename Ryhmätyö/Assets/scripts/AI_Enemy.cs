using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Transform player;
    [SerializeField]
    int Manualspeed;
    private Animator animator;
    Vector3 startPos;
    private float startspeed;
    private bool shooted = false;
    private float time = 0;
    private float speedUp;
    private int itemsToWin = GameVariables.intemsToWin;

    void Start()
    {

        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (Manualspeed > 0)
            agent.speed = Manualspeed;
        startspeed = agent.speed;
        speedUp = startspeed * 2 / (itemsToWin - 1); // eli kun jää vaan 1 itemi enää, susin nopeus on x3
    }

    void Update()
    {
        if (!shooted) {
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", 1);
            agent.speed = startspeed;
        } else {
            if (time > 10f) {
                time = 0;
                shooted = false;
                agent.speed = startspeed;
                return;
            } else {
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
        agent.speed = agent.speed + speedUp;
    }
}
