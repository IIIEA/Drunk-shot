using UnityEngine;

public class SimpleAudioPlayer : AudioPlayer
{
    public override void PlayAudio(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    public override void PlayAudio()
    {
        if (Sounds.Length == 0)
            return;

        var randomSound = Random.Range(0, Sounds.Length - 1);

        AudioSource.PlayOneShot(Sounds[randomSound]);
    }
}
