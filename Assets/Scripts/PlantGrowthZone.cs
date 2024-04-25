using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public class PlantGrowthZone : MonoBehaviour
{
    [SerializeField] private SettingsSO m_SettingsSO;
    [SerializeField] private Image FillImage;
    [SerializeField] private TextMeshProUGUI meshText;
    [SerializeField] private Transform ForkReference;
    [SerializeField] private Transform TrowelReference;

    public UnityEvent RunTimeFertilizerUpdate;
    public UnityEvent TillingEvent;
    public UnityEvent HoleCreationEvent;
    public UnityEvent FirstFertilizerEvent;
    public UnityEvent SeedPlantEvent;
    


    private float initialQuantity = 0;
    private float _fillAmount = 0;
    private bool canUpdate = false;


    private BoxCollider _boxCollider;

    private void Start()
    {
        calculateFill();
        FillImage.fillAmount = initialQuantity <= 0 ? 0 : initialQuantity / m_SettingsSO.phMax;
        meshText.text = "<color=yellow>ph</color> " + initialQuantity;
        _boxCollider = GetComponent<BoxCollider>();
    }

    private bool fork = true;
    private bool trowel = true;

    private void Update()
    {
        if ((canUpdate))
        {
            UpdateMeter();
            canUpdate = false;
        }

        if (fork)
        {
           if(DistanceCheck(ForkReference))
           {
                StartCoroutine(updateFork(3));

                fork = false;
           }
        }

        if (trowel)
        {
            if(DistanceCheck(TrowelReference))
            {
                StartCoroutine(updateTrowel(3));
                trowel = false;
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("fertilizer"))
        {
            StartCoroutine(RunFertilizerUpdate(other.gameObject));
        }

        /*
        if (other.gameObject.CompareTag("fork"))
        {
            StartCoroutine(updateFork(3));
        }

        if (other.gameObject.CompareTag("trowel"))
        {
            updateTrowel(3);
        }
        */

        if (other.gameObject.CompareTag("seed"))
        {
            SeedPlantEvent.Invoke();
            other.gameObject.SetActive(false);
        }
    }

    public bool DistanceCheck(Transform other)
    {
        if(Vector3.Distance(transform.position, other.position) < m_SettingsSO.gapCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, ForkReference.position);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, TrowelReference.position);
    }

    IEnumerator updateFork(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(TillingEvent != null)
        {
            TillingEvent.Invoke();
        }
    }

    IEnumerator updateTrowel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(HoleCreationEvent != null)
        {
            HoleCreationEvent.Invoke();
        }
    }

    bool runOnce = true;


    IEnumerator RunFertilizerUpdate(GameObject t)
    {
        yield return new WaitForSeconds(m_SettingsSO.TimeToValidate);

        initialQuantity += m_SettingsSO.fertilizerMin;
        //m_SettingsSO.currentFertilizer += initialQuantity;

        //initialQuantity = m_SettingsSO.currentFertilizer + m_SettingsSO.fertilizerMin;
        canUpdate = true;

        if (runOnce)
        {
            if (RunTimeFertilizerUpdate != null)
            {
                RunTimeFertilizerUpdate.Invoke();
            }

            if (FirstFertilizerEvent != null)
            {
                FirstFertilizerEvent.Invoke();
            }
            runOnce = false;

            Destroy(t);
        }

        calculateFill();
    }


    void calculateFill()
    {
        _fillAmount = initialQuantity <= 0 ? 0 : initialQuantity / m_SettingsSO.phMax;
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
