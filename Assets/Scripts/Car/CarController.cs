using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float moveSpeed = 100f;         // Tốc độ bình thường
    public float boostedSpeed = 150f;      // Tốc độ khi boost
    public float turnSpeed = 100f;         // Tốc độ rẽ

    private float currentSpeed;
    private Rigidbody rb;
    private float verticalInput;
    private float horizontalInput;

    public Action<CarController> OnPlayerCrashed { get; internal set; }

    private bool isBoosting = false;
    private bool isCooldown = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed;
        this.enabled = false; // Bật thủ công từ nơi khác khi cần
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && !isBoosting && !isCooldown)
        {
            StartCoroutine(SpeedBoost(4f));  // Boost trong 4 giây
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 moveDirection = transform.right * verticalInput * currentSpeed;
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }

    private void Turn()
    {
        float turnAmount = horizontalInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    public IEnumerator SpeedBoost(float duration = 4f)
    {
        isBoosting = true;
        currentSpeed = boostedSpeed;
        Debug.Log("🔥 Speed Boost Activated!");

        yield return new WaitForSeconds(duration);

        currentSpeed = moveSpeed;
        isBoosting = false;
        isCooldown = true;

        Debug.Log("⏹️ Speed Boost Ended. Cooldown started.");

        yield return new WaitForSeconds(duration); // Hồi sau 4 giây

        isCooldown = false;
        Debug.Log("✅ Boost Ready Again!");
    }
}
