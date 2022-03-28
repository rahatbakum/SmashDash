using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMassApplier : MonoBehaviour
{
    private MassApplier _massApplier;
    void Start()
    {
        _massApplier = GetComponent<MassApplier>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            float mass = 2f * Mathf.Clamp((float) Input.mousePosition.x / Screen.width, 0f, 1f);
            Debug.Log($"ApplyMass({mass})");
            _massApplier.ApplyMass(mass);
        }
    }
}
