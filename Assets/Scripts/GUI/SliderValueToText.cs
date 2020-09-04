using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    public Text text;
    public int roundDecimals;
    Slider slider;

    void Start ()
    {
        slider = GetComponent<Slider>();
        ShowSliderValue();
    }

    public void ShowSliderValue ()
    {
        text.text = System.Math.Round(slider.value, roundDecimals).ToString();
    }
}