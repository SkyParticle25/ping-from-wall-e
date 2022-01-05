using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class WorldObject : MonoBehaviour
{
    // connections 
    World world; 


    // Start is called before the first frame update
    void Start()
    {
        world = World.instance; 

        world.onWorldResized += OnWorldResized; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    //  Events  ----------------------------------------------------- 
    public void OnWorldResized (float widthChange) 
    {
        Vector3 position = transform.position; 
        position.x *= widthChange; 
        transform.position = position; 
    }

}
