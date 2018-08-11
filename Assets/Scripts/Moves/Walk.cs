using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : AMove
{
	public override void ApplyEffect(BoardPiece actingPiece, BoardSpace space)
	{
		BoardSpace actingPieceSpace = Board.Instance.GetSpaceContaining(actingPiece);

		actingPieceSpace.IsBroken = true;

		actingPieceSpace.OccupyingPiece = null;
		space.OccupyingPiece = actingPiece;
		
		actingPiece.transform.position = space.transform.position;
	}

	public override List<BoardSpace> GetLegalMoves(BoardPiece actingPiece, Board board)
	{
		Vector2Int actingPos = (Vector2Int) Board.Instance.GetPositionOf(actingPiece);

		Vector2Int[] potentials =
		{
			actingPos + Vector2Int.up,
			actingPos + Vector2Int.right,
			actingPos + Vector2Int.down,
			actingPos + Vector2Int.left,
		};

		return potentials.Where(Board.Instance.PositionIsWalkable).Select(v => Board.Instance.Spaces[v]).ToList();
	}
}
