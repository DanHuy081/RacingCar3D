// RotateCar.cs
using UnityEngine;

public class RotateCar : MonoBehaviour
{
    public float rotationSpeed = 20f; // Tốc độ quay

    void Update()
    {
        // Quay quanh trục Y mỗi frame
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
