using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static Board Instance;

	[Tooltip("X is width, Y is height, in spaces. Coordinates start at bottom left (0, 0)")]
	public Vector2Int Dimensions;
	public BoardSpace SpacePrefab;
	public BiDictionary<Vector2Int, BoardSpace> Spaces;
	
	public BoardPiece Player;
	public List<AIPiece> Enemies;

	public Turn Turn;// { get; private set; }

	bool turningToPlayer = false;

	// Dictionary<AIPiece, AMove> enemyMoves = new Dictionary<AIPiece, AMove>();

	void Start ()
	{
		if (Instance != null)
			throw new System.Exception("There can only be one Board per scene");

		Instance = this;

		Spaces = new BiDictionary<Vector2Int, BoardSpace>();
		initializeBoard();
	}

	void Update ()
	{
		;
		if (Turn == Turn.AI && !turningToPlayer)
		{
			turningToPlayer = true;
			StartCoroutine(changeTurnAfterSeconds(2));
		}
	}

	IEnumerator changeTurnAfterSeconds (float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Turn = Turn.Player;
		turningToPlayer = false;
	}
	
	#region Public Helper Methods
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
	#endregion

	void initializeBoard ()
	{
		for (int x = 0; x < Dimensions.x; x++)
		{
			for (int y = 0; y < Dimensions.y; y++)
			{
				Vector2Int boardPos = new Vector2Int(x, y);
				Vector2 scaledPos = boardPos * SpacePrefab.Dimensions;
				Vector3 spawnPos = new Vector3(scaledPos.x, 0, scaledPos.y);

				Spaces[boardPos] = Instantiate(SpacePrefab, spawnPos, Quaternion.identity);
				Spaces[boardPos].transform.parent = transform;
			}
		}

		Spaces[Player.StartingPosition].OccupyingPiece = Player;
		Player.transform.position = Spaces[Player.StartingPosition].transform.position;

		foreach (var enemy in Enemies)
		{
			Spaces[enemy.StartingPosition].OccupyingPiece = enemy;
			enemy.transform.position = Spaces[enemy.StartingPosition].transform.position;
		}
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
