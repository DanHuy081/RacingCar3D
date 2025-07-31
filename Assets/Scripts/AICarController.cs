using UnityEngine;

public class AICarController : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float turnSpeed = 5f;
    public float waypointReachDistance = 2f;

    private int currentIndex = 0;
    public Transform waypointParent;
    private Transform[] waypoints;

    void Start()
    {
        int count = waypointParent.childCount;
        waypoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
        this.enabled = false;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentIndex];

        // Move
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        // Check if reached
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        if (distance < waypointReachDistance)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
                currentIndex = 0; // Lặp lại đường đua
        }

        float angle = Vector3.Angle(transform.forward, direction);
        float speedMultiplier = Mathf.Clamp01(1f - (angle / 90f)); // càng thẳng càng nhanh

        float adjustedSpeed = moveSpeed * (1f + speedMultiplier * 0.25f); // +50% khi thẳng
        transform.position += direction * adjustedSpeed * Time.deltaTime;
    }
}
