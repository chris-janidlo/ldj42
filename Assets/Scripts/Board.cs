using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static Board Instance;

	[Tooltip("X is width, Y is height, in spaces")]
	public Vector2Int Dimensions;
	// bottom left is 0,0, top right is X,Y

	public Dictionary<Vector2Int, bool> BrokenSpaces, OccupiedSpaces;

	public BoardPiece Player;
	public List<AIPiece> Enemies;

	public Turn Turn { get; private set; }

	Dictionary<AIPiece, AMove> enemyMoves = new Dictionary<AIPiece, AMove>();

	void Start ()
	{
		if (Instance != null)
			throw new System.Exception("There can only be one Board per scene");

		Instance = this;
	}
	
	public void SelectPlayerMove (AMove move, params BoardSpace[] spaces)
	{
		move.ApplyEffect(Player, spaces);
		Turn = Turn.AI;
	}

	public void SelectAIMove (AMove move, AIPiece piece, params BoardSpace[] spaces)
	{
		enemyMoves.Add(piece, move);
		if (enemyMoves.Count == Enemies.Count)
		{
			foreach (var kvpair in enemyMoves)
			{
				kvpair.Value.ApplyEffect(kvpair.Key, spaces);
			}
			enemyMoves.Clear();
			Turn = Turn.Player;
		}
	}
}

public enum Turn
{
	Player, AI
}
