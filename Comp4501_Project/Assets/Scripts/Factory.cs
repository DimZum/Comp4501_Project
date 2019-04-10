using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Stats))]
public class Factory : MonoBehaviour {

    GameMaster gm;
    ResourceManager rm;

    private Player p_owner;
    public Player Owner {
        get { return this.p_owner; }
        set { this.p_owner = value; }
    }

    public Stats stats;

    public GameObject factoryUI;
    public GameObject shipDesignerUI;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;
        rm = ResourceManager.instance;

        factoryUI = rm.factoryMenuUI;
        shipDesignerUI = rm.shipDesignerUI;

        stats.MaxHealth = 100;
        stats.Armor = 30;
        stats.Speed = 0;
    }

    // Update is called once per frame
    void Update() {
        if (stats.isSelected) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                stats.ToggleIsSelected();

                if (factoryUI.activeSelf) {
                    ToggleFactoryUI();
                }

                if (shipDesignerUI.activeSelf) {
                    ToggleShipDesignerUI();
                }
            }
        }
    }

    private void OnMouseDown() {
        //if (p_owner == gm.player) {
        if (true) {
            stats.ToggleIsSelected();

            ToggleFactoryUI();
        }
    }

    public void ToggleFactoryUI() {
        factoryUI.SetActive(!factoryUI.activeSelf);
    }

    public void ToggleShipDesignerUI() {
        shipDesignerUI.SetActive(!shipDesignerUI.activeSelf);
    }

    public void Destroy() {
        ToggleFactoryUI();
        stats.Die();
    }

    public void Design() {
        ToggleFactoryUI();
        ToggleShipDesignerUI();
    }
}
