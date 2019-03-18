using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour {

    private int maxHealth;
    private int p_currentHealth;
    public int CurrentHealth {
        get { return p_currentHealth; }
        set { this.p_currentHealth = value; }
    }

    private int p_armor;
    public int Armor {
        get { return p_armor; }
        set { this.p_armor = value; }
    }

    private int p_speed;
    public int Speed {
        get { return p_speed; }
        set { this.p_speed = value; }
    }

    private void Awake() {
        //p_currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        damage -= this.Armor;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public void Die() {

    }
}
