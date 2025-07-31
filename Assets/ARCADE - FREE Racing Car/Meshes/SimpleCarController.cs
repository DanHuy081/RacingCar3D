using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;

    public WheelCollider FrontLeftWheel;
    public WheelCollider FrontRightWheel;
    public WheelCollider RearLeftWheel;
    public WheelCollider RearRightWheel;

    public Transform wheelFLTransform;
    public Transform wheelFRTransform;
    public Transform wheelRLTransform;
    public Transform wheelRRTransform;

    private float motorInput;
    private float steeringInput;

    void FixedUpdate()
    {
        motorInput = Input.GetAxis("Vertical") * maxMotorTorque;
        steeringInput = Input.GetAxis("Horizontal") * maxSteeringAngle;

        // Steering (chỉ bánh trước)
        FrontLeftWheel.steerAngle = steeringInput;
        FrontRightWheel.steerAngle = steeringInput;

        // Motor (chỉ bánh sau)
        RearLeftWheel.motorTorque = motorInput;
        RearRightWheel.motorTorque = motorInput;

        UpdateWheelPose(FrontLeftWheel, wheelFLTransform);
        UpdateWheelPose(FrontRightWheel, wheelFRTransform);
        UpdateWheelPose(RearLeftWheel, wheelRLTransform);
        UpdateWheelPose(RearRightWheel, wheelRRTransform);
    }

    void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
