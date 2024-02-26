using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class Enemy_Melee : MonoBehaviour
{
    public GameObject player;
    public CharacterController controller;
    public Vector3 moveDirection;
    public ParticleSystem ps;
    public MeshRenderer mR;
    public Collider c;
    public Transform[] movePoints;
    public Animator animator;

    [Header("Stats")]
    public float moveSpeed;
    public float visionRange;
    public float visionAngle;
    public float deathRange;
    private int indexOfTarget;
    private Vector3 targetPosition;

    public float maxHealth;
    public float currentHealth;

    private State state = State.Patrol;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        //ps = GameObject.Find("Sparks").GetComponent<ParticleSystem>();
        ps = transform.Find("Sparks").GetComponent<ParticleSystem>();
        mR = GetComponent<MeshRenderer>();
        c = GetComponent<Collider>();
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
        currentHealth = maxHealth;

        animator.SetFloat("Movespeed", 1f);
    }

    // Update is called once per frame
    private void Update()
    {
        moveDirection = player.transform.position - controller.transform.position;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }


        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }


    }


    IEnumerator Exsplode()
    {
        animator.SetBool("IsExploding", true);

        //yield return new WaitForSeconds(1f);

        mR.enabled = false;
        c.enabled = false;

        yield return new WaitForSeconds(0.25f);

        ps.Play();

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

        yield return null;
    }



    void LookAtPlayer()
    {
        Vector3 lookAtPlayer = player.transform.position;
        lookAtPlayer.y = transform.position.y;

        Vector3 lookToPlayerDirection = (lookAtPlayer - transform.position).normalized;
        transform.forward = lookToPlayerDirection;
    }

    float GetDistanceToPlayer()
    {
        return
            (player.transform.position - transform.position)
            .magnitude;
    }

    float GetAngleToPlayer()
    {
        Vector3 directionToPlayer =
            (player.transform.position - transform.position)
            .normalized;
        return Vector3.Angle(transform.forward, directionToPlayer);
    }

    bool SightLineObstructed()
    {
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        Ray ray = new Ray(
            transform.position,
            vectorToPlayer);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, vectorToPlayer.magnitude))
        {
            GameObject obj = hitInfo.collider.gameObject;
            return obj != player;
        }
        return false;
    }

    bool CanSeePlayer()
    {
        if (GetDistanceToPlayer() < visionRange && !SightLineObstructed() && GetAngleToPlayer() < visionAngle)
        {
            return true;
        }
        return false;
    }

    void NextTarget()
    {
        indexOfTarget = (indexOfTarget + 1) % movePoints.Length;
        targetPosition = movePoints[indexOfTarget].position;
        targetPosition.y = transform.position.y;
    }

    void LookAtTarget()
    {
        Vector3 lookAt = targetPosition;
        lookAt.y = transform.position.y;

        Vector3 lookDir = (lookAt - transform.position).normalized;
        transform.forward = lookDir;
    }

    void Patrol()
    {
        if (CanSeePlayer())
        {
            state = State.Chase;
        }

        if (agent.remainingDistance < agent.radius)
        {
            NextTarget();
        }
        agent.SetDestination(targetPosition);
    }


    void Chase()
    {
        if (GetDistanceToPlayer() < deathRange)
        {
            StartCoroutine(Exsplode());
        }

        if (!CanSeePlayer())
        {
            state = State.Patrol;
        }

        LookAtPlayer();
        Vector3 velocity = player.transform.position - transform.position;
        velocity.y = 0f;
        velocity.Normalize();
        velocity *= moveSpeed * Time.deltaTime;
        controller.Move(velocity);

        if ((transform.position - player.transform.position).magnitude > visionRange)
        {
            state = State.Patrol;
            LookAtTarget();
        }
    }




    enum State
    {
        Patrol,
        Chase,
    }


}
