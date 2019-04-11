using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    #region Singleton
    public static ResourceManager instance;
    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one RM instance in scene!");
            return;
        }

        instance = this;
    }
    #endregion

    // GameObjects
    public GameObject baseUI;
    public Text baseInfo;

    public GameObject shipyardMenuUI;
    public GameObject shipyardUI;
    public Text shipyardInfo;

    public GameObject factoryMenuUI;
    public GameObject shipDesignerUI;
    public Text factoryInfo;
    
    // Ships
    public GameObject scoutPrefab;
    public GameObject destroyerPrefab;
    public GameObject lightCruiserPrefab;
    public GameObject heavyCruiserPrefab;
    public GameObject battleCruiserPrefab;
    public GameObject battleshipPrefab;
    public GameObject dreadnoughtPrefab;

    // Buildings
    public GameObject basePrefab;
    public GameObject shipyardPrefab;
    public GameObject factoryPrefab;

    // Humanoid
    public GameObject humanoidPrefab;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public GameObject GetShipPrefab(Constants.ShipClass shipToBuild) {

        if (shipToBuild == Constants.ShipClass.Dreadnought) { return dreadnoughtPrefab; }
        else if (shipToBuild == Constants.ShipClass.Battleship) { return battleshipPrefab; }
        else if (shipToBuild == Constants.ShipClass.BattleCruiser) { return battleCruiserPrefab; }
        else if (shipToBuild == Constants.ShipClass.HeavyCruiser) { return heavyCruiserPrefab; }
        else if (shipToBuild == Constants.ShipClass.LightCruiser) { return lightCruiserPrefab; }
        else if (shipToBuild == Constants.ShipClass.Destroyer) { return destroyerPrefab; }
        else { return scoutPrefab; }
    }
}
