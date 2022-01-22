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
    [SerializeField] AI_Activity.Parameters idle; 
    [SerializeField] AI_Activity.Parameters catching; 
    // connections 
    Platform platform; 
    // data 
    AI ai; 
    float destination = 0; 



    void Awake () 
    {
        platform = GetComponent<Platform>(); 
        InitEvents(); 
    }

    public void Init (Square square) 
    {
        this.square = square; 
    }

    void Start()
    {
        InitMotion(); 
        ai = new AI(this, idle, catching); 
    }

    void Update()
    {
        ai.Update(); 
        MoveTowards(destination); 
    }

    void OnDestroy () 
    {
        ai.OnDestroy(); 
        CLearEvents(); 
    }





    //  Info  ------------------------------------------------------- 
    public Platform Platform => platform; 
    public Square Square => square; 





    //  Events  ----------------------------------------------------- 
    void InitEvents () 
    {
        Game.onRoundReset += Reset; 
        GameSettings.onChanged += OnSettingsChanged; 
    }

    void CLearEvents () 
    {
        Game.onRoundReset -= Reset; 
        GameSettings.onChanged -= OnSettingsChanged; 
    }

    public void OnSettingsChanged () 
    {
        speed = GameSettings.platformSpeed; 
    }

    public void Reset () 
    {
        ResetMotion(); 
        ai.Reset(); 
    }





    //  Motion  ----------------------------------------------------- 
    public float Destination => destination; 

    void InitMotion () 
    {
        speed = GameSettings.platformSpeed; 
    }

    public void SetDestination (float y, float maxError = 0) 
    {
        if (maxError == 0) 
        {
            destination = y; 
        }
        else 
        {
            float offsetAmount = Random.Range(- maxError, maxError); 
            float offset = offsetAmount * platform.Rect.height / 2; 
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

    void ResetMotion () 
    {
        destination = 0; 
    }

}
