using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AudioPlayer : MonoBehaviour
{
    [SerializeField] protected AudioClip[] Sounds;

    protected AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();    
    }

    public abstract void PlayAudio(AudioClip clip);
    public abstract void PlayAudio();
}
