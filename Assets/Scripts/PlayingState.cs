
using UnityEngine;

public class PlayingState : StateBase
{
    private PieceView _playerPieceView;

    GameStateManager _gameStateManager;

    public override void EnterState(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
        _gameStateManager._boardView.CardHovered += CardHovered;
        _gameStateManager._boardView.CardDropped += CardDropped;

        var pieceViews = GameObject.FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
        {
            _gameStateManager._board.Place(PositionHelper.GridPosition(pieceView.Position), pieceView);
            if (pieceView.IsPlayer)
            {
                _playerPieceView = pieceView;
                _gameStateManager._board.Playerpiece = pieceView;
            }
        }
    }

    private void CardDropped(object sender, InteractionEventArgs e)
    {
        if (_gameStateManager._board.TryGetPieceAt(e.Position.GridPosition, out var piece))
            Debug.Log($"Found Piece: {piece}");

        Debug.Log($"{e.Card.CardType} dropped on {e.Position.GridPosition} ");

        if (_gameStateManager._boardView.ActivatedPositions.Contains(e.Position.GridPosition))
        {
            _gameStateManager._engine.Drop(
            e.Card.CardType,
            _gameStateManager._boardView.ActivatedPositions);
        }
    }

    private void CardHovered(object sender, InteractionEventArgs e)
    {
        Debug.Log($"{e.Card.CardType} hovered on {e.Position.GridPosition}");

        var positions = _gameStateManager._engine.MoveSet.For(
            e.Card.CardType).Positions(
            PositionHelper.GridPosition(_playerPieceView.Position),
            e.Position.GridPosition);

        _gameStateManager._boardView.ActivatedPositions = positions;
    }
}
