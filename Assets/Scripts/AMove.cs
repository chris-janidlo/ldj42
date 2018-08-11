using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMove : MonoBehaviour
{	
	public List<AMove> Combos;
	
	public abstract List<BoardSpace> GetLegalMoves (BoardPiece actingPiece, Board board);

	// assumes legality
	public abstract void ApplyEffect (BoardPiece actingPiece, params BoardSpace[] spaces);
}
