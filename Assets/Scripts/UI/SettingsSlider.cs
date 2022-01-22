using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 




public class SettingsSlider : MonoBehaviour
{
    // parameters 
    [SerializeField] Slider slider; 
    [SerializeField] Text valueText; 



    //  Events  ----------------------------------------------------- 
    public void OnValueChanged () 
    {
        float value = slider.value; 
        valueText.text = value.ToString("F1"); 
    }

}
