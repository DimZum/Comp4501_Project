using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class ShipyardManager : MonoBehaviour {

    GameMaster gm;

    private Player p_owner;
    public Player Owner {
        get { return this.p_owner; }
        set { this.p_owner = value; }
    }

    public Stats stats;

    public GameObject shipyardUI;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;
        stats.GetComponent<Stats>();

        stats.MaxHealth = 100;
        stats.Armor = 30;
        stats.Speed = 0;
    }

    // Update is called once per frame
    void Update() {
        if (stats.isSelected) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                stats.isSelected = false;
            }
        }
    }

    private void OnMouseDown() {
        if (p_owner == gm.player) {
            stats.ToggleIsSelected();

            ToggleShipyardUI();
        }
    }
    
    // Opens shipyard UI
    public void ToggleShipyardUI() {
        shipyardUI.SetActive(!shipyardUI.activeSelf);
    }
}
