using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Array to configure the disks on the scene
	// This is in the reverse order in which they stack on the scene
	public GameObject[] goDiskPrefabs;

	public Text inputNameUI;
	public Text uiNameText;
	public Text uiMovesText;
	public Text uiTimerText;
	public Text uiGameOverText;
	public GameObject HudPanel;
	public GameObject GameOverPanel;

	public Vector2 startPosition = new Vector2 (-7.19f, 2);
	private string playerName;
	private float timer;
	private List<GameObject> goDisks;
	private PoleScript oldPole;
	private PoleScript newPole;

	public static int moves;

	void Start () {

		playerName = "dummy";
		goDisks = new List<GameObject>();
	}

	void Update()
	{
		// Update HUD items with new data
		timer += Time.deltaTime;
		uiTimerText.text = timer.ToString("0.0");

		uiMovesText.text = "Moves: " + moves.ToString();
	}
	

	public void StartGame()
	{
		// Create disks for the first time
		if (goDisks.Count <= 0)
		{
			// Create the disks on the scene
			for (int i = 0; i < goDiskPrefabs.Length; i++) {
				GameObject disk = (GameObject)Instantiate(goDiskPrefabs[i], transform.position, Quaternion.identity);
				goDisks.Add(disk);
			}
		}

		// Move disks to start position
		for (int i = 0; i < goDisks.Count; i++) 
		{
			goDisks[i].GetComponent<DiskMovement>().snapPosition = startPosition;
		}

		// Display the HUD
		HudPanel.SetActive(true);

		timer = 0;
		moves = -4;
	}


	// Get player name to display on the HUD
	public void GetPlayerName()
	{
		if (inputNameUI.text.Length != 0)
			playerName = inputNameUI.text;

		uiNameText.text = playerName;
	}

	public void GameOver()
	{
		GameOverPanel.SetActive(true);

		uiGameOverText.text = "Congratulations!! You finished in " 
			+ moves + " moves. \n Your time is " + timer.ToString("0.0") + "s.";
	}

	public void EndGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
