using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private readonly Board _board;
    private readonly MoveSetCollection _moveSetCollection;

    public MoveSetCollection MoveSet => _moveSetCollection;

    public Engine(Board board)
    {
        _board = board;
        _moveSetCollection = new MoveSetCollection(_board);
    }

    internal bool Drop(CardType cardType, List<Position> positions)
    {
        if (!_board.TryGetPieceAt(PositionHelper.GridPosition(_board.Playerpiece.Position), out var piece))
            return false;

        if (!_moveSetCollection.TryGetMoveSet(cardType, out var moveSet))
            return false;


        // kaart alleen disablen bij droppen op bord
        CardManager manager = FindObjectOfType<CardManager>();
        GameObject card = manager.ActivateNext();
        card.GetComponent<CardView>().used = true;
        card.SetActive(false);

        if (!moveSet.Execute(positions))
            return false;


        return true;
    }
}
