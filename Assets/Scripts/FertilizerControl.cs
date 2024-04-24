using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerControl : MonoBehaviour
{
    [SerializeField] private Material DefaultM;
    [SerializeField] private Material HighlightM;

    private MeshRenderer meshRenderer;
    private Material[] all_materials = new Material[2];

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        all_materials = meshRenderer.materials;
    }

    public void UpdateVisualFertilizer(bool state)
    {
        if(state)
        {
            all_materials[1] = HighlightM;
            meshRenderer.materials = all_materials; 
        }

        if(!state)
        {
            all_materials[1] = DefaultM;
            meshRenderer.materials = all_materials;
        }
    }


}
