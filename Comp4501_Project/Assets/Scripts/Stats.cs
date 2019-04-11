using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour {

    AudioManager am;

    public NavMeshAgent agent;

    [SerializeField] private int p_maxHealth;
    public int MaxHealth {
        get { return this.p_maxHealth; }
        set { this.p_maxHealth = value; }
    }

    [SerializeField] private int p_currentHealth;
    public int CurrentHealth {
        get { return this.p_currentHealth; }
        set { this.p_currentHealth = value; }
    }

    [SerializeField] private int p_armor;
    public int Armor {
        get { return this.p_armor; }
        set { this.p_armor = value; }
    }

    [SerializeField] private int p_speed;
    public int Speed {
        get { return p_speed; }
        set { this.p_speed = value; }
    }

    public event System.Action<int, int> OnHealthChanged;

    public bool isSelected;

    [SerializeField] string explosionSound = "Explosion";

    private void Awake() {

        isSelected = false;
    }

    private void Start() {
        am = AudioManager.instance;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            Die();
        }
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

        if (OnHealthChanged != null) {
            OnHealthChanged(MaxHealth, CurrentHealth);
        }

        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public void ToggleIsSelected() {
        isSelected = !isSelected;
    }

    public void Die() {
        am.PlaySound(explosionSound);
        GameObject.Destroy(this.gameObject, .5f);
    }
}
