using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance { get { return _instance; } }
    public List<Spawner> spawners;
    [SerializeField] public GameObject[] spawnGroups;
    [SerializeField] public Sprite[] blockUI;


    private void Awake()
    {
        // Singleton
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        GameManager.Instance.nextStopEvent += EnableSpawners;
        DisableSpawners();
        EnableSpawners();
    }

    void EnableSpawners()
    {
        if (GameManager.Instance.spawnSide == "left")
        {
            spawners[0].enabled = true;
            spawners[1].enabled = true;
            spawners[2].enabled = false;
            spawners[3].enabled = false;
        }
        else
        {
            spawners[0].enabled = false;
            spawners[1].enabled = false;
            spawners[2].enabled = true;
            spawners[3].enabled = true;
        }
    }

    public void DisableSpawners()
    {
        foreach (Spawner s in spawners)
        {
            s.enabled = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.nextStopEvent -= EnableSpawners;
    }
}
