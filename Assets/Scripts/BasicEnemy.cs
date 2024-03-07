using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BasicEnemy : MonoBehaviour
{
    //state Enumerator
    private enum EnemyState
    {
        patrolling,
        chasing,
        searching,
        attacking,
        retreating
    }

    //variables
    private EnemyState currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //State changing section

    //patrolling state
    private void EnterPatrollingState()
    {

    }

    private void UpdatePatrollingState()
    {

    }

    private void ExitPatrollingState()
    {

    }

    //chasing state
    private void EnterChasingState()
    {

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
