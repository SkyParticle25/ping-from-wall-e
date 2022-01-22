using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 





/// <summary> 
/// Pauses the game on Esc button 
/// </summary> 
public class PauseTrigger : MonoBehaviour 
{
    void Update() 
    {
        if (!Game.IsPaused && Keyboard.current.escapeKey.wasPressedThisFrame) 
        {
            Game.Pause(); 
        }
    }
    
}
