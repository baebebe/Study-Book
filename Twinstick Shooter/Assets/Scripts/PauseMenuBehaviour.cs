using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuBehaviour : MainMenuBehaviour {
	public static bool isPaused;
	public GameObject pauseMenu;
	public GameObject optionsMenu;

	// Use this for initialization
	void Start () {
		isPaused = false;	
		pauseMenu.SetActive(false);
		optionsMenu.SetActive(false);

		UpdateQualityLabel();
		UpdateVolumeLabel();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("escape")){
			if(!optionsMenu.activeInHierarchy){
				// false가 true가 되거나 그 반대이거나
				isPaused = !isPaused;
				// isPaused가 true라면 0, 아니면 1
				Time.timeScale = (isPaused) ? 0 : 1;
				pauseMenu.SetActive(isPaused);
			}else{
				OpenPauseMenu();
			}
		}
	}

	public void ResumeGame(){
		isPaused = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

	public void RestartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void IncreaseQuality(){
		QualitySettings.IncreaseLevel();
		UpdateQualityLabel();
	}

	public void DecreaseQuality(){
		QualitySettings.DecreaseLevel();
		UpdateQualityLabel();
	}

	public void SetVolume(float value){
		AudioListener.volume = value;
		UpdateVolumeLabel();
	}

	private void UpdateQualityLabel(){
		int currentQuality = QualitySettings.GetQualityLevel();
		string qualityName = QualitySettings.names[currentQuality];

		optionsMenu.transform.FindChild("QualityLevel").GetComponent<UnityEngine.UI.Text>().text = "Quality Level - " + qualityName;
	}

	private void UpdateVolumeLabel(){
		optionsMenu.transform.FindChild("MasterVolume").GetComponent<UnityEngine.UI.Text>().text = "Master Volume - " + (AudioListener.volume * 100).ToString("f2") + "%";
	}

	public void OpenOptions(){
		optionsMenu.SetActive(true);
		pauseMenu.SetActive(false);
	}

	public void OpenPauseMenu(){
		optionsMenu.SetActive(false);
		pauseMenu.SetActive(true);
	}
}
