using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 





public class Square : MonoBehaviour
{

    // parameters 
    [SerializeField] Platform leftPlatform; 
    [SerializeField] Platform rightPlatform; 
    [SerializeField] float startOffset = 2; 
    [SerializeField] float speed = 10; 
    [Range(0, 40)] [SerializeField] float angleSpread = 25; 
    // connections 
    Game game; 
    World world; 
    // geometry 
    float width; 
    float height; 
    // motion 
    Vector2 velocity; 
    Vector2 position01; 
    float distance; 



    // Start is called before the first frame update
    void Start()
    {
        world = World.instance; 
        game = Game.instance; 

        game.onReset += Reset; 

        InitGeometry(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game.instance.IsPaused) 
        {
            Move(); 
            DoCollisions(); 
        }
    }





    //  Connections  ------------------------------------------------ 
    public Platform LeftPlatform 
    {
        get { return leftPlatform; } 
        set {
            leftPlatform = value; 
        }
    }

    public Platform RightPlatform 
    {
        get { return rightPlatform; } 
        set {
            rightPlatform = value; 
        }
    }





    //  Events  ----------------------------------------------------- 
    public delegate void GoalEventHandler (Side side); 
    public event GoalEventHandler onGoal; 





    //  State  ------------------------------------------------------ 
    void Launch () 
    {
        float angle = 
              45 + 90 * Random.Range(0, 4)                  // choose 1 out of 4 directions  
            + angleSpread * Random.Range(-0.5f, 0.5f);      // add random offset 

        Vector2 direction = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad), 
            Mathf.Sin(angle * Mathf.Deg2Rad) 
        ); 
        velocity = speed * direction; 
    }

    public void LaunchAfter (float seconds) 
    {
        StartCoroutine(_LaunchAfter(seconds)); 
    }

    IEnumerator _LaunchAfter (float seconds) 
    {
        yield return new WaitForSeconds(seconds); 
        Launch(); 
    }

    public void Reset () 
    {
        ResetPosition(); 
        ResetVelocity(); 

        StopAllCoroutines(); 
    }





    //  Geometry  --------------------------------------------------- 
    public float xMin => Position.x - width / 2; 
    public float yMin => Position.y - height / 2; 
    public float xMax => Position.x + width / 2; 
    public float yMax => Position.y + height / 2; 
    public float Width => width; 
    public float Height => height; 
    float MaxAngle => 45 + angleSpread / 2; 

    void InitGeometry () 
    {
        width = transform.localScale.x / 2; 
        height = transform.localScale.y / 2; 
    }





    //  Position  --------------------------------------------------- 
    public Vector2 Position 
    {
        get { return transform.position; } 
        private set {
            transform.position = value; 
        }
    }

    void ResetPosition () 
    {
        Position = game.NextLaunchSide == Side.Left ? 
            new Vector2(- startOffset, 0) : 
            new Vector2(  startOffset, 0); 
    }





    //  Motion  ----------------------------------------------------- 
    public Vector2 Velocity => velocity; 
    public float Distance => distance; 

    void Move () 
    {
        Vector2 motion = velocity * Time.deltaTime; 

        Position += motion; 
        distance += motion.magnitude; 
    }

    void FlipVelocityVertically () 
    {
        velocity.y = - velocity.y; 
    }

    void ReflectOffPlatform (Platform platform) 
    {
        float reflectionAngle = CreateReflectionAngle(platform); 

        Vector2 newV = new Vector2(
            speed * Mathf.Cos(reflectionAngle * Mathf.Deg2Rad), 
            speed * Mathf.Sin(reflectionAngle * Mathf.Deg2Rad) 
        ); 
        if (velocity.x > 0) newV.x = - newV.x; 

        velocity = newV; 
    }

    float CreateReflectionAngle (Platform platform) 
    {
        float yDelta = Position.y - platform.Position.y; 
        float maxYDelta = platform.Rect.height / 2; 
        
        float t = Mathf.Clamp(yDelta / maxYDelta, -1, 1); 
        return t * MaxAngle; 
    }

    void ResetVelocity () 
    {
        velocity = Vector2.zero; 
    }





    //  Collisions  ------------------------------------------------- 
    void DoCollisions () 
    {
        Platform platform = CheckPlatforms(); 
        if (platform != null) ReflectOffPlatform(platform); 

        if (CheckHorizontalBounds(out Side side)) 
        {
            if (onGoal != null) onGoal(side); 
            return; 
        }

        if (CheckVerticalBounds()) FlipVelocityVertically(); 
    }

    bool CheckHorizontalBounds (out Side side) 
    {
        if (velocity.x < 0) 
        {
            side = Side.Left; 
            return xMin <= world.Rect.xMin; 
        }
        if (velocity.x > 0) 
        {
            side = Side.Right; 
            return xMax >= world.Rect.xMax; 
        }

        side = Side.Right; 
        return false; 
    }

    bool CheckVerticalBounds () 
    {
        return velocity.y < 0 && yMin <= world.Rect.yMin 
            || velocity.y > 0 && yMax >= world.Rect.yMax; 
    }

    Platform CheckPlatforms () 
    {
        //  Square intersects a platform if: 
        //  - square top line passes platform front line 
        //  - square bottom line passes platform front line 
        // 
        //  Other cases are omitted by design: 
        //  - square x platform side 
        //  - square x platfrom back 

        SegmentH squareBottom = new SegmentH(yMin, xMin, xMax); 
        SegmentH squareTop = new SegmentH(yMax, xMin, xMax); 
        SegmentV leftSurface = leftPlatform.Surface; 
        SegmentV rightSurface = rightPlatform.Surface; 

        if (
            velocity.x < 0 && 
            (
                Geometry.SegmentH_SegmentV_Continious(
                    squareBottom, 
                    leftSurface, 
                    velocity, 
                    leftPlatform.Velocity, 
                    Time.deltaTime 
                )
                || 
                Geometry.SegmentH_SegmentV_Continious(
                    squareTop, 
                    leftSurface, 
                    velocity, 
                    leftPlatform.Velocity, 
                    Time.deltaTime 
                )
            )
        ) return leftPlatform; 

        if (
            velocity.x > 0 && 
            (
                Geometry.SegmentH_SegmentV_Continious(
                    squareBottom, 
                    rightSurface, 
                    velocity, 
                    leftPlatform.Velocity, 
                    Time.deltaTime 
                )
                || 
                Geometry.SegmentH_SegmentV_Continious(
                    squareTop, 
                    rightSurface, 
                    velocity, 
                    leftPlatform.Velocity, 
                    Time.deltaTime 
                )
            )
        ) return rightPlatform; 

        return null; 
    }

}
