using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    #region Singleton
    public static GameMaster instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one GM instance in scene!");
            return;
        }

        instance = this;
    }
    #endregion

    public static bool isGameOver;
    public static bool isGamePaused;

    //public GameObject gamerOverUI;
    //public PauseMenu pauseMenu;

    public GameObject ShipYardUI;
    public GameObject ShipDesignerUI;

    public Player player, enemy;

    // Start is called before the first frame update
    void Start() {
        isGameOver = false;
        isGamePaused = false;

        player = new Player(0, 4);
        enemy = new Player(1, 0);
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
