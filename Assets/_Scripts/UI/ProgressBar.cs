using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    float fillSpeed = 0.5f;

    Slider slider;

    float targetProgress = 0;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();

        if (slider == null)
            Debug.LogError($"Slider reference missing on {gameObject.name}!", this);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
            slider.value += fillSpeed * Time.deltaTime;

        if (slider.value > targetProgress)
            slider.value -= fillSpeed * Time.deltaTime;
    }

    public void IncrementProgress(float newProgress)
    {
        if ((slider.value + newProgress) > slider.maxValue)
        {
            targetProgress = slider.maxValue;
        }
        else
        {
            targetProgress = slider.value + newProgress;
        }
    }

    public void DecrementProgress(float newProgress)
    {
        if ((slider.value - newProgress) < slider.minValue)
        {
            targetProgress = slider.minValue;
        }
        else
        {
            targetProgress = slider.value - newProgress;
        }
    }

    public void SetProgress(float newProgress)
    {
        if (newProgress > slider.maxValue)
        {
            targetProgress = slider.maxValue;
        }
        else if (newProgress < slider.minValue)
        {
            targetProgress = slider.minValue;
        }
        else
        {
            targetProgress = newProgress;
        }
    }

    public float GetSliderValue()
    {
        return slider.value;
    }
}
