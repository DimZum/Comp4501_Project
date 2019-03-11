﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private Vector3 originalPos;
    private bool isCameraLocked = false;

    public float panSpeed = 150f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 10f;

    private int scrollMultiplier = 250;
    private float minY = 50f;
    private float maxY = 200f;

    // Start is called before the first frame update
    void Start() {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (GameMaster.isGameOver) { return; }

        if (isCameraLocked) { return; }

        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position = originalPos;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            isCameraLocked = !isCameraLocked;
        }

        #region Rotated on y-axis by 90
        if (Input.GetKey("d") /*|| Input.mousePosition.y >= Screen.height - panBorderThickness*/) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("w") /*|| Input.mousePosition.x <= panBorderThickness*/) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") /*|| Input.mousePosition.y <= panBorderThickness*/) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") /*|| Input.mousePosition.x >= Screen.width - panBorderThickness*/) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollMultiplier * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}