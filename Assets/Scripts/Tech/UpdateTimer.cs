using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class UpdateTimer 
{
    // update function 
    public delegate void UpdateFunction (); 
    UpdateFunction function; 
    // parameters 
    float updateInterval; 
    float updateVariance; 
    // data 
    float lastUpdated; 
    float nextUpdateInterval; 
    

    public UpdateTimer (
        UpdateFunction function, 
        float updateInterval, 
        float updateVariance 
    ) {
        this.function = function; 
        this.updateInterval = updateInterval; 
        this.updateVariance = updateVariance; 

        Reset(); 
    }

    public void UpdateParameters (
        float updateInterval, 
        float updateVariance 
    ) {
        bool changed = 
            this.updateInterval != updateInterval || 
            this.updateVariance != updateVariance; 
        if (!changed) return; 

        this.updateInterval = updateInterval; 
        this.updateVariance = updateVariance; 

        nextUpdateInterval = FIndNextUpdateInterval(); 
    }
    




    //  Timing  ----------------------------------------------------- 
    bool ShouldUpdateNow (float time) 
    {
        return time - lastUpdated >= nextUpdateInterval; 
    }

    void OnUpdated (float time) 
    {
        lastUpdated = time; 
        nextUpdateInterval = FIndNextUpdateInterval(); 
    }

    float FIndNextUpdateInterval () 
    {
        float offset = Random.value * updateVariance * updateInterval; 
        return updateInterval + offset; 
    }

    public void Reset (float lastUpdated = float.MinValue) 
    {
        this.lastUpdated = lastUpdated; 
        nextUpdateInterval = FIndNextUpdateInterval(); 
    }





    //  Updating  --------------------------------------------------- 
    public bool UpdateIfNeeded (float time) 
    {
        if (ShouldUpdateNow(time)) 
        {
            function(); 
            OnUpdated(time); 
            return true; 
        }

        return false; 
    }


}
