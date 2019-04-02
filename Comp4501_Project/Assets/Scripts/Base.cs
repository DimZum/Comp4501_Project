using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Base : MonoBehaviour {

    GameMaster gm;

    private Player p_owner;
    public Player Owner {
        get { return this.p_owner; }
        set { this.p_owner = value; }
    }

    public Stats stats;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;
        stats = GetComponent<Stats>();

        stats.MaxHealth = 200;
        stats.Armor = 50;
        stats.Speed = 0;
    }

    // Update is called once per frame
    void Update() {
        if (stats.isSelected) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                stats.ToggleIsSelected();
            }
        }
    }

    private void OnMouseDown() {
        if (p_owner == gm.player) {
            stats.ToggleIsSelected();

            // Toggle menu UI
        }
    }

    // Destroy base - forfeit the game
    public void Surrender() {
        stats.Die();
        gm.EndGame();
    }
}
