using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationCounterUI : MonoBehaviour {
    GameMaster gm;

    public Text text;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;
        
    }

    // Update is called once per frame
    void Update() {
        text.text = "Population: " + (int)gm.player.ManPower;
    }
}
