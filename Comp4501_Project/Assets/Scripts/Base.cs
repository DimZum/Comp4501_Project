using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Stats))]
public class Base : MonoBehaviour {

    GameMaster gm;
    ResourceManager rm;

    private Player p_owner;
    public Player Owner {
        get { return this.p_owner; }
        set { this.p_owner = value; }
    }

    public Stats stats;

    public GameObject baseUI;
    public Text info;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;
        rm = ResourceManager.instance;

        baseUI = rm.baseUI;
        info = rm.baseInfo;

        stats.MaxHealth = 200;
        stats.CurrentHealth = stats.MaxHealth;
        stats.Armor = 50;
        stats.Speed = 0;
    }

    // Update is called once per frame
    void Update() {
        if (stats.isSelected) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                stats.ToggleIsSelected();
                ToggleBaseUI();
            }

            info.text = "Owner:  " + Owner.ID + "\n"
                + "Health:  " + stats.CurrentHealth + "/" + stats.MaxHealth + "\n"
                + "Armor:  " + stats.Armor;
        }
    }

    private void OnMouseDown() {
        //if (p_owner == gm.player) {
        if (true) {
            stats.ToggleIsSelected();

            ToggleBaseUI();
        }
    }

    public void ToggleBaseUI() {
        baseUI.SetActive(!baseUI.activeSelf);
    }

    // Destroy base - forfeit the game
    public void Surrender() {
        ToggleBaseUI();

        stats.Die();
        gm.EndGame();
    }
}
