using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Range : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyBullet;
    public GameObject spawnPoint;
    public CharacterController controller;
    public Vector3 moveDirection;
    public ParticleSystem ps;
    public MeshRenderer mR;
    public Transform[] movePoints;
    public Vector3 velocity;

    public float bulletInterval;
    private float bulletTime;
    public float bulletSpeed;
    public Vector3 playerDirection;

    [Header("Stats")]
    public float moveSpeed;
    public float visionRange;
    public float visionAngle;
    public float shootRange;
    private int indexOfTarget;
    private Vector3 targetPosition;

    public State state = State.Patrol;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        ps = GameObject.Find("Sparks").GetComponent<ParticleSystem>();
        mR = GetComponent<MeshRenderer>();
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
    }

    // Update is called once per frame
    private void Update()
    {
        moveDirection = player.transform.position - controller.transform.position;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;
        playerDirection = (player.transform.position - transform.position).normalized;

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Follow:
                Follow();
                break;
            case State.Shoot:
                Shoot();
                break;
        }
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
        LookAtTarget();
        if (CanSeePlayer() && GetDistanceToPlayer() > shootRange)
        {
            state = State.Follow;
        }
        if (CanSeePlayer() && GetDistanceToPlayer() < shootRange)
        {
            state = State.Shoot;
        }

        if (agent.remainingDistance < agent.radius)
        {
            NextTarget();
        }
        agent.SetDestination(targetPosition);
    }


    void Follow()
    {
        LookAtPlayer();
        if (!CanSeePlayer() || (transform.position - player.transform.position).magnitude > visionRange)
        {
            state = State.Patrol;
        }
        if (CanSeePlayer() && GetDistanceToPlayer() < shootRange)
        {
            state = State.Shoot;
        }
        if (CanSeePlayer() && GetDistanceToPlayer() > shootRange)
        {
            //LookAtPlayer();
            velocity = player.transform.position - transform.position;
            velocity.Normalize();
            velocity *= moveSpeed * Time.deltaTime;
            controller.Move(velocity);
        }
    }


    void Shoot()
    {
        if (CanSeePlayer() && GetDistanceToPlayer() > shootRange)
        {
            state = State.Follow;
        }

        agent.SetDestination(transform.position);
        velocity = Vector3.zero;

        LookAtPlayer();
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = bulletInterval;
            
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(playerDirection * bulletSpeed, ForceMode.Impulse);
        Destroy(bulletObj, 5f);
    }


    public enum State
    {
        Patrol,
        Follow,
        Shoot
    }


}
