using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform goal;
    public GameObject player;

    NavMeshAgent selfAI;

    private void Awake()
    {
        selfAI = gameObject.GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        selfAI.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        goal.position = player.transform.position;
        selfAI.destination = goal.position;
    }
}