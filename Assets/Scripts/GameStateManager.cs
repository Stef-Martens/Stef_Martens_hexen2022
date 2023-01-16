using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    StateBase currentState;
    public PlayingState PlayingState = new PlayingState();
    public StartState StartState = new StartState();
    public Board _board;
    public Engine _engine;
    public BoardView _boardView;
    public GameObject EnemyPrefab;

    void Start()
    {
        currentState = StartState;

        currentState.EnterState(this);
    }

    public void SwitchState()
    {
        currentState = PlayingState;
        PlayingState.EnterState(this);
    }
}
