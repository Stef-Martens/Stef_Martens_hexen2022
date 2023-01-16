
using System;
using System.Collections.Generic;
using UnityEngine;

public class StartState : StateBase
{
    private MenuView _menuView;

    List<GameObject> Enemies = new List<GameObject>();

    public GameStateManager _gameStateManager;

    public override void EnterState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _menuView = GameObject.FindObjectOfType<MenuView>();
        if (_menuView != null)
            _menuView.PlayClicked += OnPlayClicked;



        gameStateManager._board = new Board(4);
        gameStateManager._board.PieceMoved += (s, e) => e.Piece.gameObject.transform.position = PositionHelper.WorldPosition(e.ToPosition);
        gameStateManager._board.PieceTaken += (s, e) => e.Piece.Take();

        gameStateManager._engine = new Engine(gameStateManager._board);

        gameStateManager._boardView = GameObject.FindObjectOfType<BoardView>();

        List<int> check = new List<int>();

        // enemies spawnen
        GameObject[] children = new GameObject[gameStateManager._boardView.gameObject.transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = gameStateManager._boardView.gameObject.transform.GetChild(i).gameObject;
        }

        GameObject[] childrenNew = new GameObject[children.Length - 1];

        System.Array.Copy(children, 1, childrenNew, 0, children.Length - 1);
        children = childrenNew;

        for (int i = 0; i < 8; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, children.Length);
            while (check.Contains(randomIndex))
            {
                randomIndex = UnityEngine.Random.Range(0, children.Length);
            }

            GameObject randomObject = children[randomIndex];
            GameObject Enemy = GameObject.Instantiate(gameStateManager.EnemyPrefab, randomObject.transform);
            Enemies.Add(Enemy);
            check.Add(randomIndex);
        }

    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        _gameStateManager.SwitchState();
    }
}
