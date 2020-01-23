using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class StateManager : MonoBehaviour
{

    static StateManager _S;

    static public StateManager S
    {
        get
        {
            if (_S != null) return _S;
            
            Debug.LogError("StateManager.cs : Trying to get singleton instance, but it is null.");
            return null;
        }
        set
        {
            if (_S != null)
            {
                Debug.LogError("StateManager.cs : Trying to set singleton instance, but it is not null.");
                return;
            }

            _S = value;
        }
    }


    public enum State
    {
        none,
        menu, 
        intro,
        inGame,
        gameOver,
        gameWon
    }

    static State _state = State.none;

    public static State STATE
    {
        get
        {
            return _state;
        }

        private set
        {
            _state = value;

            //call delegates
            if (STATE_CHANGED != null) STATE_CHANGED(value);

            switch (value)
            {
                case State.menu:
                    if (EVENT_MENU != null) EVENT_MENU();
                    break;

                case State.intro:
                    if (EVENT_INTRO != null) EVENT_INTRO();
                    break;

                case State.inGame:
                    if (EVENT_INGAME != null) EVENT_INGAME();
                    break;

                case State.gameOver:
                    if (EVENT_GAMEOVER != null) EVENT_GAMEOVER();
                    break;

                case State.gameWon:
                    if (EVENT_GAMEWON != null) EVENT_GAMEWON();
                    break;

                default:
                    break;
            }
        }
    }


    public delegate void StateChangedDelegate(State s);
    public delegate void StateStartDelegate();

    static public StateChangedDelegate STATE_CHANGED;
    static public StateStartDelegate EVENT_INTRO, 
                                        EVENT_MENU, 
                                        EVENT_INGAME, 
                                        EVENT_GAMEOVER, 
                                        EVENT_GAMEWON;


    public GameObject gameOverPanel;
    public GameObject gameWonPanel;



    

    private void Awake()
    {
        S = this;

        STATE = State.menu;

        Time.timeScale = 1;
    }



    static public void StartGame()
    {
        STATE = State.inGame;
    }




    static public void GameOver()
    {
        STATE = State.gameOver;
        S.gameOverPanel.SetActive(true);

        Time.timeScale = 0;
    }


    static public void GameWon()
    {
        STATE = State.gameWon;
        S.gameWonPanel.SetActive(true);

        Time.timeScale = 0;
    }





    public void StartIntro()
    {
        STATE = State.intro;
        PlayableDirector director = Camera.main.GetComponent<PlayableDirector>();

        director.Stop();
        director.Play();
    }


    public void DebugButton()
    {
        Debug.Log("Button called this function");
    }
   
}
