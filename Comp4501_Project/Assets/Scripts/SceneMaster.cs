// Script contains some code taken from Brackey's 3D Tower Defense Tutorial

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMaster : MonoBehaviour {

    // Scene names
    public static string mainMenu = "MainMenu";
    public static string howToPlay = "HowToPlay";
    public static string game = "Game";
    public static string options = "Settings";

    public Image img;
    public AnimationCurve curve;

    private float fadeInSpeed = .5f;
    private float fadeOutSpeed = .75f;

    void Start() {
        StartCoroutine(FadeIn());
    }

    public void SceneToLoad(string scene) {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn() {
        float t = 1f;

        while (t > 0f) {
            t -= Time.deltaTime * fadeInSpeed;
            float alpha = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, alpha);

            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene) {
        float t = 0f;

        while (t < 1f) {
            t += Time.deltaTime * fadeOutSpeed;
            float alpha = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, alpha);

            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
