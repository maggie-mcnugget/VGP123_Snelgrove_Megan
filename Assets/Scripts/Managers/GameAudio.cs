using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip gameOverMusic;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip enemydamageSFX;
    [SerializeField] private AudioClip throwSFX;
    [SerializeField] private AudioClip pickupSFX;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Music source (existing AudioSource)
        musicSource = GetComponent<AudioSource>();

        // SFX source (separate so sounds can overlap)
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        AudioClip target = null;

        if (sceneName == "Title") target = titleMusic;
        else if (sceneName == "Game") target = gameMusic;
        else if (sceneName == "GameOver") target = gameOverMusic;

        if (musicSource.clip != target)
        {
            musicSource.Stop();
            musicSource.clip = target;

            if (target != null)
                musicSource.Play();
        }
    }

    // 🎵 GENERIC SFX FUNCTION
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // 🎯 OPTIONAL: named helpers (easier to use)
    public void PlayJump() => PlaySFX(jumpSFX);
    public void PlayDamage() => PlaySFX(damageSFX);
    public void PlayShoot() => PlaySFX(shootSFX);
    public void PlayEnemyDamage() => PlaySFX(enemydamageSFX);
    public void PlayThrow() => PlaySFX(throwSFX);
    public void PlayPickup() => PlaySFX(pickupSFX);
}