using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// method to  update steps.
// reference settings
// reference ui panel
// reference watering can
// reference fertilizer 
// reference tools
// reference anim tutorials

public class SimulationManager : MonoBehaviour
{
    [Header("Setting Reference")]
    [SerializeField] private SettingsSO _settings;
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private Button HarvetButton;

    [Header("UI Panel Reference")]
    [SerializeField] private TextMeshProUGUI stepText;


    [Header("WateringCan Reference")]
    [SerializeField] private CheckWaterCan Watercan;

    [Header("Fertilizer Reference")]
    [SerializeField] private List<FertilizerControl> allFertizers;

    [Header("Fork Tool Reference")]
    [SerializeField] private MaterialChange ForkTool;

    [Header("Spoon Tool Reference")]
    [SerializeField] private MaterialChange TrowelTool;

    [Header("Anim Tutorial Reference")]
    [SerializeField] private GameObject SeedTutorial;
    [SerializeField] private GameObject ForkToolTutorial;
    [SerializeField] private GameObject TrowelToolTutorial;
    [SerializeField] private GameObject FertilizerTutorial;

    [Header("Plant Growth Stages")]
    [SerializeField] private List<GameObject> Plantlevels;
    [SerializeField] private TrackingManager trackingManager;


    private void Start()
    {
        InitialSetup();
    }

    private void GrowPlant(int index)
    {
        for(int i = 0; i < Plantlevels.Count; i++)
        {
            Plantlevels[i].SetActive(false);
        }
        Plantlevels[index].SetActive(true);
    }

    // step 1
    private void ContinueCheck()
    {
        if (inputAction.action.IsPressed())
        {
            // perform next action
            UpdateStep(1);
            StartCoroutine(ImproveSoilph(4));
        }
    }

    private void InitialSetup()
    {
        UpdateStep(0);
    }

    private void UpdateStep(int index)
    {
        if(index < allFertizers.Count)
        {
            stepText.text = _settings.all_Steps[index];
        }
    }


    // step 2
    IEnumerator ImproveSoilph(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        TillSoilStep();
        UpdateStep(2);
    }


    // step 3
    private void TillSoilStep()
    {
        // update step
        // show till tutorial
        // highlight tool to be used
        ForkToolTutorial.SetActive(true); 
        ForkTool.HighLightTool(true);
    }

    // step 4 : uses and external event trigger calling, when till happens
    public void AddFertilizer()
    {
        // disable tutorial
        // un highlight
        // increase soil quality after tilling activity
        //_settings.currentFertilizer += 30f;
        ForkToolTutorial.SetActive(false);
        ForkTool.HighLightTool(false);

        // fertilizer tutorial
        // highlight all fertilizers
        // update next step
        // increase soil quality



        FertilizerTutorial.SetActive(true);
        foreach(FertilizerControl f in allFertizers)
        {
            f.UpdateVisualFertilizer(true);
        }

        UpdateStep(3);
    }

    // step 5 : uses and external event trigger
    public void CreateHolePlanting()
    {

        // disable all fertilizer highlight.
        // disable tutorial as well.
        FertilizerTutorial.SetActive(false);
        foreach (FertilizerControl f in allFertizers)
        {
            f.UpdateVisualFertilizer(false);
        }

        // show tutorial
        // highlight tool
        // update step

        TrowelToolTutorial.SetActive(true);
        TrowelTool.HighLightTool(true);
        UpdateStep(4);
    }

    // step 6 ; uses and external event trigger
    public void SeedPlanting()
    {
        // disable trowel highlight
        // disable trowel tutorial

        TrowelToolTutorial.SetActive(false);
        TrowelTool.HighLightTool(false);

        // enable seed tutorial
        
        SeedTutorial.SetActive(true);
        UpdateStep(5);
    }

    // step 7 ; externally triggered
    public void WaterAddition()
    {
        // disable seed tutorial
        // update next step
        // highlight watering can

        SeedTutorial.SetActive(false);
        UpdateStep(6);
        Watercan.ChangeVisuals(true);
        StartCoroutine(WaitingforGermination());
    }

    int currentIndex = 0;
    float currentTime = 0;
    bool canGrow = false;

    // step 8: 
    IEnumerator WaitingforGermination()
    {
        yield return new WaitForSeconds(8);

        // disable most stuff
        Watercan.ChangeVisuals(false);
        UpdateStep(7);
        GrowPlant(currentIndex);
        canGrow = true;
    }

    private void Update()
    {
        ContinueCheck();
        StartGrowthSequence();
    }
   

    void StartGrowthSequence()
    {
        if (canGrow)
        {
            if(currentTime > _settings.growthGap)
            {
                UpdateStep(8);
                currentTime = 0;
                currentIndex += 1;

                if(currentIndex >= Plantlevels.Count)
                {
                    canGrow = false;
                    stepText.text = "COMPLETED";
                    HarvetButton.interactable = true;
                }

                if(currentIndex < Plantlevels.Count)
                {
                   // _settings.growthGap += 0.5f;
                    _settings.TotalAvailableWater -= 10;
                    _settings.SoilQuality -= 10;
                    trackingManager.UpdateCanWaterTracker();
                    trackingManager.UpdateFertilizerTracker();
                    GrowPlant(currentIndex);
                }
            }
            currentTime += Time.deltaTime;
        }
    }


}
