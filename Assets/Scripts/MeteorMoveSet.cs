using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MeteorMoveSet : MoveSet
{
    public MeteorMoveSet(Board board) : base(board)
    {
    }

    public override List<Position> Positions(Position fromPosition, Position hoverPosition)
    {
        var allValidPositions = new List<Position>();
        var validPositions = new List<Position>();


        for (int d = 0; d < 6; d++)
        {
            var direction = Position.Direction(d);

            Position currentPosition = hoverPosition.Add(direction);
            if (Board.IsValid(currentPosition))
            {
                validPositions.Add(currentPosition);
            }
        }

        validPositions.Add(hoverPosition);

        allValidPositions.AddRange(validPositions);


        return allValidPositions;
    }

    internal override bool Execute(List<Position> positions)
    {
        foreach (var position in positions)
        {
            Board.Take(position);
        }
        return true;
    }
}
