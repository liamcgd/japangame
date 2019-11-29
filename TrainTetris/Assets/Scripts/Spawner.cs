using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groups;

    private void Start()
    {
        // Spawn initial Group
        //SpawnNext();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            SpawnNext();
        }
    }

    public void SpawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        GameObject blockGroup = Instantiate(groups[i], transform.position, Quaternion.identity);
        blockGroup.GetComponent<BlockGroup>().ChangeColor();
    }
}
