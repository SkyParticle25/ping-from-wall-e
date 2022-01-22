using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 





public class MainMenu : Menu 
{
    [SerializeField] GameObject playMenu; 
    [SerializeField] GameObject settingsMenu; 



    //  Events  ----------------------------------------------------- 
    public void Play () 
    {
        ToMenu(playMenu); 
    }

    public void Options () 
    {
        ToMenu(settingsMenu); 
    }

    public void Quit () 
    {
        #if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false; 
        #endif 

        Application.Quit(); 
    }

}
