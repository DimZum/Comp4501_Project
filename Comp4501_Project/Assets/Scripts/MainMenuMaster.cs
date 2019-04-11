using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMaster : MonoBehaviour {

    [SerializeField] string hoverOverSound = "ButtonHover";
    [SerializeField] string buttonPressedSound = "ButtonPress";

    public SceneMaster sceneMaster;

    AudioManager am;

    private void Start() {
        am = AudioManager.instance;
    }

    public void StartGame() {
        PlayButtonPressedSound();
        sceneMaster.SceneToLoad(SceneMaster.game);
    }

    public void HowToPlay() {
        PlayButtonPressedSound();
        Debug.Log("HowToPlay");
    }

    public void Settings() {
        PlayButtonPressedSound();
        Debug.Log("Settings");
    }

    public void Quit() {
        PlayButtonPressedSound();
        // For use in editor
        if (UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        Application.Quit();
    }

    public void OnMouseOver() {
        am.PlaySound(hoverOverSound);
    }

    void PlayButtonPressedSound() {
        am.PlaySound(buttonPressedSound);
    }
}
