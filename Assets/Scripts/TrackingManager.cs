using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackingManager : MonoBehaviour
{
    [SerializeField] private Slider WaterSlider;
    [SerializeField] private Slider SoilSlider;
    [SerializeField] private Button HarvestButton;
    [SerializeField] private SettingsSO _settingsSO;

    private ColorBlock _colorBlock;


    private void Start()
    {
       
        _colorBlock = new ColorBlock();
        _colorBlock.highlightedColor = Color.white;
        _colorBlock.pressedColor = Color.white;
        _colorBlock.selectedColor = Color.white;
        _colorBlock.disabledColor = Color.cyan;
        _colorBlock.colorMultiplier = 1f;
        HarvestButton.interactable = false;
        SetDefaultValues(0);
        SetFirstCheck();

        WaterSlider.onValueChanged.AddListener(SetDefaultValues);
        SoilSlider.onValueChanged.AddListener(SetDefaultValues);
    }

    public void UpdateCanWaterTracker()
    {
        WaterSlider.value = _settingsSO.currentWater / _settingsSO.TotalAvailableWaterMax;
    }

    public void UpdateFertilizerTracker()
    {
        SoilSlider.value = _settingsSO.currentFertilizer / _settingsSO.SoilQualityMax;
    }

    private void SetFirstCheck()
    {
        WaterSlider.value = 0;
        SoilSlider.value = 0;
        HarvestButton.interactable = false;
    }

    private void SetDefaultValues(float value)
    {

        if (WaterSlider.value > _settingsSO.upperbound)
        {
            _colorBlock.normalColor = _settingsSO.Good;
            WaterSlider.colors = _colorBlock;
        }

        if(SoilSlider.value > _settingsSO.upperbound)
        {
            _colorBlock.normalColor = _settingsSO.Good;
            SoilSlider.colors = _colorBlock;
        }


        if (WaterSlider.value > _settingsSO.middlebound && WaterSlider.value < _settingsSO.upperbound)
        {
            _colorBlock.normalColor = _settingsSO.normal;
            WaterSlider.colors = _colorBlock;
        }


        if (SoilSlider.value > _settingsSO.middlebound && SoilSlider.value < _settingsSO.upperbound)
        {
            _colorBlock.normalColor = _settingsSO.normal;
            SoilSlider.colors = _colorBlock;
        }

        if (WaterSlider.value > 0 && WaterSlider.value < _settingsSO.middlebound)
        {
            _colorBlock.normalColor = _settingsSO.bad;
            WaterSlider.colors = _colorBlock;
        }

        if (SoilSlider.value >= 0 && SoilSlider.value < _settingsSO.middlebound)
        {
            _colorBlock.normalColor = _settingsSO.bad;
            SoilSlider.colors = _colorBlock;
        }

        // might include check for zero value below
    }

}
