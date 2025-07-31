using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour
{
    public GameObject[] cars;
    public int currentIndex = 0;

    public TextMeshProUGUI countText;
    public Button nextButton;
    public Button previousButton;
    public Button selectButton;

    // 🆕 Thêm hiệu ứng âm thanh
    public AudioSource sfxSource;
    public AudioClip switchSound;

    void Start()
    {
        if (cars == null || cars.Length == 0) return;

        if (PlayerPrefs.HasKey("SelectedCarIndex"))
            currentIndex = PlayerPrefs.GetInt("SelectedCarIndex");

        if (nextButton != null) nextButton.onClick.AddListener(NextCar);
        if (previousButton != null) previousButton.onClick.AddListener(PreviousCar);
        if (selectButton != null) selectButton.onClick.AddListener(OnSelectCar);

        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i] != null)
                cars[i].SetActive(i == currentIndex);
        }

        UpdateCountText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextCar();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousCar();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnSelectCar();
        }
    }

    public void NextCar()
    {
        if (!IsValid()) return;

        if (cars[currentIndex] != null)
            cars[currentIndex].SetActive(false);

        currentIndex = (currentIndex + 1) % cars.Length;
        ShowCar(currentIndex);

        PlaySwitchSound(); // 🆕
    }

    public void PreviousCar()
    {
        if (!IsValid()) return;

        if (cars[currentIndex] != null)
            cars[currentIndex].SetActive(false);

        currentIndex = (currentIndex - 1 + cars.Length) % cars.Length;
        ShowCar(currentIndex);

        PlaySwitchSound(); // 🆕
    }

    private void ShowCar(int index)
    {
        if (cars[index] != null)
        {
            cars[index].SetActive(true);
            UpdateCountText();

            PlayerPrefs.SetInt("SelectedCarIndex", currentIndex);
            PlayerPrefs.Save();
        }
    }

    public void OnSelectCar()
    {
        PlaySwitchSound(); // 🆕 Phát âm thanh khi nhấn Select

        PlayerPrefs.SetInt("SelectedCarIndex", currentIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene("ChooseTerrain"); // Đảm bảo scene đã nằm trong Build Settings
    }

    private void UpdateCountText()
    {
        if (countText != null)
            countText.text = $"{currentIndex + 1}/{cars.Length}";
    }

    private bool IsValid()
    {
        return cars != null && cars.Length > 0;
    }

    // 🆕 Hàm phát âm thanh
    private void PlaySwitchSound()
    {
        if (sfxSource != null && switchSound != null)
        {
            sfxSource.PlayOneShot(switchSound);
        }
    }
}
