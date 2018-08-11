using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static Board Instance;

	[Tooltip("X is width, Y is height, in spaces. Coordinates start at bottom left (0, 0)")]
	public Vector2Int Dimensions;
	public BiDictionary<Vector2Int, BoardSpace> Spaces;
	
	public BoardPiece Player;
	public List<AIPiece> Enemies;

	public Turn Turn;// { get; private set; }

	// Dictionary<AIPiece, AMove> enemyMoves = new Dictionary<AIPiece, AMove>();

	void Start ()
	{
		if (Instance != null)
			throw new System.Exception("There can only be one Board per scene");

		Instance = this;
	}
	
	public BoardSpace GetSpaceContaining (BoardPiece piece)
	{
		var space = Spaces.SingleOrDefault(p => p.Value.OccupyingPiece == piece);
		if (space.Equals(default(KeyValuePair<Vector2Int, BoardSpace>)))
		{
			return null;
		}
		else
		{
			return space.Value;
		}
	}

	public Vector2Int? GetPositionOf (BoardPiece piece)
	{
		BoardSpace space = GetSpaceContaining(piece);
		if (space == null)
		{
			return null;
		}
		else
		{
			return Spaces.Reverse[space];
		}
	}

	public bool PositionIsWalkable (Vector2Int pos)
	{
		bool inRange = pos.x >= 0 && pos.x < Dimensions.x &&
			pos.y >= 0 && pos.y < Dimensions.y;
		BoardSpace space = Spaces[pos];
		return inRange && !space.IsBroken && space.OccupyingPiece == null;
	}
	
	// TODO: move these to their respective classes
	// public void SelectPlayerMove (AMove move, params BoardSpace[] spaces)
	// {
	// 	move.ApplyEffect(Player, spaces);
	// 	Turn = Turn.AI;
	// }

	// public void SelectAIMove (AMove move, AIPiece piece, params BoardSpace[] spaces)
	// {
	// 	enemyMoves.Add(piece, move);
	// 	if (enemyMoves.Count == Enemies.Count)
	// 	{
	// 		foreach (var kvpair in enemyMoves)
	// 		{
	// 			kvpair.Value.ApplyEffect(kvpair.Key, spaces);
	// 		}
	// 		enemyMoves.Clear();
	// 		Turn = Turn.Player;
	// 	}
	// }
}

public enum Turn
{
	Player, AI
}
