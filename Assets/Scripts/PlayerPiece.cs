using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerPiece : BoardPiece
{
	public Image GameOverImage;

	protected override void Start ()
	{
		base.Start();
		GameOverImage.gameObject.SetActive(false);
	}

	protected override void die ()
	{
		StartCoroutine(gameOverSequence());
	}

	IEnumerator gameOverSequence ()
	{
		GameOverImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(3.7f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
