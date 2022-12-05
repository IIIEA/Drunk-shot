using UnityEngine;
using UnityEngine.Events;

public class PauseNotifier : MonoBehaviour
{
    public UnityEvent<bool> Paused;

    public void SetPause(bool isPaused)
    {
        Paused?.Invoke(isPaused);
    }
}
