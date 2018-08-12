using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : APlayerMove
{
	public override void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		Board.Instance.MovePieceToSpace(actingPiece, space);
	}

	public override List<BoardSpace> GetLegalMoves (BoardPiece actingPiece)
	{
		return Board.Instance.GetPlusShapePositionsAroundPiece(actingPiece)
			.Select(v => Board.Instance.Spaces[v])
			.Where(Board.Instance.SpaceIsWalkable)
			.ToList();
	}
}
