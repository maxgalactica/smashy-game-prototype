using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestructibleBuilding : MonoBehaviour
{
    public delegate void BuildingDelegate();
    public static event BuildingDelegate buildingDelegate;

    // Start is called before the first frame update
    void Start()
    {
        buildingDelegate = TestDelegate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame) buildingDelegate?.Invoke();
        if (Keyboard.current.nKey.wasPressedThisFrame) Debug.Log("Printing without going through a delegate");
    }

    void TestDelegate()
    {
        Debug.Log("Building delegate invoked at " + this.gameObject.name);
    }
}