using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateScript : MonoBehaviour
{
    public int counter = 0;
    public bool ball1Scored = false;
    public bool ball2Scored = false;
    int player1Goals = 0;
    int player2Goals = 0;
    float playerOffset = 10.5f;
    //float ballOffset = 4;
    int playerWhoMadeLastGoal = 1;

    private static GamestateScript _instance;
    float timer = 120;


    public static GamestateScript Instance { get { return _instance; } }
    // Start is called before the first frame update

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        counter++;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Escape))
        {
            RestartScene(1);
        }
        //if (ball1Scored || ball2Scored)
        //{
        //    RestartScene(2);
        //}

    }

    public float PlayerStartPosition(int playerNumber)
    {
        if (playerWhoMadeLastGoal == 1 && playerNumber == 1)
        {
            return playerOffset;
        }
        else if(playerWhoMadeLastGoal == 2 && playerNumber == 2)
        {
            return -playerOffset;
        }
        else
        {
            return 0;
        }
    }

    private void RestartScene(int playerAdvantage)
    {
        counter++;
        ball1Scored = false;
        ball2Scored = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        PlayerController[] players = FindObjectsOfType<PlayerController>();
        players[0].transform.position = new Vector3(3, players[0].transform.position.y, 0);
        Debug.Log("Player1 score: " + player1Goals + " Player2 score: " + player2Goals);
    }

    //private void 

    private void OnEnable()
    {
        _instance = this; 
    }

    public void Goal(int player, int ball)
    {
        if(player == 0)
        {
            return;
        }
        if(ball == 1)
        {
            if(!ball1Scored)
            {
                if (player == 1)
                {
                    player1Goals++;
                    playerWhoMadeLastGoal = 1;
                }
                else
                {
                    player2Goals++;
                    playerWhoMadeLastGoal = 2;
                }
            }
            ball1Scored = true;
        }
        if (ball == 2)
        {
            if (!ball2Scored)
            {
                if (player == 1)
                {
                    player1Goals++;
                    playerWhoMadeLastGoal = 1;
                }
                else
                {
                    player2Goals++;
                    playerWhoMadeLastGoal = 2;
                }
            }
            ball2Scored = true;
        }
        
        RestartScene(player);
    }
    


}
