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
        AI_Activity.Parameters idleParams, 
        AI_Activity.Parameters catchingParams 
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

    public void Reset () 
    {
        SetActivity(idle); 
    }

    public void OnDestroy () 
    {
        idle.OnDestroy(); 
        catching.OnDestroy(); 
    }
    




    //  Info  ------------------------------------------------------- 
    public Platform Platform => npc.Platform; 
    public NPC NPC => npc; 
    public Square Square => npc.Square; 
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
        SquarePathExplorer pathExplorer = new SquarePathExplorer(Square); 
        return pathExplorer.IsGoingToSide(Platform.Side); 
    }

    bool CanCatchNow () 
    {
        SquarePathExplorer pathExplorer = new SquarePathExplorer(Square); 
        
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
