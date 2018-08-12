using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : APlayerMove
{
	public float Damage;

	public override void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		BoardSpace actingPieceSpace = Board.Instance.GetSpaceContaining(actingPiece);
		Vector2Int midpoint = (Board.Instance.Spaces.Reverse[actingPieceSpace] + Board.Instance.Spaces.Reverse[space]).DivideBy(2);

		BoardSpace middleSpace = Board.Instance.Spaces[midpoint];

		if (middleSpace.OccupyingPiece != null)
		{
			middleSpace.OccupyingPiece.Health -= Damage;
		}
		else
		{
			middleSpace.IsBroken = false;
		}

		Board.Instance.MovePieceToSpace(actingPiece, space);
	}

	public override List<BoardSpace> GetLegalMoves (BoardPiece actingPiece)
	{
		return Board.Instance.GetPlusShapePositionsAroundPiece(actingPiece, 2)
			.Select(v => Board.Instance.Spaces[v])
			.Where(Board.Instance.SpaceIsWalkable)
			.ToList();
	}
}
