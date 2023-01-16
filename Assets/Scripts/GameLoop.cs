using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board;
    private Engine _engine;

    private PieceView _playerPieceView;
    private BoardView _boardView;
    public GameObject EnemyPrefab;
    List<GameObject> Enemies = new List<GameObject>();

    public void OnEnable()
    {
        _board = new Board(4);
        _board.PieceMoved += (s, e) => e.Piece.gameObject.transform.position = PositionHelper.WorldPosition(e.ToPosition);
        _board.PieceTaken += (s, e) => e.Piece.Take();

        _engine = new Engine(_board);

        _boardView = FindObjectOfType<BoardView>();

        List<int> check = new List<int>();

        // enemies spawnen
        GameObject[] children = new GameObject[_boardView.gameObject.transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = _boardView.gameObject.transform.GetChild(i).gameObject;
        }

        GameObject[] childrenNew = new GameObject[children.Length - 1];

        System.Array.Copy(children, 1, childrenNew, 0, children.Length - 1);
        children = childrenNew;

        for (int i = 0; i < 8; i++)
        {
            int randomIndex = Random.Range(0, children.Length);
            while (check.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, children.Length);
            }

            GameObject randomObject = children[randomIndex];
            GameObject Enemy = Instantiate(EnemyPrefab, randomObject.transform);
            Enemies.Add(Enemy);
            check.Add(randomIndex);
        }




        _boardView.CardHovered += CardHovered;
        _boardView.CardDropped += CardDropped;

        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
        {
            _board.Place(PositionHelper.GridPosition(pieceView.Position), pieceView);
            if (pieceView.IsPlayer)
            {
                _playerPieceView = pieceView;
                _board.Playerpiece = pieceView;
            }
        }


        // ENEMIES - UITBEREIDING
        /*foreach (var item in pieceViews)
        {
            if (item.gameObject.tag == "Enemy")
            {
                item.GetComponent<Enemy>().Player = _playerPieceView.gameObject;
            }
        }*/

    }

    private void CardDropped(object sender, InteractionEventArgs e)
    {
        if (_board.TryGetPieceAt(e.Position.GridPosition, out var piece))
            Debug.Log($"Found Piece: {piece}");

        Debug.Log($"{e.Card.CardType} dropped on {e.Position.GridPosition} ");

        if (_boardView.ActivatedPositions.Contains(e.Position.GridPosition))
        {
            _engine.Drop(
            e.Card.CardType,
            _boardView.ActivatedPositions);

            /*// ENEMIES - UITBEREIDING
            foreach (GameObject Enemy in Enemies)
            {
                if (Enemy.active)
                    Enemy.GetComponent<Enemy>().MoveToPlayer(_board);
            }*/
        }
    }

    private void CardHovered(object sender, InteractionEventArgs e)
    {
        Debug.Log($"{e.Card.CardType} hovered on {e.Position.GridPosition}");

        var positions = _engine.MoveSet.For(
            e.Card.CardType).Positions(
            PositionHelper.GridPosition(_playerPieceView.Position),
            e.Position.GridPosition);

        _boardView.ActivatedPositions = positions;
    }
}
