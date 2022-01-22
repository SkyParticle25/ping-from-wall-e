using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 





public class PauseUI : MonoBehaviour
{
    // parameters 
    [SerializeField] GameObject pauseMenu; 
    [SerializeField] GameObject settingsMenu; 



    void Awake () 
    {
        InitEvents(); 
    }
    
    void Start () 
    {
        // originally it is active so that it can recieve Awake() callback and initialize 
        // it is in Start() so that kids can initialize too 
        gameObject.SetActive(false); 
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            Game.Continue(); 
        }
    }

    void OnDestroy () 
    {
        ClearEvents(); 
    }





    //  Events  ----------------------------------------------------- 
    void InitEvents () 
    {
        Game.onPause += OnPause; 
        Game.onContinue += OnContinue; 
    }

    void ClearEvents () 
    {
        Game.onPause -= OnPause; 
        Game.onContinue -= OnContinue; 
    }

    public void OnPause () 
    {
        gameObject.SetActive(true); 

        pauseMenu.SetActive(true); 
        settingsMenu.SetActive(false); 
    }

    public void OnContinue () 
    {
        gameObject.SetActive(false); 
    }

    
}
