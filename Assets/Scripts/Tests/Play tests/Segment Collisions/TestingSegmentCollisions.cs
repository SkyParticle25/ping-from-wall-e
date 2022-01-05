using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 





public class TestingSegmentCollisions : MonoBehaviour
{
    public TestSegment testSegmentH; 
    public TestSegment testSegmentV; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool intersecting = Geometry.SegmentH_SegmentV_Continious(
            testSegmentH.SegmentH, 
            testSegmentV.SegmentV, 
            testSegmentH.Velocity, 
            testSegmentV.Velocity, 
            Time.deltaTime 
        ); 

        if (intersecting) 
        {
            testSegmentH.LightUp(0.5f); 
            testSegmentV.LightUp(0.5f); 
        }
    }

}





