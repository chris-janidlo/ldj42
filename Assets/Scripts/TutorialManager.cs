using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
	public Image Image;
	public Text Text;

	[TextArea]
	public string[] Messages;

	int index = 0;
	bool showMessage, messageUp = true;

	void Update ()
	{
		if (Board.Instance.Turn == Turn.AI)
		{
			showMessage = true;
		}
		else if (showMessage)
		{
			showMessage = false;
			messageUp = true;

			StartCoroutine(showMessageDelay());
		}

		if (messageUp && Input.GetMouseButton(0))
		{
			if (index == Messages.Length)
			{
				SceneManager.LoadScene("Menu");
			}
			messageUp = false;

			Image.enabled = false;
			Text.enabled = false;
		}
	}

	IEnumerator showMessageDelay ()
	{
		yield return new WaitForSeconds(1);

		Image.enabled = true;
		Text.enabled = true;

		Text.text = Messages[index++];
	}
}
