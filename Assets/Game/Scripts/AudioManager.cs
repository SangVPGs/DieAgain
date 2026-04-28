using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip bgm;
    public AudioClip jump;
    public AudioClip die;
    public AudioClip goal;
    public AudioClip lose;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayJump()
    {
        sfxSource.PlayOneShot(jump);
    }

    public void PlayDie()
    {
        sfxSource.PlayOneShot(die);
    }

    public void PlayGoal()
    {
        StopBGM();
        sfxSource.PlayOneShot(goal);
    }

    public void PlayLose()
    {
        StopBGM();
        sfxSource.PlayOneShot(lose);
    }
}