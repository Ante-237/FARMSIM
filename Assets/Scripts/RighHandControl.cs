using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RighHandControl : MonoBehaviour
{
    private Animator anim;
    public InputActionProperty TriggerPhase;

    private void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(TriggerPhase.action.ReadValue<float>() > 0.5f)
        {
            anim.SetBool("trigger", true);
        }

        if(TriggerPhase.action.ReadValue<float>() < 0.5f)
        {
            anim.SetBool("trigger", false); 
        }
    }
}
