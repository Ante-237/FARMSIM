using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsSO", menuName = "Settings/SettingsSO")]
public class SettingsSO : ScriptableObject
{
    public float TotalAvailableWater = 0.5f;
    public float SoilQuality = 0.5f;
    public float TotalAvailableWaterMax = 100f;
    public float SoilQualityMax = 100f;


    public float WaterMax = 50;
    public Color Good;
    public float upperbound = 0.5f;
    public Color normal;
    public float middlebound = 0.2f;
    public Color bad;
    public float timeFactorScale = 0.002f;

    public float TimeToValidate = 2.0f;


    public float phMax = 50;
    public float fertilizerMin = 5f;
}
