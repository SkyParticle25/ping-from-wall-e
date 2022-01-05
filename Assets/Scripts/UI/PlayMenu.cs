using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 




public class PlayMenu : MonoBehaviour
{
    public GameObject mainMenu; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    //  Events  ----------------------------------------------------- 
    public void PlayerPlayer () 
    {
        Game.SetStartParameters(PlayerType.Player, PlayerType.Player); 
        SceneManager.LoadSceneAsync("Game"); 
    }

    public void PlayerAI () 
    {
        Game.SetStartParameters(PlayerType.Player, PlayerType.NPC); 
        SceneManager.LoadSceneAsync("Game"); 
    }

    public void AIAI () 
    {
        Game.SetStartParameters(PlayerType.NPC, PlayerType.NPC); 
        SceneManager.LoadSceneAsync("Game"); 
    }

    public void Back () 
    {
        gameObject.SetActive(false); 
        mainMenu.SetActive(true); 
    }


}
