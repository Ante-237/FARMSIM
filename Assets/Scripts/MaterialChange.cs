using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highLightMaterial;

    private Material[] Current_M = new Material[4];


    private MeshRenderer meshRenderer;
   

    private void Start()
    {
       meshRenderer = GetComponent<MeshRenderer>();
       Current_M = meshRenderer.materials;
    }

    public void HighLightTool(bool state)
    {
        if(state)
        {
            Current_M[2] = highLightMaterial;
            Current_M[3] = highLightMaterial;
            meshRenderer.materials = Current_M;
        }

        if (!state)
        {
            Current_M[2] = defaultMaterial;
            Current_M[3] = defaultMaterial;
            meshRenderer.materials = Current_M;
        }
    }
}
