using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    public float pulseSpeed = 1.5f;
    public float scaleAmount = 1.1f;

    private bool isPulsing = false;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPulsing)
        {
            float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * (scaleAmount - 1);
            transform.localScale = originalScale * scale;
        }
    }

    public void StartPulse()
    {
        isPulsing = true;
    }

    public void StopPulse()
    {
        isPulsing = false;
        transform.localScale = originalScale;
    }
}
