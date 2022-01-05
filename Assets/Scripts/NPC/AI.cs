using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class AI 
{
    // connections 
    NPC npc; 
    // data 
    AI_Activity activity; 
    AI_Idle idle; 
    AI_Catch catching; 


    public AI (
        NPC npc, 
        AI_Idle_Params idleParams, 
        AI_Catching_Params catchingParams 
    ) {
        this.npc = npc; 

        idle      = new AI_Idle  (this, idleParams); 
        catching  = new AI_Catch (this, catchingParams); 
        SetActivity(idle); 
    }

    public void Update () 
    {
        UpdateState(); 
        activity.Update(); 
    }

    public void UpdateParameters (
        AI_Idle_Params idleParams, 
        AI_Catching_Params catchingParams 
    ) {
        idle.UpdateParameters(idleParams); 
        catching.UpdateParameters(catchingParams); 
    }

    public void Reset () 
    {
        SetActivity(idle); 
    }
    




    //  Info  ------------------------------------------------------- 
    public Platform Platform => npc.Platform; 
    public NPC NPC => npc; 
    public Square Square => npc.Square; 
    public World World => npc.World; 
    public float Time => npc.Square.Distance; 





    //  State  ------------------------------------------------------ 
    void UpdateState () 
    {
        if (CanCatchNow()) 
        {
            SetActivity(catching); 
        } 
        else 
        {
            SetActivity(idle); 
        }
    }

    bool IsSquareGoingToMe () 
    {
        SquarePathExplorer pathExplorer = new SquarePathExplorer(Square, World); 
        return pathExplorer.IsGoingToSide(Platform.Side); 
    }

    bool CanCatchNow () 
    {
        SquarePathExplorer pathExplorer = new SquarePathExplorer(Square, World); 
        
        if (pathExplorer.IsLookingAtLineInX(Platform.Position.x)) 
        {
            return true; 
        }

        pathExplorer.MoveNext(); 
        return pathExplorer.IsLookingAtLineInX(Platform.Position.x); 
    }





    //  Activities  -------------------------------------------------     
    void SetActivity (AI_Activity newActivity) 
    {
        if (activity == newActivity) return; 

        activity?.Stop(); 
        activity = newActivity; 
        activity.Start(); 
    }

}
