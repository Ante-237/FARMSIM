using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highLightMaterial;
    public bool swap = false;
    private bool swapdown = true;
    private Material[] Current_M = new Material[4];


    private MeshRenderer meshRenderer;
   

    private void Start()
    {
       meshRenderer = GetComponent<MeshRenderer>();
    }


    private void Update()
    {
        if (swap)
        {

            Current_M = meshRenderer.materials;
           
            Current_M[2] = highLightMaterial;
            Current_M[3] = highLightMaterial;
          

            swap = false;
            meshRenderer.materials = Current_M;
        }
        
    }
}
