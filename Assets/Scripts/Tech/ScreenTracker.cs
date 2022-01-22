using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/// <summary> 
/// <para> Tracks screen size and sends screen resized events. </para>
/// <para> Must update before everything else. </para>
/// </summary>
public class ScreenTracker : Singleton<ScreenTracker> 
{
    // data 
    Vector2 screenSize; 



    void Awake () 
    {
        InitSingleton(this); 
        RememberScreenSize(); 
    }

    void Update()
    {
        if (DidScreenSizeChange()) 
        {
            RememberScreenSize(); 
            onScreenResized(); 
        }
    }

    void OnDestroy () 
    {
        ClearSingleton(); 
    }





    //  Events  ----------------------------------------------------- 
    public delegate void EventHandler (); 
    public static event EventHandler onScreenResized = delegate {}; 





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
