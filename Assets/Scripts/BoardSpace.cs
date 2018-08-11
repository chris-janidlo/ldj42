using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BoardSpace : MonoBehaviour
{
	public Vector2 Dimensions;
	public BoardPiece OccupyingPiece;
	public bool IsBroken;
}
