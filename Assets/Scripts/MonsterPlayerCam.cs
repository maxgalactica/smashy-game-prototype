using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayerCam : MonoBehaviour
{
    public Transform playerPos;
    public float yOffset = 0f;
    public float zOffset = 0f;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerPos.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + new Vector3(0f, yOffset, zOffset);
    }
}