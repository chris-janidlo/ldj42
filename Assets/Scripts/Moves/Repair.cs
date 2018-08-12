using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : APlayerMove
{
	public override void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		space.IsBroken = false;
	}

	public override List<BoardSpace> GetLegalMoves (BoardPiece actingPiece)
	{
		return Board.Instance.GetPlusShapePositionsAroundPiece(actingPiece)
			.Select(v => Board.Instance.Spaces[v])
			.Where(s => s.IsBroken)
			.ToList();
	}
}
