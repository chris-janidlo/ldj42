﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	public Button ContinueButton;

	bool active = false;

	void Start ()
	{
		ContinueButton.onClick.AddListener(continueButtonDelegate);
		setMenu(false);	
	}

	void Update ()
	{
		if (!active && Input.GetButton("Cancel"))
		{
			setMenu(true);
		}
	}

	void continueButtonDelegate ()
	{
		setMenu(false);
	}

	void setMenu (bool value)
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(value);
		}
	}
}
