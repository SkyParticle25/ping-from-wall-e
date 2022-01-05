using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public enum Side { Left, Right } 


public class World : MonoBehaviour
{
    // singleton 
    public static World instance; 
    // parameters 
    [SerializeField] Vector2 size = new Vector2(50, 25); 
    // geometry 
    Rect rect; 
    

    void Awake () 
    {
        instance = this; 

        ScreenTracker.instance.onScreenResized += OnScreenResized; 

        InitRect(); 
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }





    //  Events  ----------------------------------------------------- 
    public delegate void WorldResizedHandler (float widthChange); 
    public event WorldResizedHandler onWorldResized; 

    public void OnScreenResized () 
    {
        float oldWidth = rect.width; 
        UpdateRectWidth(); 
        float newWidth = rect.width; 

        float widthChange = newWidth / oldWidth; 
        onWorldResized(widthChange); 
    }





    //  Rect  ------------------------------------------------------- 
    public Rect Rect => rect; 

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
