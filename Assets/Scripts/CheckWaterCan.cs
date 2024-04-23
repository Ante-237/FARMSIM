using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWaterCan : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem.playOnAwake = false;

    }

    private void Update()
    {
        if(transform.localEulerAngles.x > 20 && transform.localEulerAngles.x < 180)
        {
            Debug.Log("Can tilled over certain angle activate water spray. at angel : " + transform.localEulerAngles.x);
            particleSystem.Play();
        }
        
        if(transform.localEulerAngles.x < 20)
        {
            particleSystem.Stop();
        }
    }
}
