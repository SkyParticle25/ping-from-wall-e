using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[System.Serializable] 
public struct AI_Catching_Params 
{
    public float startDelay; 
    public float startVariance; 
    public float moveInterval; 
    public float moveVariance; 
    public float maxMoveError; 
}





public class AI_Catch : AI_Activity 
{
    // parameters 
    float maxMoveError; 
    // data 
    float catchPosition; 


    public AI_Catch (
        AI ai, 
        AI_Catching_Params parameters 
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

    public void UpdateParameters (AI_Catching_Params parameters) 
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
    protected override void OnStart () 
    {
        catchPosition = FindCatchPosition(); 
    }

    protected override void OnUpdate () 
    {
        NPC.SetDestination(catchPosition, maxMoveError); 
    }





    //  Info  ------------------------------------------------------- 
    float FindCatchPosition () 
    {
        SquarePathExplorer pathExplorer = new SquarePathExplorer(Square, World); 

        if (!pathExplorer.IsLookingAtLineInX(Platform.Position.x)) 
        {
            pathExplorer.MoveNext(); 
        }

        return pathExplorer.GetPathY(Platform.Position.x); 
    }

}
