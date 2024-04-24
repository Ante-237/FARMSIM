using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckWaterCan : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private Image imageFill;
    [SerializeField] private TextMeshProUGUI textMesh;

    // code to highlight can
    [SerializeField] private Material defaultM;
    [SerializeField] private Material HighlightM;
    private Material[] _materials = new Material[6];
    private MeshRenderer _meshRenderer;

    private float initialAmount = 0;
    private bool isflowing = false;
    private bool isWater = true;


    private float timeGap = 2f;
    private float initialTime = 0.0f;
    private float _amountfill = 0.0f;

    [System.Obsolete]
    private void Start()
    {
        particleSystem.playOnAwake = false;
        initialAmount = settingsSO.WaterMax;
        _meshRenderer = GetComponent<MeshRenderer>();
        startState();
    }

    public void ChangeVisuals(bool state)
    {
        _materials = _meshRenderer.materials;

        if(state)
        {
            _materials[0] = HighlightM;
            _materials[1] = HighlightM;
            _materials[2] = HighlightM;
        }

        if (!state)
        {
            _materials[0] = defaultM;
            _materials[1] = defaultM;
            _materials[2] = defaultM;
        }
    }

    private void Update()
    {

        if(transform.localEulerAngles.x > 20 && transform.localEulerAngles.x < 180 && isWater)
        {
            Debug.Log("Can tilled over certain angle activate water spray. at angel : " + transform.localEulerAngles.x);
            particleSystem.Play();
            isflowing = true;
        }
        
        if(transform.localEulerAngles.x < 20 || !isWater)
        {
            particleSystem.Stop();
            isflowing = false;
        }

        reduceWaterAmount();
    }

    void startState()
    {
        textMesh.text = initialAmount + "<color=yellow>l</color>";
        _amountfill = (initialAmount / settingsSO.WaterMax);
        imageFill.fillAmount = (initialAmount / settingsSO.WaterMax);
    }

  

    void reduceWaterAmount()
    {
        if(isflowing)
        {
            if(initialTime  > timeGap)
            {
                initialTime = 0;
                initialAmount -= timeGap;
            }

            initialTime += Time.deltaTime;
        }

        _amountfill = (initialAmount / settingsSO.WaterMax);

        if (_amountfill > settingsSO.upperbound)
        {
            imageFill.color = settingsSO.Good;
        }
        
        if(_amountfill > settingsSO.middlebound && _amountfill < settingsSO.upperbound)
        {
            imageFill.color = settingsSO.normal;
        }

        if(_amountfill > 0  && _amountfill < settingsSO.middlebound)
        {
            imageFill.color = settingsSO.bad;
        }

        if(_amountfill < 0)
        {
            isWater = false;
            isflowing = false;
            initialAmount = 0;
        }


        textMesh.text = initialAmount + "<color=yellow>l</color>";
        imageFill.fillAmount = (initialAmount / settingsSO.WaterMax);
    }
}
