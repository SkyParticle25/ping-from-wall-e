using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 




public enum PlayerType {Player, NPC} 


public class Game : Singleton<Game> 
{
    // start parameters 
    public static PlayerType leftPlayerType = PlayerType.Player; 
    public static PlayerType rightPlayerType = PlayerType.NPC; 

    // parameters 
    [SerializeField] GameObject playerPrefab; 
    [SerializeField] GameObject npcPrefab; 
    [Space]
    [SerializeField] Square square; 
    [SerializeField] Transform platformsContainer; 
    [Space]
    [SerializeField] float startWaitTime = 1; 
    // data 
    bool isPaused; 
    Side nextLaunchSide; 



    void Awake () 
    {
        InitSingleton(this); 
        InitEvents(); 
        InitGame(); 
        InitLaunchSide(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            RestartRound(); 
        }
    }

    void OnDestroy () 
    {
        ClearSingleton(); 
        ClearEvents(); 
        ClearTime(); 
    }





    //  Events  ----------------------------------------------------- 
    public delegate void EventHandler (); 
    public delegate void GoalEventHandler (Side squareLeftTo); 
    public static event GoalEventHandler onGoal = delegate {}; 
    public static event EventHandler onPause = delegate {}; 
    public static event EventHandler onContinue = delegate {}; 
    public static event EventHandler onRoundReset = delegate {}; 
    public static event EventHandler onGameReset = delegate {}; 

    void InitEvents () 
    {
        square.onGoal += OnGoal; 
    }

    void ClearEvents () 
    {
        square.onGoal -= OnGoal; 
    }

    public static void OnGoal (Side squareLeftTo) 
    {
        onGoal(squareLeftTo); 
        instance.RestartRound(); 
    }





    //  Game  ------------------------------------------------------- 
    void InitGame () 
    {
        GameSettings.Load(); 

        CreatePlatform(leftPlayerType, Side.Left); 
        CreatePlatform(rightPlayerType, Side.Right); 
    }

    void CreatePlatform (PlayerType playerType, Side side) 
    {
        // create platform 
        GameObject prefab = 
            playerType == PlayerType.Player ? 
            playerPrefab : 
            npcPrefab; 
        GameObject platform = Instantiate(
            prefab, 
            Vector2.zero, 
            Quaternion.identity, 
            platformsContainer 
        ); 

        // init platform 
        platform.GetComponent<Platform>().Init(side); 

        // init player / NPC 
        switch (playerType) 
        {
            case PlayerType.Player: 
                Player player = platform.GetComponent<Player>(); 
                player.Init(side == Side.Left ? PlayerNumber.Player1 : PlayerNumber.Player2); 
                break; 
            case PlayerType.NPC: 
                NPC npc = platform.GetComponent<NPC>(); 
                npc.Init(square); 
                break; 
        }

        // add platform to square 
        switch (side) 
        {
            case Side.Left: 
                square.LeftPlatform = platform.GetComponent<Platform>(); 
                break; 
            case Side.Right: 
                square.RightPlatform = platform.GetComponent<Platform>(); 
                break; 
        }
    }

    void StartGame () 
    {
        StartRound(); 
    }

    void ResetGame () 
    {
        ResetRound(); 
        onGameReset(); 
    }

    public static void RestartGame () 
    {        
        instance.ResetGame(); 
        instance.StartGame(); 
    }





    //  Rounds  ----------------------------------------------------- 
    void StartRound () 
    {
        square.LaunchAfter(startWaitTime); 
    }

    void ResetRound () 
    {
        UpdateNextLaunchSide(); 
        onRoundReset(); 
    }

    void RestartRound () 
    {
        ResetRound(); 
        StartRound(); 
    }





    //  Pause  ------------------------------------------------------ 
    public static bool IsPaused => instance.isPaused; 

    public static void Pause () 
    {
        instance.isPaused = true; 
        Time.timeScale = 0; 

        onPause(); 
    }

    public static void Continue () 
    {
        instance.isPaused = false; 
        Time.timeScale = 1; 

        onContinue(); 
    }

    void ClearTime () 
    {
        Time.timeScale = 1; 
    }





    //  Launch side  ------------------------------------------------ 
    public static Side NextLaunchSide => instance.nextLaunchSide; 

    void InitLaunchSide () 
    {
        nextLaunchSide = Random.value < 0.5f ? Side.Left : Side.Right; 
    }

    void UpdateNextLaunchSide () 
    {
        if (nextLaunchSide == Side.Left) 
        {
            nextLaunchSide = Side.Right; 
        }
        else 
        {
            nextLaunchSide = Side.Left; 
        }
    }

}
