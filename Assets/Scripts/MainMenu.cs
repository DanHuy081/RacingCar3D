using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string carSelectionSceneName = "ChooseCar"; // Tên scene chọn xe

    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene(carSelectionSceneName);
    }
}
