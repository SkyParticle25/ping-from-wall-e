using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;



public class Test_SquarePathExplorer
{




    //  IsOutOfWorldH ()  ------------------------------------------- 
    [Test]
    public void IsOutOfWorldH_Inside ()
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isOut = pathExplorer.IsOutOfWorldH(); 

        // assert 
        Assert.That(!isOut); 
    }

    [Test]
    public void IsOutOfWorldH_OnEdgeLeft ()
    {
        // set up 
        Vector2 position = new Vector2(0, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isOut = pathExplorer.IsOutOfWorldH(); 

        // assert 
        Assert.That(!isOut); 
    }

    [Test]
    public void IsOutOfWorldH_OnEdgeRight ()
    {
        // set up 
        Vector2 position = new Vector2(50, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isOut = pathExplorer.IsOutOfWorldH(); 

        // assert 
        Assert.That(!isOut); 
    }

    [Test]
    public void IsOutOfWorldH_Left ()
    {
        // set up 
        Vector2 position = new Vector2(-20, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isOut = pathExplorer.IsOutOfWorldH(); 

        // assert 
        Assert.That(isOut); 
    }

    [Test]
    public void IsOutOfWorldH_Right ()
    {
        // set up 
        Vector2 position = new Vector2(70, 20); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isOut = pathExplorer.IsOutOfWorldH(); 

        // assert 
        Assert.That(isOut); 
    }





    //  DistanceToLineInX ()  --------------------------------------- 
    [Test]
    public void DistanceToLineInX ()
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        float distance = pathExplorer.DistanceToLineInX(25); 

        // assert 
        Assert.That(distance, Is.EqualTo(5)); 
    }





    //  IsGoingToSide ()  --------------------------------------- 
    [Test]
    public void IsGoingToSide_ToLeft_LeftSide () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isGoingToSide = pathExplorer.IsGoingToSide(Side.Left); 

        // assert 
        Assert.That(isGoingToSide, Is.True); 
    }

    [Test]
    public void IsGoingToSide_ToLeft_RightSide () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isGoingToSide = pathExplorer.IsGoingToSide(Side.Right); 

        // assert 
        Assert.That(isGoingToSide, Is.False); 
    }

    [Test]
    public void IsGoingToSide_ToRight_LeftSide () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isGoingToSide = pathExplorer.IsGoingToSide(Side.Left); 

        // assert 
        Assert.That(isGoingToSide, Is.False); 
    }

    [Test]
    public void IsGoingToSide_ToRight_RightSide () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isGoingToSide = pathExplorer.IsGoingToSide(Side.Right); 

        // assert 
        Assert.That(isGoingToSide, Is.True); 
    }

    [Test]
    public void IsGoingToSide_ZeroVelocity_LeftSide () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(0, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isGoingToSide = pathExplorer.IsGoingToSide(Side.Left); 

        // assert 
        Assert.That(isGoingToSide, Is.False); 
    }

    [Test]
    public void IsGoingToSide_ZeroVelocity_RightSide () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(0, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool isGoingToSide = pathExplorer.IsGoingToSide(Side.Right); 

        // assert 
        Assert.That(isGoingToSide, Is.False); 
    }






    //  IsLookingAtLineInX ()  -------------------------------------- 
    [Test]
    public void IsLookingAtLineInX_ToLeft () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 0.5f); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 10; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.True); 
    }

    [Test]
    public void IsLookingAtLineInX_ToRight () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 0.5f); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 35; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.True); 
    }

    [Test]
    public void IsLookingAtLineInX_ToLeft_Miss () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 0; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_ToRight_Miss () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, -1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 50; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_ToLeft_Away () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 0.5f); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 30; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_ToRight_Away () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 0.5f); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 10; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_ToLeft_Horizontal () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 0); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 0; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.True); 
    }

    [Test]
    public void IsLookingAtLineInX_ToRight_Horizontal () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 0); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 50; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.True); 
    }

    [Test]
    public void IsLookingAtLineInX_ToLeft_Horizontal_Away () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(-1, 0); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 50; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_ToRight_Horizontal_Away () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(1, 0); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 0; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_Vertical_Left () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(0, 0.5f); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 0; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }

    [Test]
    public void IsLookingAtLineInX_Vertical_Right () 
    {
        // set up 
        Vector2 position = new Vector2(20, 10); 
        Vector2 velocity = new Vector2(0, 0.5f); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.25f, 0.25f); 
        Rect worldRect = new Rect(0, 0, 50, 25); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        float lineX = 50; 

        // act 
        bool IsLooking = pathExplorer.IsLookingAtLineInX(lineX); 

        // assert 
        Assert.That(IsLooking, Is.False); 
    }





    //  Integriation tests  -------------------------------------------- 
    [Test]
    public void Integration_1 () 
    {
        // set up 
        Vector2 position = new Vector2(2, 2); 
        Vector2 velocity = new Vector2(1, -1); 
        Ray2D path = new Ray2D(position, velocity); 
        Vector2 squareSize = new Vector2(0.1f, 0.1f); 
        Rect worldRect = new Rect(0, 0, 10, 2); 
        SquarePathExplorer pathExplorer = new SquarePathExplorer(
            path, 
            squareSize, 
            worldRect
        ); 

        // act 
        bool leftWorldEarly = false; 
        bool noticedEdge = false; 
        for (int i = 0; i < 5; i++) 
        {
            if (pathExplorer.IsOutOfWorldH()) 
                leftWorldEarly = true; 

            if (pathExplorer.IsLookingAtLineInX(10) && i == 4) 
                noticedEdge = true; 

            pathExplorer.MoveNext(); 
        }

        bool goingToSide = pathExplorer.IsGoingToSide(Side.Right); 
        bool leftWorld = pathExplorer.IsOutOfWorldH(); 

        // assert 
        Assert.That(
            new bool [] {
                !leftWorldEarly, 
                noticedEdge, 
                goingToSide, 
                leftWorld 
            }, 
            Is.All.True 
        ); 
    }

}
