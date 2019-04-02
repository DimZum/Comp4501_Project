using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour {

    GameMaster gm;

    GameObject ship;

    public LayerMask movementMask;

    Camera cam;
    ShipMotor motor;

    public bool isSelected;

    // Start is called before the first frame update
    void Start() {
        gm = GameMaster.instance;

        ship = this.gameObject;

        cam = Camera.main;
        motor = GetComponent<ShipMotor>();

        isSelected = false;
    }

    // Update is called once per frame
    void Update() {
        GameObject go;
        if (EventSystem.current.IsPointerOverGameObject()) {
            go = EventSystem.current.gameObject;
        } else {
            go = null;
        }

        // Left mouse
        if (Input.GetMouseButton(1)) {
            if (isSelected) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 1000, movementMask)) {
                    if (hit.collider.gameObject.GetComponent<Ship>()) {

                    }
                    // Check if clicked gameobject is owned by the enemy
                    if (go) {
                        if (hit.collider.gameObject.GetComponent<Ship>().Owner != gm.player) {
                            motor.MoveToPoint(go.transform.position);
                        }
                    }

                    motor.MoveToPoint(hit.point);
                }
            }
        }
    }

    private void OnMouseDown() {
        //if (!isSelected) { isSelected = true; }

        ToggleSelection();
    }

    public void ToggleSelection() {
        isSelected = !isSelected;
    }
}
