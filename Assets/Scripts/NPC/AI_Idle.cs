using System.Collections;
using System.Collections.Generic;
using UnityEngine; 





public class AI_Idle : AI_Activity 
{



    public AI_Idle (
        AI ai, 
        Parameters ps 
    ) 
    : base (ai, ps) {
        
    }





    //  Life cycle  ------------------------------------------------- 
    protected override void OnUpdate () 
    {
        NPC.SetDestination(0, maxMoveError); 
    }
    
}
