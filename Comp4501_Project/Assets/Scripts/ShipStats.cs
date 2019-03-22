using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipStats {

    [SerializeField] private int p_maxHealth;
    public int MaxHealth {
        set { this.p_maxHealth = value; }
    }

    [SerializeField] private int p_currentHealth;
    public int CurrentHealth {
        get { return p_currentHealth; }
        set { this.p_currentHealth = value; }
    }

    [SerializeField] private int p_armor;
    public int Armor {
        get { return p_armor; }
        set { this.p_armor = value; }
    }

    [SerializeField] private int p_speed;
    public int Speed {
        get { return p_speed; }
        set { this.p_speed = value; }
    }

    private void Awake() {
        //p_currentHealth = maxHealth;
    }

    public void TakeDamage(int caliber) {
        float damage = (caliber + 20) / 3; //Base damage

        damage = Mathf.RoundToInt(damage);
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= (int)damage;

        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public void Die() {

    }
}
