using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 




public class NPC : MonoBehaviour 
{    
    // parameters 
    [SerializeField] Square square; 
    [SerializeField] float speed = 10; 
    [Header("AI")]
    [SerializeField] AI_Idle_Params idle; 
    [SerializeField] AI_Catching_Params catching; 
    // connections 
    Platform platform; 
    World world; 
    // AI 
    AI ai; 
    // motion 
    float destination = 0; 



    public void Init (Square square) 
    {
        this.square = square; 
    }

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Platform>(); 
        world = World.instance; 

        Game.instance.onReset += Reset; 

        ai = new AI(this, idle, catching); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateParameters(); 

        ai.Update(); 
        MoveTowards(destination); 
    }

    void UpdateParameters () 
    {
        ai.UpdateParameters(idle, catching); 
    }





    //  Info  ------------------------------------------------------- 
    public Platform Platform => platform; 
    public Square Square => square; 
    public World World => world; 





    //  State  ------------------------------------------------------ 
    public void Reset () 
    {
        destination = 0; 
        ai.Reset(); 
    }





    //  Motion  ----------------------------------------------------- 
    public float Destination => destination; 

    public void SetDestination (float y, float maxError = 0) 
    {
        if (maxError == 0) 
        {
            destination = y; 
        }
        else 
        {
            float offsetScale = Random.Range(- maxError, maxError); 
            float offset = offsetScale * platform.Rect.height / 2; 
            destination = y + offset; 
        }
    }
    
    void MoveTowards (float y) 
    {
        float motion = y - platform.PosY; 

        float maxDistance = speed * Time.deltaTime; 
        motion = Mathf.Clamp(motion, - maxDistance, maxDistance); 

        platform.Move(motion); 
    }

}
