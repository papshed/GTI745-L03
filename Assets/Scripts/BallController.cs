using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour {
    public Camera Camera;

    private Transform Transform => transform;
    private Rigidbody Rigidbody => GetComponent<Rigidbody>();

    public bool Grounded = false;

    private void Update() {
        Grounded = isGrounded();

        if (isGrounded())
            roll();

        Camera.transform.position = new Vector3(
            Transform.position.x,
            Camera.transform.position.y,
            Transform.position.z
        );
    }

    private bool isGrounded() {
        var isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            out var hit,
            0.55f
        );

        if (isGrounded) {
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.down) * hit.distance,
                Color.red
            );

            return true;
        }

        Debug.DrawRay(
            transform.position,
            transform.TransformDirection(Vector3.down) * Transform.position.y,
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
            addForce(Vector3.back);

        if (Input.GetKey(KeyCode.Space))
            halt();

        if (Input.GetKey(KeyCode.LeftShift))
            addForce(Vector3.up, 25f);
    }

    private void halt() {
        var velocity = Rigidbody.velocity;
        addForce(-velocity);
    }
}
