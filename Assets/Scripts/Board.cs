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
	
	public PlayerPiece Player;
	[Tooltip("The enemies currently on the board. Also can be set in the editor with (already instantiated) enemies to re-position at the start of the game")]
	public List<AIPiece> Enemies;

	public AIPiece EnemyPrefab;

	[Tooltip("At every turn, the chance to spawn an additional enemy is the value of this curve at time = (number of enemies currently alive).")]
	public AnimationCurve EnemySpawnRate;

	public Turn Turn;

	bool calculatingAI;

	void Awake ()
	{
		if (Instance != null)
			throw new System.Exception("There can only be one Board per scene");

		Instance = this;

		Spaces = new BiDictionary<Vector2Int, BoardSpace>();
		initializeBoard();
	}

	// sorts Vector2Ints so that vectors closer to the given center are sorted first
	// not 100% accurate but that makes things fun anyway
	class Vector2IntComparerClosestToGiven : IComparer<Vector2Int>
	{
		Vector2Int center;

		public Vector2IntComparerClosestToGiven (Vector2Int center)
		{
			this.center = center;
		}

		public int Compare(Vector2Int x, Vector2Int y)
		{
			return (int) (Vector2Int.Distance(x, center) - Vector2Int.Distance(y, center));
		}
	}

	void Update ()
	{
		if (Player == null) throw new System.Exception("Player cannot be null");

		if (Turn == Turn.AI && !calculatingAI)
		{
			StartCoroutine(enemyTurnRoutine());
		}
	}

	IEnumerator enemyTurnRoutine ()
	{
		calculatingAI = true;
		yield return new WaitForSeconds(.2f);

		foreach (var enemy in Enemies)
		{
			BoardSpace closestSpace = GetPlusShapePositionsAroundPiece(enemy)
				.OrderBy(v => v, new Vector2IntComparerClosestToGiven((Vector2Int)GetPositionOf(Player)))
				.Select(v => Spaces[v])
				.Where(s => s.OccupyingPiece == null || s.OccupyingPiece == Player)
				.First();

			if (closestSpace.OccupyingPiece == Player)
			{
				Player.Health -= enemy.Damage;
			}
			else
			{
				MovePieceToSpace(enemy, closestSpace);
			}
		}

		float chance = EnemySpawnRate.Evaluate(Enemies.Count);
		if (Random.value < chance)
		{
			var playerPos = (Vector2Int)GetPositionOf(Player);
			var spawnSpaces = Spaces.Keys
				.Where(v => SpaceIsWalkable(Spaces[v]))
				.Except(new Vector2Int[] {
						playerPos + Vector2Int.up,
						playerPos + Vector2Int.right,
						playerPos + Vector2Int.down,
						playerPos + Vector2Int.left,
				}
					.Where(PositionInRange)
				)
				.ToList();

			SpawnEnemy(spawnSpaces[Random.Range(0, spawnSpaces.Count)]);
		}

		calculatingAI = false;
		Turn = Turn.Player;
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

	public bool SpaceIsWalkable (BoardSpace space)
	{
		return !space.IsBroken && space.OccupyingPiece == null;
	}

	public bool PositionInRange (Vector2Int pos)
	{
		return pos.x >= 0 && pos.x < Dimensions.x &&
			pos.y >= 0 && pos.y < Dimensions.y;
	}

	public void MovePieceToSpace (BoardPiece piece, BoardSpace space)
	{
		BoardSpace currentSpace = GetSpaceContaining(piece);

		currentSpace.IsBroken = true;
		currentSpace.OccupyingPiece = null;

		space.OccupyingPiece = piece;
		piece.DesiredPosition = space.transform.position;
	}

	public void SpawnPiece (BoardPiece piece)
	{
		var space = Spaces[piece.StartingPosition];
		space.OccupyingPiece = piece;

		piece.DesiredPosition = space.transform.position;
		piece.transform.position = space.transform.position;
	}

	public void SpawnEnemy (Vector2Int pos)
	{
		var enemy = Instantiate(EnemyPrefab);

		enemy.transform.parent = transform.Find("Enemies");
		enemy.StartingPosition = pos;

		Enemies.Add(enemy);
		SpawnPiece(enemy);
	}

	public IEnumerable<Vector2Int> GetPlusShapePositionsAroundPiece (BoardPiece piece, int radius = 1)
	{
		Vector2Int center = (Vector2Int) GetPositionOf(piece);

		return new Vector2Int[]
		{
			center + Vector2Int.up * radius,
			center + Vector2Int.right * radius,
			center + Vector2Int.down * radius,
			center + Vector2Int.left * radius,
		}
			.Where(PositionInRange);
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

		SpawnPiece(Player);

		foreach (var enemy in Enemies)
		{
			SpawnPiece(enemy);
		}
	}

}

public enum Turn
{
	Player, AI
}
