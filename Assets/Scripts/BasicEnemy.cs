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
    private int patrolIndex;

    [Header("Chasing")]
    [SerializeField] private float chaseSpeed;

    [Header("Searching")]
    [SerializeField] private float searchSpeed;
    [SerializeField] private float searchRadius;
    [SerializeField] private Vector3 searchPos;

    [Header("Attacking")]
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;

    [Header("Player Detection")]
    [SerializeField] private Transform detectionOrigin;
    [SerializeField] private Transform lastKnownPosition;
    [SerializeField] private float detectionDuration;
    [SerializeField] private float searchDuration;
    [SerializeField] private float detectionDistance;
    [SerializeField] private bool playerDetected;
    [SerializeField] private bool searching;
    [SerializeField] private float detectionCountdown;
    [SerializeField]private float searchCountdown;


    [Header("State Indication")]
    [SerializeField] private Material enemyMT;
    [SerializeField] private Color32 patrollingColor = new Color32(0, 128, 0, 255);
    [SerializeField] private Color32 chasingColor = new Color32(255, 102, 0, 255);
    [SerializeField] private Color32 searchingColor = new Color32(255, 204, 0, 255);
    [SerializeField] private Color32 attackingColor = new Color32(217, 33, 33, 255);
    [SerializeField] private Color32 retreatingColor = new Color32(0, 171, 240, 255);

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
        UpdateDetection();
    }

    //State changing section

    //patrolling state
    private void EnterPatrollingState()
    {
        //sets color to the patrolling color
        enemyMT.color = patrollingColor;

        //sets NavMesh agent parameters
        agent.speed = patrolSpeed;
        agent.stoppingDistance = 0f;

        if (patrolPoints.Count > 0)
        {
            //sets to first patrol point
            patrolIndex = 0;
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

        if (playerDetected)
        {
            SwitchState(EnemyState.chasing);
        }
    }

    private void ExitPatrollingState()
    {

    }

    //chasing state
    private void EnterChasingState()
    {
        //sets color to the chasing color
        enemyMT.color = chasingColor;

        //sets NavMesh agent parameters
        agent.speed = chaseSpeed;
        agent.stoppingDistance = attackRange;
    }

    private void UpdateChasingState()
    {
        if (lastKnownPosition == null)
        {
            //update to retreating
        }

        //updates destination to last known position if they are not the same
        if (agent.destination != lastKnownPosition.position)
        {
            agent.SetDestination(lastKnownPosition.position);
        }

        //arrives at target location
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && Vector3.Magnitude(lastKnownPosition.position - transform.position) <= attackRange)
        {
            float attackRange = this.attackRange + gameObject.GetComponent<CapsuleCollider>().radius;

            //check if target is within attack radius
            if (Physics.CheckSphere(gameObject.transform.position, attackRange, playerLayer))
            {
                SwitchState(EnemyState.attacking);
            }
            //player not detected
            else{
                SwitchState(EnemyState.searching);
            }
        }

        //player detection timed out
        if (!playerDetected)
        {
            SwitchState(EnemyState.searching);
        }
    }

    private void ExitChasingState()
    {
        agent.stoppingDistance = 0f; 
    }

    //searching state
    private void EnterSearchingState()
    {
        //sets color to the chasing color
        enemyMT.color = searchingColor;

        //variables for search timer
        searchCountdown = searchDuration;
        searching = true;

        //sets NavMesh agent parameters
        agent.speed = searchSpeed;
        
    }

    private void UpdateSearchingState()
    {
        //generate new search position
        if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
        {    
            GenerateSearchPosition();
            agent.SetDestination(searchPos);
        }

        //searching times out
        if (!searching)
        {
            SwitchState(EnemyState.retreating);
        }

        if (playerDetected)
        {
            float attackRange = this.attackRange + gameObject.GetComponent<CapsuleCollider>().radius;

            //check if target is within attack radius
            if (Physics.CheckSphere(gameObject.transform.position, attackRange, playerLayer))
            {
                SwitchState(EnemyState.attacking);
            }
            else
            {
                SwitchState(EnemyState.chasing);
            }
        }
    }

    private void ExitSearchingState()
    {

    }

    //attacking state
    private void EnterAttackingState()
    {
        //sets color to the attacking color
        enemyMT.color = attackingColor;
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
        enemyMT.color = retreatingColor;
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

    /// <summary>
    /// Method that determines if the player is seen
    /// </summary>
    /// <param name="player">player object in detection collider</param>
    public bool DetectPlayer(Transform player)
    {
        //get direction of player
        Vector3 rayDirection = Vector3.Normalize(player.position - gameObject.transform.position);

        RaycastHit hit;
        Physics.Raycast(detectionOrigin.transform.position, rayDirection, out hit, detectionDistance);

        //check that player is not obstructed
        if (hit.transform.tag == "Player")
        {
            //register detection
            playerDetected = true;
            lastKnownPosition = player;
            detectionCountdown = detectionDuration;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Method that updates the detection bools of the enemy
    /// </summary>
    private void UpdateDetection()
    {
        if (playerDetected)
        {
            detectionCountdown -= Time.deltaTime;
            
            if (detectionCountdown <= 0)
            {
                playerDetected = false;
            }
        }
        else if (searching)
        {
            searchCountdown -= Time.deltaTime;

            if (searchCountdown <= 0)
            {
                searching = false;
            }
        }

    }

    /// <summary>
    /// Method that generates a new search position
    /// </summary>
    private void GenerateSearchPosition()
    {
        searchPos = lastKnownPosition.position;
        searchPos.x += Random.Range(-searchRadius, searchRadius);
        searchPos.z += Random.Range(-searchRadius, searchRadius);
    }
    
}
