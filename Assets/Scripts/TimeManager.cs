using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    private float slowFactor = 0.2f;

    public void DoSlowmotion()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
