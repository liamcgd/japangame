using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Spawner> spawners;

    void Start()
    {
        //spawners = new List<Spawner>();
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
