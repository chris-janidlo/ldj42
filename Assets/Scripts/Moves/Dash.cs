using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : AMove
{
	public override void ApplyEffect(BoardPiece actingPiece, BoardSpace space)
	{
		BoardSpace actingPieceSpace = Board.Instance.GetSpaceContaining(actingPiece);
		Vector2Int midpoint = (Board.Instance.Spaces.Reverse[actingPieceSpace] + Board.Instance.Spaces.Reverse[space]).DivideBy(2);

		BoardSpace middleSpace = Board.Instance.Spaces[midpoint];

		actingPieceSpace.IsBroken = true;

		if (middleSpace.OccupyingPiece != null)
		{
			// middleSpace.OccupyingPiece.)KILL
		}
		else
		{
			middleSpace.IsBroken = false;
		}

		actingPieceSpace.OccupyingPiece = null;
		space.OccupyingPiece = actingPiece;
	}

	public override List<BoardSpace> GetLegalMoves(BoardPiece actingPiece, Board board)
	{
		Vector2Int actingPos = (Vector2Int) Board.Instance.GetPositionOf(actingPiece);

		Vector2Int[] potentials =
		{
			actingPos + Vector2Int.up * 2,
			actingPos + Vector2Int.right * 2,
			actingPos + Vector2Int.down * 2,
			actingPos + Vector2Int.left * 2,
		};

		return potentials.Where(v =>
		{
			var b = Board.Instance.Spaces[v];
			return Board.Instance.PositionInRange(v) && !b.IsBroken && b.OccupyingPiece == null;
		})
			.Select(v => Board.Instance.Spaces[v]).ToList();
	}
}
