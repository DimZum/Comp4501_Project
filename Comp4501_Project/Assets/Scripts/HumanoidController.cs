using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidController : MonoBehaviour {

    public float lookRadius = 70f;

    [SerializeField] private Transform p_target;
    public Transform Target {
        get { return p_target; }
        set { this.p_target = value; }
    }

    NavMeshAgent agent;
    Camera cam;

    public bool isSelected;
    public LayerMask movementMask;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;

        isSelected = false;
    }

    // Update is called once per frame
    void Update() {
        // For testing animation
        if (Input.GetMouseButton(1)) {
            if (isSelected) {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 1000, movementMask)) {
                    agent.SetDestination(hit.point);
                }
            }
        }

        if (p_target != null) {
            float distance = Vector3.Distance(p_target.position, transform.position);

            if (distance <= lookRadius) {
                agent.SetDestination(p_target.position);

                if (distance <= agent.stoppingDistance) {
                    // Attack p_target
                    FaceTarget();
                }
            }
        }
    }

    void FaceTarget() {
        Vector3 direction = (p_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnMouseDown() {
        //if (!isSelected) { isSelected = true; }
        
        ToggleSelection();
    }

    public void ToggleSelection() {
        isSelected = !isSelected;
    }

    // Visual representation of look radius in editor
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
