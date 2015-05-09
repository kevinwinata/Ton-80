using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

	public Animator contentPanel;

	public void StartGame()
	{
		Application.LoadLevel("Main");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void ToggleMenu()
	{
		contentPanel.enabled = true;

		bool isHidden = contentPanel.GetBool("isHidden");
		contentPanel.SetBool("isHidden", !isHidden);
	}
}
