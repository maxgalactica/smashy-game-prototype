using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject slicer;

    Renderer slicerRenderer;

    private void Awake()
    {
        slicerRenderer = slicer.GetComponent<Renderer>();
        slicerRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            float newRotationZ = (Random.Range(190f, 10f));
            slicer.transform.localEulerAngles = new Vector3(0f, 0f, newRotationZ);
            StartCoroutine(Flash());
            DoSlice();
        }
    }

    public void DoSlice()
    {
        var mesh = slicer.GetComponent<MeshFilter>().sharedMesh;
        var center = mesh.bounds.center;
        var extents = mesh.bounds.extents;

        extents = new Vector3(extents.x * slicer.transform.localScale.x,
                              extents.y * slicer.transform.localScale.y,
                              extents.z * slicer.transform.localScale.z);

        // Cast a ray and find the nearest object
        RaycastHit[] hits = Physics.BoxCastAll(slicer.transform.position, extents, slicer.transform.forward, slicer.transform.rotation, extents.z);

        foreach (RaycastHit hit in hits)
        {
            var obj = hit.collider.gameObject;
            var sliceObj = obj.GetComponent<Slice>();
            Enemy enemy = obj.GetComponent<Enemy>();

            if (sliceObj != null && enemy != null)
            {
                enemy.TakeDamage(1);

                if(enemy.Health <= 0)

                sliceObj.GetComponent<MeshRenderer>()?.material.SetVector("CutPlaneOrigin", Vector3.positiveInfinity);
                sliceObj.ComputeSlice(slicer.transform.up, slicer.transform.position);
            }
        }
    }

    private IEnumerator Flash()
    {
        slicerRenderer.enabled = true;
        yield return new WaitForSeconds(0.125f);
        slicerRenderer.enabled = false;
    }
}