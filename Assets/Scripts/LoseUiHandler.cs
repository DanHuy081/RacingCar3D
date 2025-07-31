using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUIHandler : MonoBehaviour
{
    

    
    public void OnPlayAgainClicked()
    {
        Time.timeScale = 1f; // Khôi phục thời gian nếu đã dừng
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại scene hiện tại

    }
}
