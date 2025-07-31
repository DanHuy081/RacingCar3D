//using UnityEngine;

//public class CarHandler : MonoBehaviour
//{
//    [SerializeField] private Rigidbody rb;
//    [SerializeField] private Transform gameModel;

//    [Header("Speed Settings")]
//    [SerializeField] private float maxForwardVelocity = 40f; // tốc độ tối đa
//    [SerializeField] private float accelerationForce = 1000f; // lực tăng tốc
//    [SerializeField] private float speedIncreaseRate = 10f; // tốc độ tăng dần mỗi giây
//    private float currentMaxSpeed = 20f; // tốc độ ban đầu mạnh hơn

//    [Header("Steering Settings")]
//    [SerializeField] private float maxSteerVelocity = 7f;
//    [SerializeField] private float steeringForce = 600f;

//    private Vector2 input = Vector2.zero;

//    void Start()
//    {
//        if (rb == null) rb = GetComponent<Rigidbody>();
//        currentMaxSpeed = 20f;
//        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
//    }

//    void Update()
//    {
//        if (gameModel != null)
//        {
//            float targetTilt = rb.velocity.x * 1.0f; // nhỏ hơn
//            Quaternion targetRotation = Quaternion.Euler(0, targetTilt, 0);
//            gameModel.transform.localRotation = Quaternion.Lerp(gameModel.transform.localRotation, targetRotation, Time.deltaTime * 5f);
//        }
//    }

//    void FixedUpdate()
//    {
//        StickToGround();
//        // Luôn tăng tốc về phía trước
//        AccelerateForward();

//        // Tăng tốc độ giới hạn theo thời gian
//        currentMaxSpeed += speedIncreaseRate * Time.fixedDeltaTime;
//        currentMaxSpeed = Mathf.Clamp(currentMaxSpeed, 0f, maxForwardVelocity);

//        // Điều khiển trái/phải
//        Steer();

//        // Hạn chế trượt ngang khi không điều khiển
//        if (Mathf.Abs(input.x) < 0.05f)
//        {
//            rb.velocity = Vector3.Lerp(rb.velocity,
//                new Vector3(0, rb.velocity.y, rb.velocity.z),
//                Time.fixedDeltaTime * 3f);
//        }
//    }

//    void AccelerateForward()
//    {
//        // Nếu tốc độ chưa tới giới hạn → tăng lực
//        if (rb.velocity.magnitude < currentMaxSpeed)
//        {
//            rb.AddForce(transform.forward * accelerationForce * Time.fixedDeltaTime, ForceMode.Force);
//        }
//    }

//    void Steer()
//    {
//        if (Mathf.Abs(input.x) > 0.05f)
//        {
//            float steerTorque = input.x * steeringForce * Time.fixedDeltaTime;
//            rb.AddTorque(Vector3.up * steerTorque, ForceMode.Force);

//        }
//    }

//    public void SetInput(Vector2 inputVector)
//    {
//        input = inputVector.normalized;
//    }

//    void StickToGround()
//    {
//        RaycastHit hit;
//        if (Physics.Raycast(transform.position + Vector3.up * 1f, Vector3.down, out hit, 3f))
//        {
//            Vector3 normal = hit.normal;
//            Quaternion groundRotation = Quaternion.FromToRotation(transform.up, normal) * transform.rotation;
//            transform.rotation = Quaternion.Lerp(transform.rotation, groundRotation, Time.fixedDeltaTime * 5f);
//        }
//    }

//}
