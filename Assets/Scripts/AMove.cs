using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMove : MonoBehaviour
{	
	public List<AMove> Combos;
	
	public abstract List<BoardSpace> GetLegalMoves (BoardPiece actingPiece, Board board);

	// assumes legality
	public virtual void ApplyEffect (BoardPiece actingPiece, BoardSpace space)
	{
		actingPiece.LastUsedMove = this;
	}

	public List<AMove> Traverse ()
	{
		List<AMove> alreadySeen = new List<AMove>();
		System.Func<AMove, List<AMove>> recursiveStep = null;
		recursiveStep = m =>
		{
			var result = new List<AMove>();
			if (!alreadySeen.Contains(m))
			{
				alreadySeen.Add(m);
				foreach (var move in m.Combos)
				{
					result.AddRange(recursiveStep(move));
				}
				result.Add(m);
			}
			return result;
		};

		return recursiveStep(this);
	}

	// moves actingPiece to space
	protected void movementEffect (BoardPiece actingPiece, BoardSpace space)
	{
		BoardSpace actingPieceSpace = Board.Instance.GetSpaceContaining(actingPiece);

		actingPieceSpace.IsBroken = true;

		actingPieceSpace.OccupyingPiece = null;
		space.OccupyingPiece = actingPiece;

		actingPiece.transform.position = space.transform.position;
	}

	protected IEnumerable<Vector2Int> getPlusShapePositions (BoardPiece piece, int radius = 1)
	{
		Vector2Int center = (Vector2Int) Board.Instance.GetPositionOf(piece);

		return new Vector2Int[]
		{
			center + Vector2Int.up * radius,
			center + Vector2Int.right * radius,
			center + Vector2Int.down * radius,
			center + Vector2Int.left * radius,
		}
			.Where(Board.Instance.PositionInRange);
	}
}
