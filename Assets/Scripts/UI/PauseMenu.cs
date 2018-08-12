using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public Button ContinueButton;

	bool active = false;

	void Start ()
	{
		setMenu(true);
		ContinueButton.onClick.AddListener(continueButtonDelegate);
		setMenu(false);	
	}

	void Update ()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			setMenu(!active);
		}
	}

	void continueButtonDelegate ()
	{
		setMenu(false);
	}

	void setMenu (bool value)
	{
		active = value;
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(value);
		}
	}
}
