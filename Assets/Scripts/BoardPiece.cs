using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class BoardPiece : MonoBehaviour
{
	public const float SpaceMoveTime = .2f;

	public Vector2Int StartingPosition;
	public Vector3 DesiredPosition;

	public List<AMove> MovesFromRest;
	public AMove LastUsedMove = null;

	public RectTransform HealthIndicator;
	public LayoutGroup HealthIndicatorParent;

	public float MaxHealth;

	[SerializeField]
	float _health_debug_view;
	public float Health
	{
		get { return _health_debug_view; }
		set {
			_health_debug_view = value;

			foreach (Transform child in HealthIndicatorParent.transform)
			{
				Destroy(child.gameObject);
			}
			for (int i = 0; i < _health_debug_view; i++)
			{
				var h = Instantiate(HealthIndicator);
				h.SetParent(HealthIndicatorParent.transform, false);
			}

			if (_health_debug_view <= 0)
				die();
		}
	}

	Vector3 currentSpaceMoveSpeed = Vector3.zero;

	protected virtual void Start ()
	{
		Health = MaxHealth;
	}

	protected virtual void Update ()
	{
		transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref currentSpaceMoveSpeed, SpaceMoveTime);
	}

	public List<AMove> GetAllMoves ()
	{
		return MovesFromRest.SelectMany(m => m.Traverse()).Distinct().ToList();
	}

	protected virtual void die () {}
}
