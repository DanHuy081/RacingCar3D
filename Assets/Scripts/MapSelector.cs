
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    public AudioSource sfxSource;     // Dùng chung AudioSource với SoundToggle
    public AudioClip clickSound;      // Dùng chung file clickSound

    private string pendingScene;

    public void LoadMap(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            PlayClickSound();
            pendingScene = sceneName;
            Invoke(nameof(DelayedLoad), 0.3f); // Delay để kịp phát âm thanh
        }
        else
        {
            Debug.LogWarning("Tên scene bị null hoặc rỗng!");
        }
    }

    private void DelayedLoad()
    {
        SceneManager.LoadScene(pendingScene);
    }

    private void PlayClickSound()
    {
        if (sfxSource != null && clickSound != null)
        {
            sfxSource.PlayOneShot(clickSound);
        }
    }
}
