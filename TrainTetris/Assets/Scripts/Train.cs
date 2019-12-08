using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public static int length = 12;
    public static int width = 8;
    public static Transform[,] grid;

    public GameObject test;


    void Start()
    {
        grid = new Transform[width, length];
        // CreateTrain();
    }

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

    public static Vector2 RoundVector(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool InsideTrain(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0 && (int)pos.y < length);
    }
}
