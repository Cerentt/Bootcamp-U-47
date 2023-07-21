using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public void Initialise()
    {
        patrolState = new PatrolState();
        ChangeState(patrolState);
        //setup default state
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }     
        
    }

    public void ChangeState(BaseState newState)
    {
        //check the activestate !=null

        if (activeState != null)
        {
            activeState.Exit();
        }
        //change to a new statement
        activeState = newState;

        if (activeState != null)
        {
            //setup a newstate
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
  