using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPiece : BoardPiece
{
	public float Damage;
	
	protected override void die () {
		Board.Instance.Enemies.Remove(this);
		Destroy(gameObject);
	}
}
