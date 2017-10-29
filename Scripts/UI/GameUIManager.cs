using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour {

	readonly int PERFECT_BONUS_SCORE = 10000;
	public AudioManager GameAudio;

	public Camera MainCamera;
	public Text ScoreText;
	public Text RoundNumberText;

	public Image HitDuckRed;
	public Image HitDuckWhite;

	public Image PauseMenu;
	public Image PauseMainPanel;

	public Image FlyAwayBox;
	public Image PerfectBox;
	public Image RoundTopBox;
	public Text RoundTopBoxNumber;
	public Image GameOverBox;

	public Text ShotText;
	public Image ProgressImage;

	public Image Bullet1;
	public Image Bullet2;
	public Image Bullet3;

	public Image DuckUIProgressRed1;
	public Image DuckUIProgressRed2;
	public Image DuckUIProgressRed3;
	public Image DuckUIProgressRed4;
	public Image DuckUIProgressRed5;
	public Image DuckUIProgressRed6;
	public Image DuckUIProgressRed7;
	public Image DuckUIProgressRed8;
	public Image DuckUIProgressRed9;
	public Image DuckUIProgressRed10;

	public Image DuckUIProgressWhite1;
	public Image DuckUIProgressWhite2;
	public Image DuckUIProgressWhite3;
	public Image DuckUIProgressWhite4;
	public Image DuckUIProgressWhite5;
	public Image DuckUIProgressWhite6;
	public Image DuckUIProgressWhite7;
	public Image DuckUIProgressWhite8;
	public Image DuckUIProgressWhite9;
	public Image DuckUIProgressWhite10;

	float flyAwayMessageTime = 4f;

	void Awake(){
		GameData.game_started = false;
		StartCoroutine(ShowRoundTopBox ());
		RoundNumberText.text = "R=" + GameData.current_round;
	}

	void Update () {
		CheckCurrentBullets ();
		CheckRoundProgress ();
		FlyAway ();
		UpdateScore ();

		if (GameData.round_duck_number > 10) {
			GameData.round_over = true;
		} else {
			GameData.round_over = false;
		}
	}

	public IEnumerator CheckRoundLimit(){
		if (GameData.round_over) {
			if (GameData.round_kills >= 5) {
				GameData.current_round++;
				if (GameData.round_kills >= 10) {
					StartCoroutine (ShowPerfectBox ());
				} else {
					StartCoroutine (ShowRoundTopBox ());
				}
				GameData.round_duck_number = 0;
				GameData.round_kills = 0;
				HideUIDucks ();
				RoundNumberText.text = "R=" + GameData.current_round;
				yield return new WaitForSeconds (5f);
				SceneManager.LoadScene ("game1");
			} else if (GameData.round_kills < 5) {
				yield return new WaitForSeconds (1f);
				StartCoroutine (ShowGameOver ());
			}
		}
	}

	void HideUIDucks(){
		DuckUIProgressRed1.gameObject.SetActive (false);
		DuckUIProgressWhite1.gameObject.SetActive (false);
		DuckUIProgressRed2.gameObject.SetActive (false);
		DuckUIProgressWhite2.gameObject.SetActive (false);
		DuckUIProgressRed3.gameObject.SetActive (false);
		DuckUIProgressWhite3.gameObject.SetActive (false);
		DuckUIProgressRed4.gameObject.SetActive (false);
		DuckUIProgressWhite4.gameObject.SetActive (false);
		DuckUIProgressRed5.gameObject.SetActive (false);
		DuckUIProgressWhite5.gameObject.SetActive (false);
		DuckUIProgressRed6.gameObject.SetActive (false);
		DuckUIProgressWhite6.gameObject.SetActive (false);
		DuckUIProgressRed7.gameObject.SetActive (false);
		DuckUIProgressWhite7.gameObject.SetActive (false);
		DuckUIProgressRed8.gameObject.SetActive (false);
		DuckUIProgressWhite8.gameObject.SetActive (false);
		DuckUIProgressRed9.gameObject.SetActive (false);
		DuckUIProgressWhite9.gameObject.SetActive (false);
		DuckUIProgressRed10.gameObject.SetActive (false);
		DuckUIProgressWhite10.gameObject.SetActive (false);

	}
	IEnumerator ShowGameOver(){
		GameOverBox.gameObject.SetActive (true);
		if (PlayerPrefs.GetInt ("Player.High_Score", 0) < GameData.game_score) {
			PlayerPrefs.SetInt ("Player.High_Score", GameData.game_score);
		}
		yield return new WaitForSeconds (6f);
		StartCoroutine (CheckRoundLimit ());
		GameOverBox.gameObject.SetActive (false);
		SceneManager.LoadScene ("menu");
	}

	IEnumerator ShowRoundTopBox(){
		RoundTopBox.gameObject.SetActive (true);
		RoundTopBoxNumber.text = "" + GameData.current_round;
		RoundNumberText.text = "R=" + GameData.current_round;
		yield return new WaitForSeconds (5f);
		GameData.game_started = true;
		GameData.round_over = false;
		RoundTopBox.gameObject.SetActive (false);
	}

	IEnumerator ShowPerfectBox(){
		PerfectBox.gameObject.SetActive (true);
		yield return new WaitForSeconds (3f);
		GameData.game_score += PERFECT_BONUS_SCORE;
		PerfectBox.gameObject.SetActive (false);
		yield return new WaitForSeconds (1f);
		StartCoroutine (ShowRoundTopBox ());
	}

	void FlyAway(){
		if (GameData.bird_flew) {
			flyAwayMessageTime -= Time.deltaTime;
			if (flyAwayMessageTime > 0) {
				FlyAwayBox.gameObject.SetActive (true);
				MainCamera.backgroundColor = new Color (0.98f, 0.73f, 0.68f, 0);
				ShotText.GetComponent<Text>().color = new Color(0.98f, 0.73f, 0.68f, 1);
				ProgressImage.GetComponent<Image>().color = new Color(0.98f, 0.73f, 0.68f, 1);
			} else {
				GameData.bird_flew = false;
			}
		} else {
			FlyAwayBox.gameObject.SetActive (false);
			flyAwayMessageTime = 4f;
			MainCamera.backgroundColor = new Color (0.24f, 0.73f, 0.98f);
			ShotText.GetComponent<Text>().color = new Color(0.24f, 0.73f, 0.98f);
			ProgressImage.GetComponent<Image>().color = new Color(0.24f, 0.73f, 0.98f);
		}
	}

	void UpdateScore(){
		ScoreText.text = GameData.game_score.ToString ("000000");
	}

	public void CheckRoundProgress(){
		if (GameData.round_duck_number == 1) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed1.gameObject.SetActive (true);
					DuckUIProgressWhite1.gameObject.SetActive (false);	
				}
			} else {
				DuckUIProgressWhite1.gameObject.SetActive (true);
				DuckUIProgressRed1.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 2) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed2.gameObject.SetActive (true);
					DuckUIProgressWhite2.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite2.gameObject.SetActive (true);
				DuckUIProgressRed2.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 3) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed3.gameObject.SetActive (true);
					DuckUIProgressWhite3.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite3.gameObject.SetActive (true);
				DuckUIProgressRed3.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 4) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed4.gameObject.SetActive (true);
					DuckUIProgressWhite4.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite4.gameObject.SetActive (true);
				DuckUIProgressRed4.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 5) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed5.gameObject.SetActive (true);
					DuckUIProgressWhite5.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite5.gameObject.SetActive (true);
				DuckUIProgressRed5.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 6) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed6.gameObject.SetActive (true);
					DuckUIProgressWhite6.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite6.gameObject.SetActive (true);
				DuckUIProgressRed6.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 7) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed7.gameObject.SetActive (true);
					DuckUIProgressWhite7.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite7.gameObject.SetActive (true);
				DuckUIProgressRed7.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 8) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed8.gameObject.SetActive (true);
					DuckUIProgressWhite8.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite8.gameObject.SetActive (true);
				DuckUIProgressRed8.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 9) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed9.gameObject.SetActive (true);
					DuckUIProgressWhite9.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite9.gameObject.SetActive (true);
				DuckUIProgressRed9.gameObject.SetActive (false);
			}
		} else if (GameData.round_duck_number == 10) {
			if (!GameData.no_more_bullets) {
				if (GameData.duck_hit) {
					DuckUIProgressRed10.gameObject.SetActive (true);
					DuckUIProgressWhite10.gameObject.SetActive (false);
				}
			} else {
				DuckUIProgressWhite10.gameObject.SetActive (true);
				DuckUIProgressRed10.gameObject.SetActive (false);
			}
		}
	}

	void CheckCurrentBullets(){
		if (GameData.round_bullets == 3) {
			Bullet1.gameObject.SetActive (true);
			Bullet2.gameObject.SetActive (true);
			Bullet3.gameObject.SetActive (true);
		} else if (GameData.round_bullets == 2) {
			Bullet1.gameObject.SetActive (true);
			Bullet2.gameObject.SetActive (true);
			Bullet3.gameObject.SetActive (false);
		} else if (GameData.round_bullets == 1) {
			Bullet1.gameObject.SetActive (true);
			Bullet2.gameObject.SetActive (false);
			Bullet3.gameObject.SetActive (false);
		} if (GameData.round_bullets <= 0) {
			Bullet1.gameObject.SetActive (false);
			Bullet2.gameObject.SetActive (false);
			Bullet3.gameObject.SetActive (false);
		}
	}

	public void PauseGame(){
		PauseMenu.gameObject.SetActive (true);
		PauseMainPanel.gameObject.SetActive (true);
		Time.timeScale = 0;
	}

	public void UnpauseGame(){
		PauseMainPanel.gameObject.SetActive (false);
		Time.timeScale = 1;
		PauseMenu.gameObject.SetActive (false);
	}

	public void RestartGame(){
		PauseMainPanel.gameObject.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene ("game1");
	}

	public void GoToMainMenu(){
		PauseMainPanel.gameObject.SetActive (false);
		Time.timeScale = 1;
		SceneManager.LoadScene ("menu");
	}
}
