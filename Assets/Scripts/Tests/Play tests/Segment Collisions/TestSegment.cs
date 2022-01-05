using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class TestSegment : DynamicTestObject 
{
    // parameters 
    public Color colorNormal = Color.white; 
    public Color colorLit = Color.green; 
    // connections 
    new SpriteRenderer renderer; 
    // light 
    float lastLitUp; 
    float lightDuration; 



    // Start is called before the first frame update
    new void Start()
    {
        base.Start(); 
        
        renderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update(); 

        UpdateLights(); 
    }





    //  Geometry  --------------------------------------------------- 
    public SegmentH SegmentH 
    {
        get {
            float y = transform.position.y; 
            float x0 = transform.position.x - transform.localScale.x / 2; 
            float x1 = transform.position.x + transform.localScale.x / 2; 

            return new SegmentH(y, x0, x1); 
        }
    }
    public SegmentV SegmentV 
    {
        get {
            float x = transform.position.x; 
            float y0 = transform.position.y - transform.localScale.y / 2; 
            float y1 = transform.position.y + transform.localScale.y / 2; 

            return new SegmentV(x, y0, y1); 
        }
    }





    //  Lights  ----------------------------------------------------- 
    public void LightUp (float duration) 
    {
        renderer.color = colorLit; 
        
        lightDuration = duration; 
        lastLitUp = Time.time; 
    }

    void UpdateLights () 
    {
        if (
            IsLit() && 
            Time.time - lastLitUp >= lightDuration
        ) {
            LightOff(); 
        }
    }

    bool IsLit () 
    {
        return renderer.color == colorLit; 
    }

    void LightOff () 
    {
        renderer.color = colorNormal; 
    }

}
