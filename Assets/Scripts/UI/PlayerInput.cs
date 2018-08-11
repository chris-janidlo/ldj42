using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
	public BoardPiece Player;

	Button passButton;
	Button[] moveButtons;

	Turn stateImpliedByInput;

	Transform moveButtonsObject;

	void Start ()
	{
		moveButtonsObject = transform.Find("MoveGraph").Find("Moves");
		moveButtons = moveButtonsObject.GetComponentsInChildren<Button>();
		passButton = transform.Find("PassButton").GetComponent<Button>();
	}

	void Update ()
	{
		if (Board.Instance.Turn == stateImpliedByInput) return;

		if (Board.Instance.Turn == Turn.Player)
		{
			var eligibleMoves = Player.MovesFromRest.Union(Player.LastUsedMove?.Combos ?? Enumerable.Empty<AMove>());
			foreach (AMove move in eligibleMoves)
			{
				// TODO: can this be nicer
				moveButtonsObject.Find(move.gameObject.name).GetComponent<Button>().interactable = true;
			}
			passButton.interactable = true;
			stateImpliedByInput = Turn.Player;
		}
		else
		{
			foreach (Button button in moveButtons)
			{
				button.interactable = false;
			}
			passButton.interactable = false;
			stateImpliedByInput = Turn.AI;
		}
	}
}
