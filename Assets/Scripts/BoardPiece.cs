using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class BoardPiece : MonoBehaviour
{
	public Vector2Int StartingPosition;

	public List<AMove> MovesFromRest;
	public AMove LastUsedMove = null;

	public float MaxHealth;

	[SerializeField]
	float _health_debug_view;
	public float Health
	{
		get { return _health_debug_view; }
		set {
			_health_debug_view = value;
			if (_health_debug_view <= 0)
				die();
		}
	}

	public virtual void Start ()
	{
		Health = MaxHealth;
	}

	public List<AMove> GetAllMoves ()
	{
		return MovesFromRest.SelectMany(m => m.Traverse()).Distinct().ToList();
	}

	protected virtual void die () {}
}
