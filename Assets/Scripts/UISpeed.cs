using TMPro;
using UnityEngine;

public class UISpeed : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private Transform carTransform;

    private bool skipFrame = false;
    private Vector3 lastPosition;
    private float displaySpeed = 0f;

    void Start()
    {
        lastPosition = carTransform.position;
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            skipFrame = true; // đánh dấu khi đang pause
            return;
        }

        if (skipFrame)
        {
            lastPosition = carTransform.position; // cập nhật lại ngay khi resume
            skipFrame = false;
            return;
        }


        float distance = Vector3.Distance(carTransform.position, lastPosition);
        float speed = (distance / Time.deltaTime) * 3.6f;

        displaySpeed = Mathf.Lerp(displaySpeed, speed, Time.deltaTime * 5f);
        speedText.text = $"Speed: {Mathf.RoundToInt(displaySpeed)} km/h";

        lastPosition = carTransform.position;
    }
}
