using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ cho trường hợp này tồn tại liên tục
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một trường hợp tồn tại
        }
    }

    public void PlayThisSound(string clipName, float volumeMultiplier)
    {
        // Tải AudioClip từ thư mục Resources/Sounds
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + clipName);

        if (clip != null)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = Mathf.Clamp(volumeMultiplier, 0f, 1f); // Đặt âm lượng an toàn giữa 0 và 1
            audioSource.PlayOneShot(clip);
            Destroy(audioSource, clip.length); // Dọn dẹp AudioSource sau khi hoàn tất
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }
}
