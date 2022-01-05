using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class ScreenTracker : MonoBehaviour
{
    // singleton 
    public static ScreenTracker instance { get; private set; } 
    // data 
    Vector2 screenSize; 



    void Awake () 
    {
        ScreenTracker.instance = this; 

        RememberScreenSize(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DidScreenSizeChange()) 
        {
            RememberScreenSize(); 
            if (onScreenResized != null) onScreenResized(); 
        }
    }





    //  Events  ----------------------------------------------------- 
    public delegate void EventHandler (); 
    public event EventHandler onScreenResized; 





    //  Screen size  ------------------------------------------------ 
    void RememberScreenSize () 
    {
        screenSize = new Vector2(Screen.width, Screen.height); 
    }

    bool DidScreenSizeChange () 
    {
        return screenSize != new Vector2(Screen.width, Screen.height); 
    }

}
