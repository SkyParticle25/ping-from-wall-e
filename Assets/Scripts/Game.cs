using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 




public enum PlayerType {Player, NPC} 


public class Game : MonoBehaviour
{
    // start parameters 
    static PlayerType leftPlayer = PlayerType.Player; 
    static PlayerType rightPlayer = PlayerType.NPC; 

    // singleton 
    public static Game instance { get; private set; } 
    // parameters 
    [SerializeField] GameObject playerPrefab; 
    [SerializeField] GameObject npcPrefab; 
    [Space]
    [SerializeField] Square square; 
    [SerializeField] Transform platformsParent; 
    [Space]
    [SerializeField] ScoreCounter scoreLeft; 
    [SerializeField] ScoreCounter scoreRight; 
    [Space]
    [SerializeField] float startWaitTime = 1; 
    // data 
    bool isPaused; 
    Side nextLaunchSide; 



    void Awake () 
    {
        instance = this; 

        InitGame(); 
        InitLaunchSide(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        square.onGoal += OnGoal; 
        StartRound(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame) 
        {
            IsPaused = !IsPaused; 
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame) 
        {
            RestartRound(); 
        }
    }

    void OnDestroy () 
    {
        Time.timeScale = 1; 
    }





    //  Events  ----------------------------------------------------- 
    public delegate void EventHandler (); 
    public event EventHandler onReset; 

    public void OnGoal (Side squareLeftTo) 
    {
        if (squareLeftTo == Side.Left) 
        {
            scoreRight.Increment(); 
        }
        else 
        {
            scoreLeft.Increment(); 
        }

        RestartRound(); 
    }





    //  Init game  -------------------------------------------------- 
    public static void SetStartParameters (PlayerType leftPlayer, PlayerType rightPlayer) 
    {
        Game.leftPlayer = leftPlayer; 
        Game.rightPlayer = rightPlayer; 
    }

    void InitGame () 
    {
        CreatePlatform(leftPlayer, Side.Left); 
        CreatePlatform(rightPlayer, Side.Right); 
    }

    void CreatePlatform (PlayerType playerType, Side side) 
    {
        // create platform 
        GameObject prefab = 
            playerType == PlayerType.Player ? 
            playerPrefab : npcPrefab; 
        GameObject platform = Instantiate(
            prefab, 
            Vector2.zero, 
            Quaternion.identity, 
            platformsParent 
        ); 

        // init platform 
        platform.GetComponent<Platform>().Init(side); 
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







    //  Game  ------------------------------------------------------- 
    public bool IsPaused 
    {
        get { return isPaused; } 
        set {
            isPaused = value; 
            Time.timeScale = value ? 0 : 1; 
        }
    }

    void StartRound () 
    {
        square.LaunchAfter(startWaitTime); 
    }

    void RestartRound () 
    {
        ResetRound(); 
        StartRound(); 
    }

    void ResetRound () 
    {
        UpdateNextLaunchSide(); 
        onReset(); 
    }





    //  Launch side  ------------------------------------------------ 
    public Side NextLaunchSide => nextLaunchSide; 

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
