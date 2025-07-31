using UnityEngine;

public class RaceCarController : MonoBehaviour
{
    public GameObject[] cars;

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        for (int i = 0; i < cars.Length; i++)
        {
            bool isSelected = (i == selectedIndex);

            if (cars[i] != null)
            {
                cars[i].SetActive(isSelected);

                // Nếu có CarController thì bật/tắt tương ứng
                CarController controller = cars[i].GetComponent<CarController>();
                if (controller != null)
                    controller.enabled = isSelected;
            }
        }
    }
}
