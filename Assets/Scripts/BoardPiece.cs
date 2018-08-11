using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
	public float Health;

	public List<AMove> MovesFromRest;
	public AMove LastUsedMove;
}
