
using UnityEngine;
/// <summary>
/// Service to manage a simple timer for tracking elapsed time
/// </summary>
public class TimerService : MonoBehaviour
{
    public bool running = false;
    public float elapsed { get; private set; }

    private void Update()
    {
        if (running) elapsed += Time.deltaTime;
    }

    public void ResetTimer() => elapsed = 0f;
    public void StartTimer() => running = true;
    public void StopTimer() => running = false;
}
