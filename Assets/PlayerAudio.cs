using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] bubblePops;
    [SerializeField]
    AudioClip strongPop;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    private void Start()
    {
        PlayStrongPop();
    }

    public void PlayRandomPopSound()
    {
        audioSource.pitch = UnityEngine.Random.Range(0.99f, 1.01f);
        audioSource.PlayOneShot(bubblePops[UnityEngine.Random.Range(0, bubblePops.Length)]);
    }

    public void PlayStrongPop()
    {
        audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(strongPop);
    }
}
