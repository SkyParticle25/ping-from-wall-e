using System.Collections;
using System.Collections.Generic;
using UnityEngine; 





public class Platform : MonoBehaviour
{
    [Header("Platform")] 
    [SerializeField] Side side = Side.Left; 
    [Range(0, 0.1f)] [SerializeField] float distanceToEdge = 0.05f; 

    // connections 
    protected World world; 
    // velocity 
    float velocity; 



    public void Init (Side side) 
    {
        this.side = side; 
    }

    // Start is called before the first frame update
    protected void Start()
    {
        world = World.instance; 

        Game.instance.onReset += Reset; 

        PositionMeOnScreen(); 
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }





    //  Geometry  --------------------------------------------------- 
    public Vector2 Position => transform.position; 
    public Side Side => side; 
    public Rect Rect => new Rect(transform.position, transform.localScale); 
    public SegmentV Surface 
    {
        get {
            Vector2 extents = transform.localScale / 2; 

            float x = (side == Side.Left) ? 
                Position.x + extents.x / 2 : 
                Position.x - extents.x / 2; 
            float yMin = Position.y - extents.y; 
            float yMax = Position.y + extents.y; 

            return new SegmentV(x, yMin, yMax); 
        }
    }





    //  Position  --------------------------------------------------- 
    float MinAllowedY => world.Rect.yMin + Rect.height / 2; 
    float MaxAllowedY => world.Rect.yMax - Rect.height / 2; 
    
    void PositionMeOnScreen () 
    {
        float normalizedX = (side == Side.Left) ? 
            distanceToEdge : 
            (1 - distanceToEdge); 

        transform.position = new Vector3(
            world.Rect.xMin + world.Rect.width * normalizedX, 
            transform.position.y, 
            transform.position.z 
        ); 
    }





    //  State  ------------------------------------------------------ 
    public virtual void Reset () 
    {
        ResetPosition(); 
    }





    //  Position  --------------------------------------------------- 
    public float PosY 
    {
        get { return transform.position.y; } 
        private set {
            value = Mathf.Clamp(value, MinAllowedY, MaxAllowedY); 
            
            transform.position = new Vector3(
                transform.position.x, 
                value, 
                transform.position.z 
            ); 
        }
    }

    public void Move (float motion) 
    {
        PosY += motion; 
        UpdateVelcoity(motion); 
    }

    void ResetPosition () 
    {
        transform.position = new Vector3(
            transform.position.x, 
            0, 
            transform.position.z 
        ); 
    }





    //  Velocity  --------------------------------------------------- 
    public Vector2 Velocity => new Vector2(0, velocity); 

    void UpdateVelcoity (float motion) 
    {
        velocity = motion / Time.deltaTime; 
    }

    void ResetVelocity () 
    {
        velocity = 0; 
    }

}





public struct PlatformSurface 
{
    public float x; 
    public float yMin; 
    public float yMax; 

    public PlatformSurface (Vector2 position, Vector2 size, Side side) 
    {
        x = (side == Side.Left) ? 
            position.x + size.x / 2 : 
            position.x - size.x / 2; 
        yMin = position.y - size.y / 2; 
        yMax = position.y + size.y / 2; 
    }
}
