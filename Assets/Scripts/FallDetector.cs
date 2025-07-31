using UnityEngine;

public class FallDetector : MonoBehaviour
{
    [SerializeField] private GameObject loseUI;
    [SerializeField] private float fallThreshold = -10f; // Nếu xe rơi dưới Y = -10 thì thua

    private bool hasLost = false;

    void Start()
    {
        if (loseUI != null)
            loseUI.SetActive(false); // Ẩn UI "You Lose" lúc bắt đầu
    }

    void Update()
    {
        if (hasLost) return;

        if (transform.position.y < fallThreshold)
        {
            loseUI.SetActive(true);
            Time.timeScale = 0f; // Dừng game
            hasLost = true;
        }

        loseUI.SetActive(false);
    }
}
