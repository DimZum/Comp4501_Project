using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceCounterUI : MonoBehaviour {

    GameMaster gm;

    public Text text;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;

        StartCoroutine(IncreaseExperience());
    }

    // Update is called once per frame
    void Update() {
        text.text = "Experience: " + gm.player.Exp;
    }

    IEnumerator IncreaseExperience() {
        while (!GameMaster.isGameOver) {
            yield return new WaitForSeconds(1);
            Debug.Log("EXP");
            gm.player.Exp++;
        }
    }
}
