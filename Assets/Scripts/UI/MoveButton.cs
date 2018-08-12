using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
	static event System.EventHandler anyButtonHasBeenSelectedHandler;

	public PlayerPiece Player;
	public AMove Move;

	[Tooltip("Color when in selected state - when button has been pressed but the move hasn't yet been confirmed with a click on an applicable BoardSpace.")]
	public Color SelectedColor;

	bool _selected;
	bool selected
	{
		get { return _selected; }
		set
		{
			_selected = value;

			buttonGraphic.color = value ? SelectedColor : oldColor;

			if (value)
			{
				applicableSpaces = Move.GetLegalMoves(Player);
				anyButtonHasBeenSelectedHandler(this, null);
			}

			foreach (var s in applicableSpaces)
			{
				if (value)
					s.BoardSpaceClickedHandler += OnBoardSpaceClick;
				else
					s.BoardSpaceClickedHandler -= OnBoardSpaceClick;
				s.IsDisabled = !value;
			}

			if (!value) applicableSpaces = null;
		}
	}

	Graphic buttonGraphic;
	Button button;
	Color oldColor;
	List<BoardSpace> applicableSpaces; // only non-null when selected

	void Start ()
	{
		buttonGraphic = GetComponent<Graphic>();
		button = GetComponent<Button>();

		oldColor = buttonGraphic.color;
		button.onClick.AddListener(OnButtonClick);

		anyButtonHasBeenSelectedHandler += onAnyButtonHasBeenSelected;
	}

	void Update ()
	{
		if (Player == null) throw new System.Exception("Player cannot be null");
	}

	void OnButtonClick ()
	{
		if (!enabled) return; // since this class is almost entirely events, disabling it in editor doesn't do much without this line

		selected = !selected;
	}

	void OnBoardSpaceClick (object o, System.EventArgs e)
	{
		var space = (BoardSpace) o;
		Move.ApplyEffect(Player, space);
		selected = false;
	}

	void onAnyButtonHasBeenSelected (object o, System.EventArgs e)
	{
		if ((MoveButton) o != this && selected)
		{
			selected = false;
		}
	}
}
