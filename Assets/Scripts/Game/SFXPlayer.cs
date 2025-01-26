using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSFX;
    AudioSource _audioSource => GetComponent<AudioSource>();
    private void OnEnable()
    {
        EventManager.KnotGrabbed += PlaySound;
        EventManager.KnotReleased += StopSound;
    }
    private void OnDisable()
    {
        EventManager.KnotGrabbed -= PlaySound;
        EventManager.KnotReleased -= StopSound;
    }
    private void PlaySound()
    {
        _audioSource.PlayOneShot(_clickSFX, 0.5f);
        _audioSource.Play();
    }
    private void StopSound()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_clickSFX, 0.5f);
    }
}
