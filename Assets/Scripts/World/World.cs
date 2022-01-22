using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public enum Side { Left, Right } 


public class World : Singleton<World> 
{
    // parameters 
    [SerializeField] Vector2 size = new Vector2(50, 25); 
    // geometry 
    Rect rect; 
    
    

    void Awake () 
    {
        InitSingleton(this); 
        InitEvents(); 
        InitRect(); 
    }    

    void OnDestroy () 
    {
        ClearSingleton(); 
        ClearEvents(); 
    }





    //  Events  ----------------------------------------------------- 
    public delegate void WorldResizedHandler (float widthChange); 
    public static event WorldResizedHandler onWorldResized = delegate {}; 

    void InitEvents () 
    {
        ScreenTracker.onScreenResized += OnScreenResized; 
    }

    void ClearEvents () 
    {
        ScreenTracker.onScreenResized -= OnScreenResized; 
    }

    public void OnScreenResized () 
    {
        float oldWidth = rect.width; 
        UpdateRectWidth(); 
        float newWidth = rect.width; 

        float widthChange = newWidth / oldWidth; 
        onWorldResized(widthChange); 
    }





    //  Rect  ------------------------------------------------------- 
    public static Rect Rect => instance.rect; 

    void InitRect () 
    {
        rect = new Rect(0, - size.y / 2, 0, size.y); 
        UpdateRectWidth(); 
    }

    void UpdateRectWidth () 
    {
        Camera camera = Camera.main; 
        Vector2 min = camera.ScreenToWorldPoint(new Vector2(0, 0)); 
        Vector2 max = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); 

        rect.x = min.x; 
        rect.width = max.x - min.x; 
    }





    //  Tech  ------------------------------------------------------- 
    void OnDrawGizmos () 
    {
        Gizmos.color = Color.white; 
        Gizmos.DrawWireCube(transform.position, size); 
    }

    void OnDrawGizmosSelected () 
    {
        Gizmos.color = Color.green; 
        Gizmos.DrawWireCube(transform.position, size); 
        Gizmos.DrawWireCube(transform.position, size * 1.001f); 
    }

}
