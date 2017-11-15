using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour {

	public AudioSource title_music;
	public Image MainPanel;

	public Text HighScoreText;

	void Awake(){
		GameData.high_score = PlayerPrefs.GetInt ("Player.High_Score", 0);
	}

	void Start(){
		HighScoreText.text = "TOP SCORE = " + GameData.high_score.ToString("000000");
		title_music.Play ();	
	}

	public void StartGameA(){
		StartCoroutine (StartGameADelay());
	}
	public void StartGameB(){
		SceneManager.LoadScene ("game1");
	}

	IEnumerator StartGameADelay(){
		InitGameData ();
		MainPanel.gameObject.SetActive (false);
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene ("game1");
	}

	void InitGameData(){
		GameData.round_kills = 0;
		GameData.game_score = 0;
		GameData.current_round = 1;
		GameData.round_duck_number = 0;
	}
}
