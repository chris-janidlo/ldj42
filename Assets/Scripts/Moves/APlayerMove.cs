using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerMove : AMove
{
	public override void ApplyEffect(BoardPiece actingPiece, BoardSpace space)
	{
		base.ApplyEffect(actingPiece, space);
		Board.Instance.Turn = Turn.AI;
	}
}
