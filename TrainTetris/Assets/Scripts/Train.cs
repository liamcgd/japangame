using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public int length;

    public int width;

    public GameObject test;

    private GameObject[,] trainGrid;
    // Start is called before the first frame update
    void Start()
    {
        CreateTrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateTrain()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                GameObject blockObject = Instantiate(test, transform.position, Quaternion.identity);
                blockObject.transform.position = -Vector2.one * (width - 1) * 0.5f + new Vector2(x, y);
                blockObject.transform.parent = transform;
            }
        }
    }
    
    
}
