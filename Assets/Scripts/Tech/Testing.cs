using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Testing : MonoBehaviour
{
    // data 
    public static new Camera camera; 



    void Awake () 
    {
        camera = Camera.main; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {

    }





    //  Drawing  ---------------------------------------------------- 
    public static void DrawPoint (Vector3 point, Color color, float duration = 0) 
    {
        float POINT_SIZE = 0.2f; 

        Vector3 right = Camera.main.transform.right * POINT_SIZE / 2; 
        Vector3 up = Camera.main.transform.up * POINT_SIZE / 2; 

        Vector3 A = point - right - up; 
        Vector3 B = point - right + up; 
        Vector3 C = point + right + up; 
        Vector3 D = point + right - up; 

        Debug.DrawLine(A, B, color, duration); 
        Debug.DrawLine(B, C, color, duration); 
        Debug.DrawLine(C, D, color, duration); 
        Debug.DrawLine(D, A, color, duration); 
    }

    public static void DrawRect (Rect rect, Color color, float duration = 0) 
    {
        // create points 
        Vector3 A = new Vector3(rect.x, rect.y, 0); 
        Vector3 B = new Vector3(rect.x + rect.width, rect.y, 0); 
        Vector3 C = new Vector3(rect.x + rect.width, rect.y + rect.height, 0); 
        Vector3 D = new Vector3(rect.x, rect.y + rect.height, 0); 

        // draw lines 
        Debug.DrawLine(A, B, color, duration); 
        Debug.DrawLine(B, C, color, duration); 
        Debug.DrawLine(C, D, color, duration); 
        Debug.DrawLine(D, A, color, duration); 
    }





    //  Drawing stuff  ---------------------------------------------- 
    public static void DrawGizmoSquare (Vector2 center, float size) 
    {
        DrawGizmoRect(center, new Vector2(size, size)); 
    }

    public static void DrawGizmoRect (Vector2 center, Vector2 size) 
    {
        Vector2 halfSize = size / 2; 

        Vector2 a = center + new Vector2(- halfSize.x, - halfSize.y); 
        Vector2 b = center + new Vector2(- halfSize.x,   halfSize.y); 
        Vector2 c = center + new Vector2(  halfSize.x,   halfSize.y); 
        Vector2 d = center + new Vector2(  halfSize.x, - halfSize.y); 

        Gizmos.DrawLine(a, b); 
        Gizmos.DrawLine(b, c); 
        Gizmos.DrawLine(c, d); 
        Gizmos.DrawLine(d, a); 
    }

}
