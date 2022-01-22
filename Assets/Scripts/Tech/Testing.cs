using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Testing : MonoBehaviour
{
    
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

}
