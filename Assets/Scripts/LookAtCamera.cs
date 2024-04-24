using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAtCamera : MonoBehaviour
{

    [SerializeField] private Transform Target;

    private void Update()
    {
        Vector3 dirToTarget = (Target.position - transform.position).normalized;
        transform.LookAt(Target.position - dirToTarget, Vector3.up);
    }
}
