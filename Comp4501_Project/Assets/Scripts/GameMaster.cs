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

    public SceneMaster sm;

    public Camera mainCam;

    public static bool isGameOver;
    public static bool isGamePaused;

    //public GameObject gamerOverUI;
    //public PauseMenu pauseMenu;

    public GameObject ShipYardUI;
    public GameObject ShipDesignerUI;

    public Player player, enemy;

    public Transform playerSpawnpoint;
    public Transform[] enemySpawnpoints;

    // Start is called before the first frame update
    void Start() {
        mainCam.transform.position = new Vector3(playerSpawnpoint.position.x + 100, mainCam.transform.position.y, playerSpawnpoint.position.z - 200);

        isGameOver = false;
        isGamePaused = false;

        player = new Player(0, 4, playerSpawnpoint.position);
        enemy = new Player(1, 0, enemySpawnpoints[Random.Range(0, enemySpawnpoints.Length)].position);
    }

    // Update is called once per frame
    void Update() {
        if (isGameOver) { return; }

        player.UpdatePlayer();
        enemy.UpdatePlayer();
    }

    public void EndGame() {
        isGameOver = true;
        Debug.Log("Your base has been destroyed");
        sm.SceneToLoad(SceneMaster.gameover);
    }
}
