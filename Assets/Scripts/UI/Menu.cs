using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 





public class Menu : MonoBehaviour
{

    protected void OnDisable () 
    {
        // deselect currently selected button 
        // so that it's not selected when the menu is shown again 
        EventSystem.current?.SetSelectedGameObject(null); 
    }


    protected void ToMenu (GameObject newMenu) 
    {
        gameObject.SetActive(false); 
        newMenu.SetActive(true); 
    }

}
