using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] bubblePops;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlayRandomPopSound()
    {
        audioSource.pitch = UnityEngine.Random.Range(0.99f, 1.01f);
        audioSource.PlayOneShot(bubblePops[UnityEngine.Random.Range(0, bubblePops.Length)]);
    }
}
