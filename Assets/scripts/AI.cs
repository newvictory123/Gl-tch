using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    /*
    [Header("Vision and Hearing")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform player_pos;
    [SerializeField] private float visionRange;
    [SerializeField] private float visionConeAngle;
    [SerializeField] private float hearingRange;
    [SerializeField] private float attackRange;
    [SerializeField] private TextMeshProUGUI stateIndicator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Vector2 Direction;

    [Header("Movement")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float ChaseDistance;
    [SerializeField] public bool Moving;

    
    [Header("Stuff")]
    private CharacterController controller;
    [SerializeField] private PlayerMechanics playerMechanics;
    [SerializeField] private bool playerIsAttacking;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Image HealthBar;
    [SerializeField] private TextMeshProUGUI runes;
    [SerializeField] private float runesAmount;
    

    private int indexOfTarget;
    private Vector3 targetPosition;

    NavMeshAgent agent;

    private bool playerIsMoving = false;

    private State state = State.Patrol;

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
        if (GetDistanceToPlayer() < visionRange && !SightLineObstructed() && GetAngleToPlayer() < visionConeAngle)
        {
            return true;
        }
        return false;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        indexOfTarget = -1;
        NextTarget();
        LookAtTarget();
        HealthBar.fillAmount = 1;
        currentHealth = maxHealth;
        runesAmount = 0;
        Moving = false;
    }


    // Update is called once per frame
    void Update()
    {
        Direction = playerMovement.Direction;
        playerIsAttacking = Input.GetMouseButton(0);
        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
        if (Direction.magnitude >= 0.1f)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
        if (currentHealth <= 0f | HealthBar.fillAmount <= 0f)
        {
            runesAmount += 1f;
            Destroy(gameObject);
            playerMechanics.runesAmount = runesAmount;

        }
        runesAmount = playerMechanics.runesAmount;

    }

    void NextTarget()
    {
        indexOfTarget = (indexOfTarget + 1) % points.Length;
        targetPosition = points[indexOfTarget].position;
        targetPosition.y = transform.position.y;
    }
    void LookAtPlayer()
    {
        Vector3 lookAtPlayer = player_pos.position;
        lookAtPlayer.y = transform.position.y;

        Vector3 lookToPlayerDirection = (lookAtPlayer - transform.position).normalized;
        transform.forward = lookToPlayerDirection;
    }
    void Patrol()
    {
        Moving = true;
        if (CanSeePlayer())
        {
            state = State.Chase;
        }

        stateIndicator.text = "Patrolling...";

        if (agent.remainingDistance < agent.radius)
        {
            NextTarget();
        }
        agent.SetDestination(targetPosition);
    }

    void LookAtTarget()
    {
        Vector3 lookAt = targetPosition;
        lookAt.y = transform.position.y;

        Vector3 lookDir = (lookAt - transform.position).normalized;
        transform.forward = lookDir;
    }

    void Chase()
    {
        if (!CanSeePlayer())
        {
            state = State.Patrol;
        }

        stateIndicator.text = "Chase!";
        LookAtPlayer();
        Vector3 velocity = player_pos.position - transform.position;
        velocity.Normalize();
        velocity *= moveSpeed * Time.deltaTime;
        Moving = true;
        controller.Move(velocity);
    }

    
    enum State
    {
        Patrol,
        Chase,
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player Weapon" && playerIsAttacking)
        {
            currentHealth -= 0.5f;
            HealthBar.fillAmount = currentHealth / 100f;
        }
    }
    */
}
