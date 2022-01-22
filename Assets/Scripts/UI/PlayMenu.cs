using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems; 




public class PlayMenu : Menu 
{
    // parameters 
    [SerializeField] GameObject mainMenu; 



    //  Events  ----------------------------------------------------- 
    public void PlayerPlayer () 
    {
        Game.leftPlayerType = PlayerType.Player; 
        Game.rightPlayerType = PlayerType.Player; 
        SceneManager.LoadSceneAsync("Game"); 
    }

    public void PlayerAI () 
    {
        Game.leftPlayerType = PlayerType.Player; 
        Game.rightPlayerType = PlayerType.NPC; 
        SceneManager.LoadSceneAsync("Game"); 
    }

    public void AIAI () 
    {
        Game.leftPlayerType = PlayerType.NPC; 
        Game.rightPlayerType = PlayerType.NPC; 
        SceneManager.LoadSceneAsync("Game"); 
    }

    public void Back () 
    {
        ToMenu(mainMenu); 
    }

}
