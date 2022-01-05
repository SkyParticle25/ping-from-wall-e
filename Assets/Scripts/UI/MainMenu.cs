using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 





public class MainMenu : MonoBehaviour
{
    public GameObject playMenu; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    //  Events  ----------------------------------------------------- 
    public void Play () 
    {
        gameObject.SetActive(false); 
        playMenu.SetActive(true); 
    }

    public void Options () 
    {

    }

    public void Quit () 
    {
        #if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false; 
        #endif 

        Application.Quit(); 
    }



}
