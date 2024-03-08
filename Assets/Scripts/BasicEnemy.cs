using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BasicEnemy : MonoBehaviour
{
    //state Enumerator
    public enum EnemyState
    {
        patrolling,
        chasing,
        searching,
        attacking,
        retreating
    }

    //variables
    [SerializeField] private EnemyState currentState;

    [Header("Navigation")]
    [SerializeField] private NavMeshAgent agent;

    [Header("Patroling")]
    [SerializeField] private List<Transform> patrolPoints;
    [SerializeField] private float patrolSpeed;
    private Transform targetPoint;
    private int patrolIndex;

    [Header("State Indication")]
    [SerializeField] private Material enemyMT;
    [SerializeField] private Color32 patrolling = new Color32(0, 128, 0, 255);
    [SerializeField] private Color32 chasing = new Color32(255, 102, 0, 255);
    [SerializeField] private Color32 searching = new Color32(255, 204, 0, 255);
    [SerializeField] private Color32 attacking = new Color32(217, 33, 33, 255);
    [SerializeField] private Color32 retreating = new Color32(0, 171, 240, 255);

    //Called on first frame of the scene even if the game object is not active
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyMT = GetComponent<MeshRenderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        //setting up enemy state
        currentState = EnemyState.patrolling;
        EnterPatrollingState();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    //State changing section

    //patrolling state
    private void EnterPatrollingState()
    {
        //sets color to the patrolling color
        enemyMT.color = patrolling;

        //sets the speed of the enemy to patrol speed
        agent.speed = patrolSpeed;

        if (patrolPoints.Count > 0)
        {
            //sets to first patrol point
            patrolIndex = 0;
            targetPoint = patrolPoints[patrolIndex];
        }
    }

    private void UpdatePatrollingState()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //iterate index
            patrolIndex++;

            //check bounderies
            if (patrolIndex >= patrolPoints.Count)
            {
                patrolIndex = 0;
            }
        }

        //checks if needs to start new path
        if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(patrolPoints[patrolIndex].position);
        }
    }

    private void ExitPatrollingState()
    {

    }

    //chasing state
    private void EnterChasingState()
    {
        //sets color to the chasing color
        enemyMT.color = chasing;
    }

    private void UpdateChasingState()
    {

    }

    private void ExitChasingState()
    {

    }

    //searching state
    private void EnterSearchingState()
    {
        //sets color to the chasing color
        enemyMT.color = searching;
    }

    private void UpdateSearchingState()
    {

    }

    private void ExitSearchingState()
    {

    }

    //attacking state
    private void EnterAttackingState()
    {
        //sets color to the attacking color
        enemyMT.color = attacking;
    }

    private void UpdateAttackingState()
    {

    }

    private void ExitAttackingState()
    {

    }

    //retreating state
    private void EnterRetreatingState()
    {
        enemyMT.color = retreating;
    }

    private void UpdateRetreatingState()
    {

    }

    private void ExitRetreatingState()
    {

    }

    //State Handling

    /// <summary>
    /// Method that swtiches the enemy's state to the desired state
    /// </summary>
    /// <param name="state">the desired state of the enemy</param>
    private void SwitchState(EnemyState state)
    {
        //exit curreent state
        switch (currentState)
        {
            case EnemyState.patrolling:
                ExitPatrollingState();
                break;

            case EnemyState.chasing:
                ExitChasingState();
                break;

            case EnemyState.searching: 
                ExitSearchingState();
                break;

            case EnemyState.attacking: 
                ExitAttackingState();
                break;

            case EnemyState.retreating:
                ExitRetreatingState();
                break;
        }

        //enter next state
        switch (state)
        {
            case EnemyState.patrolling:
                EnterPatrollingState();
                break;

            case EnemyState.chasing:
                EnterChasingState();
                break;

            case EnemyState.searching:
                EnterSearchingState();
                break;

            case EnemyState.attacking:
                EnterAttackingState();
                break;

            case EnemyState.retreating:
                EnterRetreatingState();
                break;
        }

        //sets the enemy's state to the input state
        currentState = state;
    }

    /// <summary>
    /// Method that updates the enemy dependant on current state
    /// </summary>
    private void UpdateState()
    {
        //determines which logic to use for update
        switch (currentState)
        {
            case EnemyState.patrolling:
                UpdatePatrollingState();
                break;

            case EnemyState.chasing:
                UpdateChasingState();
                break;

            case EnemyState.searching:
                UpdateSearchingState(); 
                break;

            case EnemyState.attacking:
                UpdateAttackingState();
                break;

            case EnemyState.retreating:
                UpdateRetreatingState();
                break;
        }
    }


}
