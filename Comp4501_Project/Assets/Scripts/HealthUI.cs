using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Stats))]
public class HealthUI : MonoBehaviour {

    Stats stats;

    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5;

    float lastMadeVisibleTime;

    Transform ui;
    Image healthSlider; // Green part of health bar
    Transform cam;

	// Use this for initialization
	void Start () {
        stats = GetComponent<Stats>();

        cam = Camera.main.transform;

		foreach (Canvas c in FindObjectsOfType<Canvas>()) {
            if (c.renderMode == RenderMode.WorldSpace) {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }

        stats.OnHealthChanged += OnHealthChanged;
	}

    // Health bar shows only when unit is attacked
    void OnHealthChanged(int maxHealth, int currentHealth) {
        if (ui != null) {
            ui.gameObject.SetActive(true);
            lastMadeVisibleTime = Time.time;

            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;

            if (currentHealth <= 0) {
                Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        if (ui != null) {
            ui.position = target.position;
            ui.forward = -cam.forward;

            if (Time.time - lastMadeVisibleTime > visibleTime) {
                ui.gameObject.SetActive(false);
            }
        }
	}
}
