using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : APlayerMove
{
	public override void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		movementEffect(actingPiece, space);
	}

	public override List<BoardSpace> GetLegalMoves (BoardPiece actingPiece, Board board)
	{
		return getPlusShapePositions(actingPiece)
			.Select(v => Board.Instance.Spaces[v])
			.Where(Board.Instance.SpaceIsWalkable)
			.ToList();
	}
}
