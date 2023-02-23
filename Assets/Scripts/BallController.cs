using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {
    public Camera mainCamera;
    public GameObject spotlight;

    private Transform Transform => transform;
    private Rigidbody Rigidbody => GetComponent<Rigidbody>();

    private void Update() {
        if (Gode.IsPaused) {
            halt();
            return;   
        }

        var position = Transform.position;

        var cameraPosition = mainCamera.transform.position;
        mainCamera.transform.position = new Vector3(
            position.x,
            cameraPosition.y,
            position.z
        );

        var spotlightPosition = spotlight.transform.position;
        spotlight.transform.position = new Vector3(
            position.x,
            spotlightPosition.y,
            position.z
        );

        if (isGrounded())
            roll();
    }

    private bool isGrounded() {
        var isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            out var hit,
            0.6f
        );

        if (isGrounded) {
            Debug.DrawRay(
                transform.position,
                Vector3.down,
                Color.red
            );

            return true;
        }

        Debug.DrawRay(
            transform.position,
            Vector3.down,
            Color.yellow
        );

        return false;
    }

    private void addForce(Vector3 direction, float magnitude = 3f) => Rigidbody.AddForce(
        direction * magnitude,
        ForceMode.Acceleration
    );

    private void roll() {
        if (Input.GetKey(KeyCode.W))
            addForce(Vector3.forward);

        if (Input.GetKey(KeyCode.A))
            addForce(Vector3.left);

        if (Input.GetKey(KeyCode.S))
            addForce(Vector3.back);

        if (Input.GetKey(KeyCode.D))
            addForce(Vector3.right);

        if (Input.GetKey(KeyCode.Space))
            halt();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            addForce(Vector3.up, 250f);
    }

    private void halt() {
        var velocity = Rigidbody.velocity;
        addForce(-velocity);
    }
}
