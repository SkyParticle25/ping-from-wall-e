using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Geometry 
{
    




    //  Intersections  ---------------------------------------------- 
    public static bool Ray_LineH (Ray2D ray, float y, out Vector2 point) 
    {
        // x = ox + dx * t 
        // y = oy + dy * t 
        // t = (y - oy) / dy 

        if (ray.direction.y == 0) 
        {
            point = Vector2.zero; 
            return false; 
        }

        float t = (y - ray.origin.y) / ray.direction.y; 
        if (t < 0) 
        {
            point = Vector2.zero; 
            return false; 
        }

        float x = ray.origin.x + ray.direction.x * t; 
        point = new Vector2(x, y); 
        return true; 
    }

    public static bool Ray_LineV (Ray2D ray, float x, out Vector2 point) 
    {
        // x = ox + dx * t 
        // y = oy + dy * t 
        // t = (x - ox) / dx 

        if (ray.direction.x == 0) 
        {
            point = Vector2.zero; 
            return false; 
        }

        float t = (x - ray.origin.x) / ray.direction.x; 
        if (t < 0) 
        {
            point = Vector2.zero; 
            return false; 
        }

        float y = ray.origin.y + ray.direction.y * t; 
        point = new Vector2(x, y); 
        return true; 
    }

    public static bool SegmentH_SegmentV_Continious (
        SegmentH segmentA, 
        SegmentV segmentB, 
        Vector2  velocityA, 
        Vector2  velocityB, 
        float    deltaTime 
    ) 
    {
        //  Segment A 
        //  x: [ax0, ax1] 
        //  y: ay 
        //  velocity: (avx, avy) 

        //  Segment B 
        //  x: [by0, by1] 
        //  y: bx 
        //  velocity: (bvx, bvy) 

        //  Point in segment A 
        //  ax0 + avx t <= x <= ax1 + avx t 
        //  y = ay + avy t 

        //  Point in segment B 
        //  by0 + bvy t <= y <= by1 + bvy t 
        //  x = bx + bvx t 

        //  Is there an intersection 
        //  ax0 <= bx + t (bvx - avx) <= ax1 
        //  by0 <= ay + t (avy - bvy) <= by1 

        //  Is there an intersection (solving for time) 
        //          t >= (ax0 - bx) / (bvx - avx) 
        //          t <= (ax1 - bx) / (bvx - avx) 
        //          bvx - avx > 0 
        //      || 
        //          t >= (ax1 - bx) / (bvx - avx) 
        //          t <= (ax0 - bx) / (bvx - avx) 
        //          bvx - avx < 0 
        //      || 
        //          special case:             bvx - avx = 0 
        //          intersection happens if:  ax0 - bx <= 0 <= ax1 - bx 
        //  &&  
        //          t >= (by0 - ay) / (avy - bvy) 
        //          t <= (by1 - ay) / (avy - bvy) 
        //          avy - bvy > 0 
        //      || 
        //          t >= (by1 - ay) / (avy - bvy) 
        //          t <= (by0 - ay) / (avy - bvy) 
        //          avy - bvy < 0 
        //      || 
        //          special case:             avy - bvy = 0 
        //          intersection happens if:  by0 - ay <= 0 <= by1 - ay 


        
        float velocityX = velocityB.x - velocityA.x; 
        float distanceX0 = segmentA.x0 - segmentB.x; 
        float distanceX1 = segmentA.x1 - segmentB.x; 
        float timeX0; 
        float timeX1; 

        float velocityY = velocityA.y - velocityB.y; 
        float distanceY0 = segmentB.y0 - segmentA.y; 
        float distanceY1 = segmentB.y1 - segmentA.y; 
        float timeY0; 
        float timeY1; 

        if (velocityX == 0) 
        {
            if (distanceX0 <= 0 && 0 <= distanceX1) 
            {
                timeX0 = float.MinValue; 
                timeX1 = float.MaxValue; 
            }
            else 
            {
                timeX0 = float.NaN; 
                timeX1 = float.NaN; 
            }
        }
        else 
        {
            float v0 = distanceX0 / velocityX; 
            float v1 = distanceX1 / velocityX; 
            if (velocityX > 0) 
            {
                timeX0 = v0; 
                timeX1 = v1; 
            }
            else 
            {
                timeX0 = v1; 
                timeX1 = v0; 
            }
        }

        if (velocityY == 0) 
        {
            if (distanceY0 <= 0 && 0 <= distanceY1) 
            {
                timeY0 = float.MinValue; 
                timeY1 = float.MaxValue; 
            }
            else 
            {
                timeY0 = float.NaN; 
                timeY1 = float.NaN; 
            }
        }
        else 
        {
            float v0 = distanceY0 / velocityY; 
            float v1 = distanceY1 / velocityY; 
            if (velocityY > 0) 
            {
                timeY0 = v0; 
                timeY1 = v1; 
            }
            else 
            {
                timeY0 = v1; 
                timeY1 = v0; 
            }
        }

        Range intersectionTimeX = new Range(timeX0, timeX1); 
        Range intersectionTimeY = new Range(timeY0, timeY1); 
        Range selectedRange = new Range(- deltaTime, 0); 
        
        return 
            Range_Range(intersectionTimeX, intersectionTimeY) && 
            Range_Range(intersectionTimeX, selectedRange) && 
            Range_Range(intersectionTimeY, selectedRange); 
    }

    public static bool Range_Range (Range a, Range b) 
    {
        //  Check if two ranges intersect 

        //  Possible cases 
        //  a  a b      b           - 
        //     a b a    b           + 
        //       b a  a b           + 
        //       b    a b a         + 
        //       b      b a  a      - 
        //     a b      b a         + 

        //  First a before first b 
        //     a b a    b           +       second a after first b 
        //     a b      b a         +       second a after first b 
        //  a  a b      b           -       second a before first b 

        //  First b before first a 
        //     b a b    a           +       second b after first a 
        //     b a      a b         +       second b after first a 
        //  b  b a      a           -       second b before first a 
        
        return a.start < b.start ? 
            a.end >= b.start : 
            b.end >= a.start; 
    }





    //  Operations  ------------------------------------------------- 
    public static Ray2D Reflect_Ray_LineH (Ray2D ray, float y) 
    {
        bool success = Geometry.Ray_LineH(ray, y, out Vector2 point); 
        if (!success) return ray; 

        return new Ray2D(
            point, 
            new Vector2(ray.direction.x, - ray.direction.y) 
        ); 
    }


}





public struct SegmentH 
{
    public float y; 
    public float x0; 
    public float x1; 

    public SegmentH (float y, float x0, float x1) 
    {
        this.y = y; 
        this.x0 = x0; 
        this.x1 = x1; 
    }

    public override string ToString () 
    {
        return "y: " + y + "  x0: " + x0 + "  x1: " + x1; 
    }
}

public struct SegmentV 
{
    public float x; 
    public float y0; 
    public float y1; 

    public SegmentV (float x, float y0, float y1) 
    {
        this.x = x; 
        this.y0 = y0; 
        this.y1 = y1; 
    }

    public override string ToString () 
    {
        return "x: " + x + "  y0: " + y0 + "  y1: " + y1; 
    }
}

public struct Range 
{
    public float start; 
    public float end; 

    public Range (float start, float end) 
    {
        this.start = start; 
        this.end = end; 
    }
}
