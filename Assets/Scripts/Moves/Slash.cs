using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : APlayerMove
{
	public float Damage;

	public override void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		if (space.OccupyingPiece != null)
		{
			space.OccupyingPiece.Health -= Damage;
		}
	}

	public override List<BoardSpace> GetLegalMoves (BoardPiece actingPiece)
	{
		return Board.Instance.GetPlusShapePositionsAroundPiece(actingPiece)
			.Select(v => Board.Instance.Spaces[v])
			.ToList();
	}
}
