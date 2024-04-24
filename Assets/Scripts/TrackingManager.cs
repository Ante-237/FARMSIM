using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class TrackingManager : MonoBehaviour
{
    [SerializeField] private Slider WaterSlider;
    [SerializeField] private Slider SoilSlider;
    [SerializeField] private Button HarvestButton;
    [SerializeField] private SettingsSO _settingsSO;

    private ColorBlock _colorBlock;


    private void Start()
    {
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        WaterSlider.value = _settingsSO.TotalAvailableWater <= 0 ? 0 : (_settingsSO.TotalAvailableWater/ _settingsSO.TotalAvailableWaterMax);
        SoilSlider.value = _settingsSO.SoilQuality <= 0 ? 0 : (_settingsSO.SoilQuality / _settingsSO.TotalAvailableWaterMax);
        HarvestButton.interactable = false;

        


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

        if (SoilSlider.value > 0 && SoilSlider.value < _settingsSO.middlebound)
        {
            _colorBlock.normalColor = _settingsSO.bad;
            SoilSlider.colors = _colorBlock;
        }



        // might include check for zero value below
    }

}
