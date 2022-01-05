using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.InputSystem; 
using UnityEngine.EventSystems; 
using UnityEngine.UI; 





public class PauseMenu : MonoBehaviour
{
    // parameters 
    [SerializeField] GameObject pauseMenuPanel; 
    [SerializeField] Game game; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            IsPaused = !IsPaused; 
        }
    }





    //  State  ------------------------------------------------------ 
    bool IsPaused 
    {
        get { return pauseMenuPanel.activeSelf; } 
        set {
            game.IsPaused = value; 
            pauseMenuPanel.SetActive(value); 

            // when closing the menu deselect last pressed button 
            // so that it's not selected when the menu is active again 
            if (!value) 
            {
                EventSystem.current.SetSelectedGameObject(null); 
            }
        }
    }





    //  Events  ----------------------------------------------------- 
    public void Continue () 
    {
        IsPaused = false; 
    }

    public void MainMenu () 
    {
        SceneManager.LoadSceneAsync("Main menu"); 
    }

}
