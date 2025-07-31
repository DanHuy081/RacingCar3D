using UnityEngine;

public class Car : MonoBehaviour
{
    public WheelCollider FrontLeftWheel;
    public WheelCollider FrontRightWheel;
    public WheelCollider RearLeftWheel;
    public WheelCollider RearRightWheel;

    public Transform FL_Collider;
    public Transform FR_Collider;
    public Transform RL_Collider;
    public Transform RR_Collider;

    public float motorForce = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;

    private float currentSteerAngle;
    private float currentBrakeForce;
    private float currentMotorForce;

    void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    void HandleMotor()
    {
        float verticalInput = Input.GetAxis("Vertical");
        currentMotorForce = verticalInput * motorForce;

        FrontLeftWheel.motorTorque = currentMotorForce;
        FrontRightWheel.motorTorque = currentMotorForce;

        currentBrakeForce = Input.GetKey(KeyCode.Space) ? brakeForce : 0f;

        ApplyBrake(currentBrakeForce);
    }

    void ApplyBrake(float brakeForce)
    {
        FrontLeftWheel.brakeTorque = brakeForce;
        FrontRightWheel.brakeTorque = brakeForce;
        RearLeftWheel.brakeTorque = brakeForce;
        RearRightWheel.brakeTorque = brakeForce;
    }

    void HandleSteering()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        currentSteerAngle = horizontalInput * maxSteerAngle;

        FrontLeftWheel.steerAngle = currentSteerAngle;
        FrontRightWheel.steerAngle = currentSteerAngle;
    }

    void UpdateWheels()
    {
        UpdateWheelPose(FrontLeftWheel, FL_Collider);
        UpdateWheelPose(FrontRightWheel, FR_Collider);
        UpdateWheelPose(RearLeftWheel, RL_Collider);
        UpdateWheelPose(RearRightWheel, RR_Collider);
    }

    void UpdateWheelPose(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
