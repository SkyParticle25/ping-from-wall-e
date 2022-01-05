using System.Collections;
using System.Collections.Generic;
using UnityEngine; 





[System.Serializable] 
public struct AI_Idle_Params 
{
    public float startDelay; 
    public float startVariance; 
    public float moveInterval; 
    public float moveVariance; 
    public float maxMoveError; 
}





public class AI_Idle : AI_Activity 
{
    // parameters 
    float maxMoveError; 


    public AI_Idle (
        AI ai, 
        AI_Idle_Params parameters 
    ) 
    : base (
        ai, 
        parameters.startDelay, 
        parameters.startVariance, 
        parameters.moveInterval, 
        parameters.moveVariance 
    ) {
        maxMoveError = parameters.maxMoveError; 
    }

    public void UpdateParameters (AI_Idle_Params parameters) 
    {
        base.UpdateParameters(
            parameters.startDelay, 
            parameters.startVariance, 
            parameters.moveInterval, 
            parameters.moveVariance 
        ); 

        maxMoveError = parameters.maxMoveError; 
    }




    //  Life cycle  ------------------------------------------------- 
    protected override void OnUpdate () 
    {
        NPC.SetDestination(0, maxMoveError); 
    }
    
}
