using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class AI_Activity 
{
    // connections 
    protected AI ai; 
    // data 
    UpdateTimer startTimer; 
    UpdateTimer updateTimer; 
    bool hasStarted; 



    public AI_Activity (
        AI ai, 
        float startDelay, 
        float startVariance, 
        float updateInterval, 
        float updateVariance 
    ) {
        this.ai = ai; 

        startTimer = new UpdateTimer(OnStart, startDelay, startVariance); 
        updateTimer = new UpdateTimer(OnUpdate, updateInterval, updateVariance); 
    }

    protected void UpdateParameters (
        float startDelay, 
        float startVariance, 
        float updateInterval, 
        float updateVariance 
    ) {
        startTimer.UpdateParameters(startDelay, startVariance); 
        updateTimer.UpdateParameters(updateInterval, updateVariance); 
    }





    //  Info  ------------------------------------------------------- 
    protected Platform Platform => ai.Platform; 
    protected NPC NPC => ai.NPC; 
    protected Square Square => ai.Square; 
    protected World World => ai.World; 





    //  Life cycle  ------------------------------------------------- 
    public void Start () 
    {
        startTimer.Reset(ai.Time); 
        updateTimer.Reset(); 

        hasStarted = false; 
    }

    public void Update () 
    {
        if (!hasStarted) 
        {
            hasStarted = startTimer.UpdateIfNeeded(ai.Time); 
            if (!hasStarted) return; 
        }

        updateTimer.UpdateIfNeeded(ai.Time); 
    }

    public void Stop () 
    {
        OnStop(); 
    }

    protected virtual void OnStart () {} 

    protected abstract void OnUpdate (); 

    protected virtual void OnStop () {} 


}
