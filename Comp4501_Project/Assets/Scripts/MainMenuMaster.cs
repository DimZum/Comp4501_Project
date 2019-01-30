using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMaster : MonoBehaviour {

    public SceneMaster sceneMaster;

    public void Play() {
        sceneMaster.SceneToLoad(SceneMaster.game);
    }

    public void Settings() {
        Debug.Log("Settings");
    }

    public void Quit() {
        // For use in editor
        if (UnityEditor.EditorApplication.isPlaying) {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        Application.Quit();
    }
}
