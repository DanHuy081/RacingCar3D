using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class RaceManager : MonoBehaviour
{
    

    public TextMeshProUGUI countdownText;
    public GameObject player;
    public GameObject[] aiCars;

    

    private bool raceStarted = false;
    private bool raceFinished = false;

    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "GO!";
        raceStarted = true;

        // Gọi StartRace() ở player và AI
        player.GetComponent<CarController>().enabled = true;
        foreach (var ai in aiCars)
        {
            ai.GetComponent<AICarController>().enabled = true;
        }

        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

   

}
