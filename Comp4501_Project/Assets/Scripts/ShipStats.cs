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
        float damage = (caliber*caliber + 20) / 10; //Base damage
        float randomValue = (Random.value + Random.value + Random.value + Random.value) * caliber /2.00f; 
        // get a random value between 0 and twice the caliber.
        // the random value is trend to the center.
        if(randomValue < Armor * 0.5)
        {
            //The shell didn't penetrate through so did no damage
            damage = 0;
        }else if (randomValue < Armor)
        {
            //The shell hits non effective area and did half damage
            damage /= 2;
        }else if (randomValue < Armor * 1.5f)
        {
            //The shell hits regular place and deal normal damage
        }
        else
        {
            //The shell hits very hard and penetrate all the way to the middle of ship body, deal massive damage.
            damage *= randomValue / Armor;
        }


        CurrentHealth -= Mathf.RoundToInt(damage);
        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public void Die() {

    }
}
