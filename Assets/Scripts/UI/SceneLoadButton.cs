using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
	public string SceneName;

	void Start ()
	{
		GetComponent<Button>().onClick.AddListener(onButtonClicked);
	}

	void onButtonClicked ()
	{
		SceneManager.LoadScene(SceneName);
	}
}
