using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ENEMY MOVEMENT UITBEREIDING
    /*  public GameObject Player;
      public void MoveToPlayer(Board board)
      {
          //Debug.Log(PositionHelper.GridPosition(gameObject.transform.position).Distance(PositionHelper.GridPosition(new Vector3(0, 0, 0))));
          List<Position> positions = PositionHelper.GridPosition(gameObject.transform.position).LineDraw(PositionHelper.GridPosition(gameObject.transform.position), PositionHelper.GridPosition(Player.gameObject.transform.position));


          PieceView check = null;
          if (positions.Count > 0)
          {
              if (!board.TryGetPieceAt(positions[1], out check))
              {
                  // nog checken of deze tile vrij is zetten voor ge zet
                  // eerst kaart uitvoeren => daarna pas enemies verplaatsen
                  board.Move(PositionHelper.GridPosition(gameObject.transform.position), PositionHelper.GridPosition(PositionHelper.WorldPosition(positions[1])));
                  GetComponent<PieceView>().MoveTo(PositionHelper.GridPosition(PositionHelper.WorldPosition(positions[1])));
              }

          }


      }*/
}
