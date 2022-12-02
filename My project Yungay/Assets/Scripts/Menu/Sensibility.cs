using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Sensibility : MonoBehaviour
{
    public Slider slider;
    public float slidervalue;
    public TMP_Text textValue;
    [SerializeField]
    private GameObject camera;
    private CinemachinePOV cinemachine;
    private CinemachineVirtualCamera cinemachineVirtual;
    // Start is called before the first frame update
    void Start()
    {
        if(!cinemachine)
        {
            camera = GameObject.FindGameObjectWithTag("ControladorCM");
            cinemachineVirtual = camera.GetComponent<CinemachineVirtualCamera>();
            cinemachine = cinemachineVirtual.AddCinemachineComponent<CinemachinePOV>();
        }
        slider.value = PlayerPrefs.GetFloat("Sensibility", 125f);
        //PlayerCam.sensX = slider.value;
        //PlayerCam.sensY = slider.value;
        cinemachine.m_HorizontalAxis.m_MaxSpeed = slider.value;
        cinemachine.m_VerticalAxis.m_MaxSpeed = slider.value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSlider(float value)
    {
        slidervalue = value;
        PlayerPrefs.SetFloat("Sensibility", slidervalue);
        //PlayerCam.sensX = slider.value;
        //PlayerCam.sensY = slider.value;
        cinemachine.m_HorizontalAxis.m_MaxSpeed = slider.value;
        cinemachine.m_VerticalAxis.m_MaxSpeed = slider.value;
        ShowValue();

    }

    public void ShowValue()
    {
        float distanceFromMin = (slider.value - slider.minValue);
        float sliderRange = (slider.maxValue - slider.minValue);
        float sliderPercent = (distanceFromMin / sliderRange);
        textValue.text = sliderPercent.ToString("F2");
    }
}
