using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Vector2 spawnTile;
    public Image nextBlockUI;
    private int nextBlockIndex;

    private void Start()
    {
        nextBlockIndex = Random.Range(0, SpawnManager.Instance.spawnGroups.Length);
        nextBlockUI.sprite = SpawnManager.Instance.blockUI[nextBlockIndex];
        nextBlockUI.SetNativeSize();
    }

    public void Spawn()
    {
        if (DoorFree())
        {
            SpawnNext();
        }
    }

    public bool DoorFree()
    {
        for (int y = (int)spawnTile.y; y < (int)spawnTile.y + 2; y++)
            for (int x = (int)spawnTile.x; x < (int)spawnTile.x + 2; x++)
                if (Train.grid[x, y] != null)
                    return false;
        return true;
    }

    public void SpawnNext()
    {
        // Spawn Group
        GameObject blockGroup = Instantiate(SpawnManager.Instance.spawnGroups[nextBlockIndex], spawnTile, Quaternion.identity);

        // Determine next group
        nextBlockIndex = Random.Range(0, SpawnManager.Instance.spawnGroups.Length);
        // Display UI for next group
        nextBlockUI.sprite = SpawnManager.Instance.blockUI[nextBlockIndex];
        nextBlockUI.SetNativeSize();
    }

    private void OnEnable()
    {
        nextBlockUI.enabled = true;
        InvokeRepeating(nameof(Spawn), 0, 1);
    }

    private void OnDisable()
    {
        CancelInvoke();
        nextBlockUI.enabled = false;
    }
}
