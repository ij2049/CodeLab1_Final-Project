using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//serializeField is better because we want to use this at the inspector not another xcript
//it might cause issue
public class SomeClass : MonoBehaviour
{
    public string playerName;
    public float speed;
    public GameObject playerPrefabs;
    public Vector3 playerPosition;

    [SerializeField] private string s_playerName;
    [SerializeField] private float s_speed;
    [SerializeField] private GameObject s_playerPrefabs;
    [SerializeField] private Vector3 s_playerPosition;
}
