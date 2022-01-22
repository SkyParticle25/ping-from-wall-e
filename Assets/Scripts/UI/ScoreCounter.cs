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



    void Awake () 
    {
        InitEvents(); 
        InitDisplay(); 
    }

    void OnDestroy () 
    {
        ClearEvents(); 
    }





    //  Events  ----------------------------------------------------- 
    void InitEvents () 
    {
        Game.onGoal += OnGoal; 
        Game.onGameReset += OnReset; 
    }

    void ClearEvents () 
    {
        Game.onGoal -= OnGoal; 
        Game.onGameReset -= OnReset; 
    }

    public void OnGoal (Side squareLeftTo) 
    {
        if (
            squareLeftTo == Side.Left && side == Side.Right || 
            squareLeftTo == Side.Right && side == Side.Left 
        ) {
            Increment(); 
        }
    }

    public void OnReset () 
    {
        ResetCounter(); 
    }





    //  Counter  ---------------------------------------------------- 
    public int Score => score; 

    void Increment () 
    {
        score++; 
        UpdateDisplay(); 
    }

    void ResetCounter () 
    {
        score = 0; 
        UpdateDisplay(); 
    }





    //  Display  ---------------------------------------------------- 
    void InitDisplay () 
    {
        text = GetComponentInChildren<Text>(); 
    }

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

            // it's here because it doesn't increase the counter after the last iteration 
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
