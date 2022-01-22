using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 





public static class GameSettings 
{
	// constants 
	const float DEFAULT_PLATFORM_SIZE = 8; 
	const float DEFAULT_PLATFORM_SPEED = 30; 
	const float DEFAULT_SQUARE_SPEED = 30; 
	const float DEFAULT_AI_ACCURACY = 0.5f; 

    // platforms 
	public static float platformSize; 
	public static float platformSpeed; 
	// square 
	public static float squareSpeed; 
	// ai 
	public static float aiAccuracy; 





	//  Events  ----------------------------------------------------- 
	public delegate void EventHandler (); 
	public static event EventHandler onChanged = delegate {}; 





	//  Operations  ------------------------------------------------- 
	public static void Save () 
	{
		PlayerPrefs.SetFloat("Platform size", platformSize); 
		PlayerPrefs.SetFloat("Platform speed", platformSpeed); 
		PlayerPrefs.SetFloat("Square speed", squareSpeed); 
		PlayerPrefs.SetFloat("AI accuracy", aiAccuracy); 

		// settings are only used in the "Game" scene 
		// this performs live settings update 
		if (SceneManager.GetActiveScene().name == "Game") 
		{
			onChanged(); 
		}
	}

	public static void Load () 
	{
		platformSize  = PlayerPrefs.GetFloat("Platform size",  DEFAULT_PLATFORM_SIZE); 
		platformSpeed = PlayerPrefs.GetFloat("Platform speed", DEFAULT_PLATFORM_SPEED); 
		squareSpeed   = PlayerPrefs.GetFloat("Square speed",   DEFAULT_SQUARE_SPEED); 
		aiAccuracy    = PlayerPrefs.GetFloat("AI accuracy",    DEFAULT_AI_ACCURACY); 
	}

	public static void Reset () 
	{
		platformSize = DEFAULT_PLATFORM_SIZE; 
		platformSpeed = DEFAULT_PLATFORM_SPEED; 
		squareSpeed = DEFAULT_SQUARE_SPEED; 
		aiAccuracy = DEFAULT_AI_ACCURACY; 
	}

}
