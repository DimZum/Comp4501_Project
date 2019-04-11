using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMaster : MonoBehaviour {

    public SceneMaster sm;
    AudioManager am;

    [SerializeField] string gameOverSound = "GameOver";
    [SerializeField] string buttonPressedSound = "ButtonPress";
    [SerializeField] string buttonHoverSound = "ButtonHover";

    // Start is called before the first frame update
    void Start() {
        am = AudioManager.instance;

        am.PlaySound(gameOverSound);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void MainMenu() {
        PlayButtonPressedSound();
        sm.SceneToLoad(SceneMaster.mainMenu);
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
        am.PlaySound(buttonHoverSound);
    }

    void PlayButtonPressedSound() {
        am.PlaySound(buttonPressedSound);
    }
}
