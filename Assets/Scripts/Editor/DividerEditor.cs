using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 





[CustomEditor(typeof(Divider))] 
public class DividerEditor : Editor 
{
    public override void OnInspectorGUI () 
    {
        DrawDefaultInspector(); 

        if (GUILayout.Button("Create squares")) 
        {
            ClearSquares(); 
            CreateSquares(); 
        }
    }

    void ClearSquares () 
    {
        Transform transform = ((MonoBehaviour) target).transform; 
        
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject); 
        }
    }

    void CreateSquares () 
    {
        Divider divider = (Divider) target; 

        float squareSize = divider.square.transform.localScale.y; 
        float dividerLength = squareSize * (divider.squareCount * 2 + 1); 

        Vector2 center = divider.transform.position; 
        Vector2 dividerEdge = center - new Vector2(0, dividerLength / 2); 

        Vector2 position = dividerEdge + new Vector2(0, squareSize * 1.5f); 
        Vector2 step = new Vector2(0, squareSize * 2); 
        for (int i = 0; i < divider.squareCount; i++) 
        {
            CreateSquare(divider, position); 
            position += step; 
        }
    }

    void CreateSquare (Divider divider, Vector2 position) 
    {
        Instantiate(
            divider.square, 
            position, 
            Quaternion.identity, 
            divider.transform 
        ); 
    }
}
