using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Humanoid : MonoBehaviour {

    public Stats stats;
    
    // Start is called before the first frame update
    void Start() {
        stats.MaxHealth = 50;
        stats.Armor = 15;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
