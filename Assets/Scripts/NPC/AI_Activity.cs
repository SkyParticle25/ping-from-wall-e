using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class AI_Activity 
{
    // constants 
    const float MAX_POSSIBLE_ERROR = 2; 

    // classes 
    [System.Serializable] 
    public class Parameters 
    {
        public float startDelay; 
        public float startVariance; 
        public float updateInterval; 
        public float updateVariance; 
    }

    // parameters 
    Parameters ps; 
    protected float maxMoveError; 
    // connections 
    protected AI ai; 
    // data 
    UpdateTimer startTimer; 
    UpdateTimer updateTimer; 
    bool hasStarted; 



    public AI_Activity (
        AI ai, 
        Parameters ps 
    ) {
        this.ai = ai; 
        this.ps = ps; 

        startTimer = new UpdateTimer(
            OnStart, 
            ps.startDelay, 
            ps.startVariance
        ); 
        updateTimer = new UpdateTimer(
            OnUpdate, 
            ps.updateInterval, 
            ps.updateVariance
        ); 

        InitEvents(); 
        SetMaxMoveError(GameSettings.aiAccuracy); 
    }

    public void OnDestroy () 
    {
        ClearEvents(); 
    }

    #if UNITY_EDITOR 
    void UpdateParameters () 
    {
        startTimer.UpdateParameters(
            ps.startDelay, 
            ps.startVariance
        ); 
        updateTimer.UpdateParameters(
            ps.updateInterval, 
            ps.updateVariance
        ); 
    }
    #endif 





    //  Events  ----------------------------------------------------- 
    void InitEvents () 
    {
        GameSettings.onChanged += OnSettingsChanged; 
    }

    void ClearEvents () 
    {
        GameSettings.onChanged -= OnSettingsChanged; 
    }

    public void OnSettingsChanged () 
    {
        SetMaxMoveError(GameSettings.aiAccuracy); 
    }





    //  Info  ------------------------------------------------------- 
    protected Platform Platform => ai.Platform; 
    protected NPC NPC => ai.NPC; 
    protected Square Square => ai.Square; 





    //  Settings  --------------------------------------------------- 
    void SetMaxMoveError (float accuracy) 
    {
        maxMoveError = 0.9f + (MAX_POSSIBLE_ERROR - 0.9f) * (1 - accuracy); 
    }





    //  Life cycle  ------------------------------------------------- 
    public void Start () 
    {
        #if UNITY_EDITOR 
        UpdateParameters(); 
        #endif 

        startTimer.Reset(ai.Time); 
        updateTimer.Reset(); 

        hasStarted = false; 
    }

    public void Update () 
    {
        #if UNITY_EDITOR 
        UpdateParameters(); 
        #endif 
        
        if (!hasStarted) 
        {
            hasStarted = startTimer.UpdateIfNeeded(ai.Time); 
            if (!hasStarted) return; 
        }

        updateTimer.UpdateIfNeeded(ai.Time); 
    }

    public void Stop () 
    {
        #if UNITY_EDITOR 
        UpdateParameters(); 
        #endif 

        OnStop(); 
    }

    protected virtual void OnStart () {} 

    protected abstract void OnUpdate (); 

    protected virtual void OnStop () {} 


}
