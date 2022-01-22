using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class WorldObject : MonoBehaviour
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
        Vector3 position = transform.position; 
        position.x *= widthChange; 
        transform.position = position; 
    }

}
