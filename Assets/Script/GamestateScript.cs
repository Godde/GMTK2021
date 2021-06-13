using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    Text text;
    [SerializeField]
    Text timerText;

    private static GamestateScript _instance;
    float timer = 120;

    AudioSource[] audioSources = new AudioSource[50];
    [SerializeField]
    AudioClip testClip;
    [SerializeField]
    AudioClip matchEndStart;
    [SerializeField]
    AudioClip goalSound;


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
        for(int i = 0; i<audioSources.Length; i++)
        {
            //audioSources[i] = new AudioSource();
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }

        GamestateScript.Instance.PlayAudioClipWithSound(matchEndStart, 1);

        //GamestateScript.Instance.PlayAudioClipWithSound(testClip, 1);
        //audioSources[0].clip = testClip;
        //audioSources[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //Debug.Log(timer);
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) )
        {
            RestartScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Reset();
        }
        //if (ball1Scored || ball2Scored)
        //{
        //    RestartScene(2);
        //}

        text.text = player2Goals + " - " + player1Goals;
        if (timer > 0)
        {
            timerText.text = (int)Mathf.Floor(timer) / 60 + ":" + (int)Mathf.Floor(timer) % 60;
        }
        else
        {
            Time.timeScale = 0;
            timerText.text = "Game Over! Press N for new game or press Escape to exit";
        }
    }

    private void Reset()
    {
        RestartScene(1);
        Time.timeScale = 1;
        timer = 120;
        ball1Scored = false;
        ball2Scored = false;
        player1Goals = 0;
        player2Goals = 0;
    }

    private void EndTimerUpdate()
    {
        if(timer < 0)
        {
            GamestateScript.Instance.PlayAudioClipWithSound(matchEndStart, 1);
        }
    }

    public void PlayAudioClipWithSound(AudioClip audioClip, float volume)
    {
        foreach(AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {

                audioSource.clip = audioClip;
                audioSource.volume = volume;
                audioSource.Play();
                return;
            }
        }
    }

    public static void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1, LoadSceneMode.Single);
    }

    public static void PreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
    }

    public static void Exit()
    {
        Application.Quit();
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
        GamestateScript.Instance.PlayAudioClipWithSound(goalSound, 1);
        counter++;
        ball1Scored = false;
        ball2Scored = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

        PlayerController[] players = FindObjectsOfType<PlayerController>();
        players[0].transform.position = new Vector3(3, players[0].transform.position.y, 0);
        Debug.Log("Player1 score: " + player1Goals + " Player2 score: " + player2Goals);
        //Time.timeScale = 0;
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
