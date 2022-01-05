using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 





public class ScoreCounter : MonoBehaviour
{
    // constants 
    const int DIGITS_COUNT = 4; 
    // parameters 
    [SerializeField] Side side; 
    // connections 
    Text text; 
    // data 
    int score; 



    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    //  Counter  ---------------------------------------------------- 
    public int Score => score; 

    public void Increment () 
    {
        score++; 
        UpdateDisplay(); 
    }

    public void Reset () 
    {
        score = 0; 
        UpdateDisplay(); 
    }





    //  Display  ---------------------------------------------------- 
    void UpdateDisplay () 
    {
        text.text = ScoreToString(); 
    }

    string ScoreToString () 
    {
        string s = ""; 
        int n = score; 

        int i = 0; 
        for (; i < DIGITS_COUNT;) 
        {
            s = n % 10 + s; 

            // because it doesn't increase the counter after the last iteration 
            // so it looks like it hasn't reached DIGITS_COUNT yet 
            i++; 

            // counter should be increased before it leaves the cycle in any way 
            n /= 10; 
            if (n == 0) break; 
        }

        for (; i < DIGITS_COUNT; i++) 
        {
            if (side == Side.Left) 
            {
                s = " " + s; 
            }
            else 
            {
                s = s + " "; 
            }
        }

        return s; 
    }


}
