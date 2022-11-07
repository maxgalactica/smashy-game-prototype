using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public PlaneSlicer slicer;

    private void Awake()
    {
        //slicer = GetComponent<PlaneSlicer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            float newRotationZ = (Random.Range(190f, 10f));
            slicer.transform.localEulerAngles = new Vector3(0f, 0f, newRotationZ);
            slicer.SliceObject();
        }
    }
}