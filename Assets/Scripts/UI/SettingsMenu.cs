using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.InputSystem; 
using UnityEngine.EventSystems; 





public class SettingsMenu : Menu 
{
    [SerializeField] GameObject mainMenu; 
    [SerializeField] Slider platformSize; 
    [SerializeField] Slider platformSpeed; 
    [SerializeField] Slider squareSpeed; 
    [SerializeField] Slider aiAccuracy; 



    void OnEnable () 
    {
        GameSettings.Load(); 
        DisplaySettings(); 
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            Back(); 
        }
    }





    //  Events  ----------------------------------------------------- 
    public void Reset () 
    {
        GameSettings.Reset(); 
        DisplaySettings(); 
    }

    public void Save () 
    {
        SaveSettings(); 
        Back(); 
    }

    public void Back () 
    {
        ToMenu(mainMenu); 
    }





    //  Operations  ------------------------------------------------- 
    void DisplaySettings () 
    {
        platformSize.value  = GameSettings.platformSize; 
        platformSpeed.value = GameSettings.platformSpeed; 
        squareSpeed.value   = GameSettings.squareSpeed; 
        aiAccuracy.value    = GameSettings.aiAccuracy; 
    }

    void SaveSettings () 
    {
        GameSettings.platformSize  = platformSize.value; 
        GameSettings.platformSpeed = platformSpeed.value; 
        GameSettings.squareSpeed   = squareSpeed.value; 
        GameSettings.aiAccuracy    = aiAccuracy.value; 
        GameSettings.Save(); 
    }

}
