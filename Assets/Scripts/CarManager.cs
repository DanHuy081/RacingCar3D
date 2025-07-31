using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarManager : MonoBehaviour
{
    public float moveSpeed = 20f;     // Tốc độ di chuyển tiến/lùi
    public float turnSpeed = 100f;    // Tốc độ rẽ trái/phải

    private Rigidbody rb;
    private float verticalInput;
    private float horizontalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");   // W/S hoặc ↑/↓
        horizontalInput = Input.GetAxis("Horizontal"); // A/D hoặc ←/→
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 moveDirection = -transform.right * verticalInput * moveSpeed;
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }

    private void Turn()
    {
        float turnAmount = horizontalInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
