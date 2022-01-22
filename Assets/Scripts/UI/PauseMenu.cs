using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.InputSystem; 
using UnityEngine.EventSystems; 
using UnityEngine.UI; 





public class PauseMenu : Menu 
{
    // parameters 
    [SerializeField] GameObject settingsMenu; 



    //  Button events  ---------------------------------------------- 
    public void Continue () 
    {
        Game.Continue(); 
    }

    public void Restart () 
    {
        Game.Continue(); 
        Game.RestartGame(); 
    }

    public void Options () 
    {
        ToMenu(settingsMenu); 
    }

    public void MainMenu () 
    {
        SceneManager.LoadSceneAsync("Main menu"); 
    }

}
