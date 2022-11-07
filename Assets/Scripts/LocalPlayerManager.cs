using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerManager : MonoBehaviour
{
    public static List<GameObject> playerContainer = new List<GameObject>();

    [SerializeField]
    private Material[] playerColors;
    [SerializeField]
    private GameObject[] playerHats;

    public void SetPlayerColor(Material color, int plyIndex)
    {
        playerContainer[plyIndex].GetComponentInChildren<MeshRenderer>().material = playerColors[plyIndex];
    }

    public void SetPlayerHat(GameObject hat, int plyIndex)
    {
        Vector3 placePos;

        //placePos = playerContainer[plyIndex].GetComponent<MonsterCosmetics>()
    }
}