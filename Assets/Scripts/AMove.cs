using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMove : MonoBehaviour
{	
	public List<AMove> Combos;
	
	public abstract List<BoardSpace> GetLegalMoves (BoardPiece actingPiece);

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
}
