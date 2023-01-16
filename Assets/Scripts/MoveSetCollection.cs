﻿using System.Collections.Generic;

public class MoveSetCollection
{
    private Dictionary<CardType, MoveSet> _movesets = new Dictionary<CardType, MoveSet>();

    internal MoveSetCollection(Board board)
    {
        _movesets.Add(CardType.Teleport, new TeleportMoveSet(board));
        _movesets.Add(CardType.Swipe, new SwipeMoveSet(board));
        _movesets.Add(CardType.Slash, new SlashMoveSet(board));
        _movesets.Add(CardType.Push, new PushbackMoveSet(board));
    }

    public IMoveSet For(CardType type) => _movesets[type];

    internal bool TryGetMoveSet(CardType type, out MoveSet moveSet)
        => _movesets.TryGetValue(type, out moveSet);
}

