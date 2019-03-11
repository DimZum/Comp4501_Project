using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    //public GameObject gamerOverUI;

    //public PauseMenu pauseMenu; 

    public static GameObject ShipYardUI;
    public static GameObject ShipDesignerUI;

    public static bool isGameOver;
    public static bool isGamePaused;

    public static Player
        player = new Player(0, 4),
        enemy = new Player(1, 0);

    // Start is called before the first frame update
    void Start() {
        isGameOver = false;
        isGamePaused = false;

        ShipYardUI = GameObject.Find("ShipYardUI");
        ShipDesignerUI = GameObject.Find("ShipDesignerUI");
        ShipYardUI.SetActive(false);
        ShipDesignerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player.Update();
        enemy.Update();
        if (isGameOver) { return; }
    }

    public void EndGame() {
        isGameOver = true;
    }
}
