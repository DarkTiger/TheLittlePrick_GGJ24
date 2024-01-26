using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaughBar : MonoBehaviour
{
    public int _maxLaughLevel = 100;
    public int _currentLaughlevel = 0;
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    public void SetLaughBar()
    {
        slider.maxValue = _maxLaughLevel;
        slider.value = _currentLaughlevel;
        fill.color = gradient.Evaluate(1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetLaughBar();
    }

    public void AddLaugh(int value)
    {
        _currentLaughlevel += value;
        slider.value = _currentLaughlevel;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void RemoveLaugh(int value)
    {
        _currentLaughlevel -= value;
        slider.value = _currentLaughlevel;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //public void Update()
    //{
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        AddLaugh(1);
    //    }
    //    if (Input.GetKey(KeyCode.F))
    //    {
    //       RemoveLaugh(1);
    //    }
    //}

}
