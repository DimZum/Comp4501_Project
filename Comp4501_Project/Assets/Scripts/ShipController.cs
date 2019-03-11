using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour {

    public LayerMask movementMask;

    Camera cam;
    ShipMotor motor;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        motor = GetComponent<ShipMotor>();
    }

    // Update is called once per frame
    void Update() {
        /*if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }*/

        // Left mouse
        if (Input.GetMouseButton(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, movementMask)) {
                motor.MoveToPoint(hit.point);
            }
        }
    }
}
