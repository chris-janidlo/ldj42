using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class BoardSpace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
	// NOTE: color names are chosen to match Unity's button naming scheme, but have slightly different semantics. NormalColor is for spaces that are applicable for the selected move, and Disabled color is for non-applicable spaces. Every other color name is self explanatory
	public Color NormalColor, HighlightedColor, PressedColor, DisabledColor, BrokenColor;
	public Vector2 Dimensions;
	public BoardPiece OccupyingPiece;

	public event System.EventHandler BoardSpaceClickedHandler;

	#region properties
	bool _broken;
	public bool IsBroken
	{
		get
		{
			return _broken;
		}
		set
		{
			setColor(value ? BrokenColor : DisabledColor);
			_broken = value;
		}
	}

	bool _disabled;
	public bool IsDisabled
	{
		get
		{
			return _disabled;
		}
		set
		{
			setColor(value ? DisabledColor : NormalColor);
			_disabled = value;
		}
	}
	#endregion

	new Renderer renderer;

	bool midClick, pointerInside;

	public void Start ()
	{
		renderer = GetComponentInChildren<Renderer>();
		IsDisabled = true;
	}

	#region pointer handlers
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (IsDisabled) return;
		
		pointerInside = true;
		setColor(midClick ? PressedColor : HighlightedColor);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (IsDisabled) return;
		
		pointerInside = false;
		setColor(midClick ? HighlightedColor : NormalColor);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (IsDisabled) return;
		
		midClick = true;
		setColor(PressedColor);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (IsDisabled) return;
		
		midClick = false;
		setColor(pointerInside ? HighlightedColor : NormalColor);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (IsDisabled) return;
		
		if (BoardSpaceClickedHandler != null)
		{
			BoardSpaceClickedHandler(this, null);
		}
	}
	#endregion

	void setColor (Color color)
	{
		// TODO: transition?
		renderer.material.color = color;
	}
}
