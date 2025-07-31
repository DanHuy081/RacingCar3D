using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject btnSoundOn;
    public GameObject btnSoundOff;

    private const string SOUND_KEY = "soundMuted"; // 0 = bật, 1 = tắt

    void Start()
    {
        // Đọc trạng thái từ PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt(SOUND_KEY, 0) == 1;

        musicSource.mute = isMuted;
        btnSoundOn.SetActive(!isMuted);
        btnSoundOff.SetActive(isMuted);
    }

    public void TurnSoundOff()
    {
        musicSource.mute = true;
        btnSoundOn.SetActive(false);
        btnSoundOff.SetActive(true);

        PlayerPrefs.SetInt(SOUND_KEY, 1);
        PlayerPrefs.Save();
    }

    public void TurnSoundOn()
    {
        musicSource.mute = false;
        btnSoundOn.SetActive(true);
        btnSoundOff.SetActive(false);

        PlayerPrefs.SetInt(SOUND_KEY, 0);
        PlayerPrefs.Save();
    }
}
