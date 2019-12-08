using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Vector2 spawnTile;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 1);
    }

    private void Spawn()
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
        // Random Index
        int i = Random.Range(0, GameManager.Instance.spawnGroups.Length);

        // Spawn Group at current Position
        GameObject blockGroup = Instantiate(GameManager.Instance.spawnGroups[i], spawnTile, Quaternion.identity);
    }
}
