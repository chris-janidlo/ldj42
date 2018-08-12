using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BoardPiece : MonoBehaviour
{
	public Vector2Int StartingPosition;
	public float Health;

	public List<AMove> MovesFromRest;
	public AMove LastUsedMove = null;

	public List<AMove> GetAllMoves ()
	{
		return MovesFromRest.SelectMany(m => m.Traverse()).Distinct().ToList();
	}
}
