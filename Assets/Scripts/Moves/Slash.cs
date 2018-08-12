using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : APlayerMove
{
	public override void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		if (space.OccupyingPiece != null)
		{
			// middleSpace.OccupyingPiece.)KILL
		}
	}

	public override List<BoardSpace> GetLegalMoves (BoardPiece actingPiece, Board board)
	{
		return getPlusShapePositions(actingPiece)
			.Select(v => Board.Instance.Spaces[v])
			.ToList();
	}
}
