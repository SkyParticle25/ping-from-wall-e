using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class Test_Geometry
{
    
    
    
    //  Ray_LineV ()  ----------------------------------------------- 
    [Test]
    public void Ray_LineV_Away () 
    {
        // arrange 
        float x = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(-2, 1), 
            new Vector2(-1, 1) 
        ); 

        // act 
        bool success = Geometry.Ray_LineV(ray, x, out Vector2 point); 

        // assert 
        Assert.That(!success); 
    }

    [Test]
    public void Ray_LineV_Towards () 
    {
        // arrange 
        float x = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(9, 0), 
            new Vector2(-1, 1) 
        ); 

        // act 
        bool success = Geometry.Ray_LineV(ray, x, out Vector2 point); 

        // assert 
        Assert.That(success && point == new Vector2(5, 4)); 
    }

    [Test]
    public void Ray_LineV_Parallel () 
    {
        // arrange 
        float x = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(9, 0), 
            new Vector2(0, 1) 
        ); 

        // act 
        bool success = Geometry.Ray_LineV(ray, x, out Vector2 point); 

        // assert 
        Assert.That(!success); 
    }





    //  Ray_LineH ()  ----------------------------------------------- 
    [Test]
    public void Ray_LineH_Away () 
    {
        // arrange 
        float y = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(1, 2), 
            new Vector2(1, -1) 
        ); 

        // act 
        bool success = Geometry.Ray_LineH(ray, y, out Vector2 point); 

        // assert 
        Assert.That(!success); 
    }

    [Test]
    public void Ray_LineH_Towards () 
    {
        // arrange 
        float y = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(1, 9), 
            new Vector2(1, -1) 
        ); 

        // act 
        bool success = Geometry.Ray_LineH(ray, y, out Vector2 point); 

        // assert 
        Assert.That(success && point == new Vector2(5, 5)); 
    }

    [Test]
    public void Ray_LineH_Parallel () 
    {
        // arrange 
        float y = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(9, 0), 
            new Vector2(1, 0) 
        ); 

        // act 
        bool success = Geometry.Ray_LineH(ray, y, out Vector2 point); 

        // assert 
        Assert.That(!success); 
    }





    //  SegmentH_SegmentV_Continious ()  ---------------------------- 
    //  Input space: 
    //      velocity in x:      negative  0  positive 
    //      velocity in y:      negative  0  positive 
    //  Output space: 
    //      intersecting:       yes  no 
    //  Special cases: 
    //      time = 0 
    //      segment length = 0 
    //      velocities are equal 
    [Test]
    public void SegmentH_SegmentV_Continious_vxn_vyn () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(7, 2, 5); 
        Vector2 velocityA = new Vector2(-1, -2); 
        Vector2 velocityB = new Vector2(0, 0); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vx0_vyn () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -5, -10); 
        Vector2 velocityA = new Vector2(2, 2); 
        Vector2 velocityB = new Vector2(2, 0); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vxp_vyn () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(10, -2, 2); 
        Vector2 velocityA = new Vector2(1, -1); 
        Vector2 velocityB = new Vector2(-1, 0); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vxn_vy0 () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(10, -2, 2); 
        Vector2 velocityA = new Vector2(-2, 1); 
        Vector2 velocityB = new Vector2(2, 1); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vxp_vy0 () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(10, -2, 2); 
        Vector2 velocityA = new Vector2(2, 1); 
        Vector2 velocityB = new Vector2(-2, 1); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vxn_vyp () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -5, -2); 
        Vector2 velocityA = new Vector2(-2, 0); 
        Vector2 velocityB = new Vector2(2, 5); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vx0_vyp () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -5, -2); 
        Vector2 velocityA = new Vector2(-2, 0); 
        Vector2 velocityB = new Vector2(-2, 1); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vxp_vyp () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -5, -2); 
        Vector2 velocityA = new Vector2(1, -1); 
        Vector2 velocityB = new Vector2(-1, 1); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_time0_false () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -5, -2); 
        Vector2 velocityA = new Vector2(1, -1); 
        Vector2 velocityB = new Vector2(-1, 1); 
        float time = 0; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_time0_true () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -2, 2); 
        Vector2 velocityA = new Vector2(1, -1); 
        Vector2 velocityB = new Vector2(-1, 1); 
        float time = 0; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_length0 () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 2, 2); 
        SegmentV segmentB = new SegmentV(2, -5, -5); 
        Vector2 velocityA = new Vector2(1, -1); 
        Vector2 velocityB = new Vector2(-1, 1); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vsame_true () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(2, -2, 5); 
        Vector2 velocityA = new Vector2(1, 0); 
        Vector2 velocityB = new Vector2(1, 0); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void SegmentH_SegmentV_Continious_vsame_false () 
    {
        // arrange 
        SegmentH segmentA = new SegmentH(0, 0, 5); 
        SegmentV segmentB = new SegmentV(9, -2, 5); 
        Vector2 velocityA = new Vector2(1, 0); 
        Vector2 velocityB = new Vector2(1, 0); 
        float time = 10; 

        // act 
        bool intersection = Geometry.SegmentH_SegmentV_Continious(
            segmentA, 
            segmentB, 
            velocityA, 
            velocityB, 
            time 
        ); 

        // assert 
        Assert.That(intersection, Is.False); 
    }





    //  Range_Range ()  --------------------------------------------- 
    [Test]
    public void Range_Range_aabb () 
    {
        // arrange 
        Range a = new Range(-5, -2); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void Range_Range_abab () 
    {
        // arrange 
        Range a = new Range(-2, 2); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_baab () 
    {
        // arrange 
        Range a = new Range(1, 2); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_baba () 
    {
        // arrange 
        Range a = new Range(2, 9); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_bbaa () 
    {
        // arrange 
        Range a = new Range(7, 9); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.False); 
    }

    [Test]
    public void Range_Range_touchab () 
    {
        // arrange 
        Range a = new Range(0, 2); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_touchba () 
    {
        // arrange 
        Range a = new Range(0, 7); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_abtouch () 
    {
        // arrange 
        Range a = new Range(-2, 5); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_batouch () 
    {
        // arrange 
        Range a = new Range(2, 5); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }

    [Test]
    public void Range_Range_same () 
    {
        // arrange 
        Range a = new Range(0, 5); 
        Range b = new Range(0, 5); 

        // act 
        bool intersection = Geometry.Range_Range(a, b); 

        // assert 
        Assert.That(intersection, Is.True); 
    }





    //  Reflect_Ray_LineH ()  ----------------------------------------- 
    [Test]
    public void Reflect_Ray_LineH_Away () 
    {
        // arrange 
        float y = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(1, 9), 
            new Vector2(1, 1) 
        ); 

        // act 
        Ray2D newRay = Geometry.Reflect_Ray_LineH(ray, y); 

        // assert 
        Assert.That(
            newRay.origin    == ray.origin && 
            newRay.direction == ray.direction 
        ); 
    }

    [Test]
    public void Reflect_Ray_LineH_Towards () 
    {
        // arrange 
        float y = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(1, 9), 
            new Vector2(1, -1) 
        ); 

        // act 
        Ray2D newRay = Geometry.Reflect_Ray_LineH(ray, y); 

        // assert 
        Assert.That(
            newRay.origin    == new Vector2(5, 5) && 
            newRay.direction == new Vector2(1, 1).normalized 
        ); 
    }

    [Test]
    public void Reflect_Ray_LineH_Parallel () 
    {
        // arrange 
        float y = 5; 
        Ray2D ray = new Ray2D(
            new Vector2(1, 9), 
            new Vector2(1, 0) 
        ); 

        // act 
        Ray2D newRay = Geometry.Reflect_Ray_LineH(ray, y); 

        // assert 
        Assert.That(
            newRay.origin    == ray.origin && 
            newRay.direction == ray.direction 
        ); 
    }

}
