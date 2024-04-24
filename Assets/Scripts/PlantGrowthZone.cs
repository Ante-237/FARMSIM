using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlantGrowthZone : MonoBehaviour
{
    [SerializeField] private SettingsSO m_SettingsSO;
    [SerializeField] private Image FillImage;
    [SerializeField] private TextMeshProUGUI meshText;


    private float initialQuantity = 0;
    private float _fillAmount = 0;
    private bool canUpdate = false;

    private void Start()
    {
        calculateFill();
        FillImage.fillAmount = initialQuantity <= 0 ? 0 : initialQuantity/m_SettingsSO.phMax;
        meshText.text = "<color=yellow>ph</color> " + initialQuantity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("fertilizer"))
        {
           StartCoroutine(RunFertilizerUpdate(other.gameObject));
        }
    }

    IEnumerator RunFertilizerUpdate(GameObject t)
    {
        yield return new WaitForSeconds(m_SettingsSO.TimeToValidate);
        initialQuantity += m_SettingsSO.fertilizerMin;
        canUpdate = true;
        Destroy(t);
        calculateFill();
    }



    void calculateFill()
    {
        _fillAmount = initialQuantity <= 0 ? 0 : initialQuantity / m_SettingsSO.phMax;
    }

    private void Update()
    {
        if ((canUpdate))
        {
            UpdateMeter();
            canUpdate = false;
        }
    }


    public void UpdateMeter()
    {
        calculateFill() ;
       
        if (_fillAmount > m_SettingsSO.upperbound)
        {
            FillImage.color = m_SettingsSO.Good;
        }

        if (_fillAmount > m_SettingsSO.middlebound && _fillAmount < m_SettingsSO.upperbound)
        {
            FillImage.color = m_SettingsSO.normal;
        }

        if (_fillAmount > 0 && _fillAmount < m_SettingsSO.middlebound)
        {
            FillImage.color = m_SettingsSO.bad;
        }

        if (_fillAmount < 0)
        {
            _fillAmount = 0;
        }

        FillImage.fillAmount = _fillAmount;
        meshText.text = "<color=yellow>ph</color> " + initialQuantity;
    }
}
