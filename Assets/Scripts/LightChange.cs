using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightChange : MonoBehaviour {

    public Slider slider;
    public Light lightintense;
    public float lighting = 1.0f;

    void Start()
    {
        //lightintense.intensity = lighting;
    }

    // Update is called once per frame
    void Update()
    {
        lightintense.intensity = slider.value;
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    lighting -= 0.05f;
        //    lightintense.intensity = lighting;
        //}
    }
}
