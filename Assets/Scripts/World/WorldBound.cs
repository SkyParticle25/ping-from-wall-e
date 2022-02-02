using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class WorldBound : MonoBehaviour
{



    void Awake ()
    {
        InitEvents(); 
    }

    void OnDestroy () 
    {
        ClearEvents(); 
    }





    //  Events  ----------------------------------------------------- 
    void InitEvents () 
    {
        World.onWorldResized += OnWorldResized; 
    }

    void ClearEvents () 
    {
        World.onWorldResized -= OnWorldResized; 
    }
    
    public void OnWorldResized (float widthChange) 
    {
        Vector3 scale = transform.localScale; 
        scale.x *= widthChange; 
        transform.localScale = scale; 
    }


}
