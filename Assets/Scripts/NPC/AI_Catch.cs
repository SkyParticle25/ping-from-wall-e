using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class AI_Catch : AI_Activity 
{
    // data 
    float catchPosition; 


    public AI_Catch (
        AI ai, 
        Parameters parameters 
    ) 
    : base (
        ai, 
        parameters 
    ) {
        
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
        SquarePathExplorer pathExplorer = new SquarePathExplorer(Square); 

        if (!pathExplorer.IsLookingAtLineInX(Platform.Position.x)) 
        {
            pathExplorer.MoveNext(); 
        }

        return pathExplorer.GetPathY(Platform.Position.x); 
    }

}
