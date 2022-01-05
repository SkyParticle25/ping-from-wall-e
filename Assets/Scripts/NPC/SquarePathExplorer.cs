using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SquarePathExplorer 
{
    // data 
    Ray2D path; 
    Vector2 squareSize; 
    Rect worldRect; 


    public SquarePathExplorer (Ray2D path, Vector2 squareSize, Rect worldRect) 
    {
        this.path = path; 
        this.squareSize = squareSize; 
        this.worldRect = worldRect; 
    }

    public SquarePathExplorer (Square square, World world) 
    {
        path = new Ray2D(square.Position, square.Velocity); 
        squareSize = new Vector2(square.Width, square.Height); 
        worldRect = world.Rect; 
    }





    //  Info  ------------------------------------------------------ 
    public Ray2D Path => path; 

    public bool IsOutOfWorldH () 
    {
        return path.origin.x > worldRect.xMax || 
               path.origin.x < worldRect.xMin; 
    }

    public float DistanceToLineInX (float lineX) 
    {
        return Mathf.Abs(lineX - path.origin.x); 
    }

    public bool IsGoingToSide (Side side) 
    {
        return side == Side.Left ? 
            path.direction.x < 0 : 
            path.direction.x > 0; 
    }

    public bool IsLookingAtLineInX (float lineX) 
    {
        if (path.direction.x == 0) return false; 

        float toLine = lineX - path.origin.x; 
        if (toLine * path.direction.x < 0) return false; 

        float dy = path.direction.y / path.direction.x; 
        float distance = DistanceToLineInX(lineX); 

        float deltaY = distance * dy; 
        float futureY = path.origin.y + deltaY; 

        return 
            futureY >= worldRect.yMin && 
            futureY <= worldRect.yMax; 
    }

    public float GetPathY (float x) 
    {
        bool found = Geometry.Ray_LineV(path, x, out Vector2 point); 
        if (!found) Debug.LogError("Path point not found"); 

        return point.y; 
    }





    //  Iterations  ------------------------------------------------- 
    float ReflectionLineInY 
    {
        get {
            if (path.direction.y == 0) 
                Debug.LogError("Asking for reflection line in Y without velocity in Y"); 

            return path.direction.y > 0 ? 
                worldRect.yMax - squareSize.y / 2 : 
                worldRect.yMin + squareSize.y / 2; 
        }
    }

    public void MoveNext () 
    {
        if (path.direction.y == 0) return; 
        
        path = Geometry.Reflect_Ray_LineH(path, ReflectionLineInY); 
    }





    //  Tech  ------------------------------------------------------- 
    public void DrawPath () 
    {
        Debug.DrawRay(path.origin, path.direction * 90, Color.white); 
    }

}
