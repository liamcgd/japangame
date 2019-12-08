using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pivot is corner piece from prefab
public class Group3 : BlockGroup
{
    private Vector2[,] rotatePoints;
    public SpriteRenderer test;

    void Start()
    {
        rotatePoints = new Vector2[,] {
            { Vector2.zero, Vector2.right, Vector2.up },
            { Vector2.zero, Vector2.up, Vector2.left },
            { Vector2.zero, Vector2.left, Vector2.down },
            { Vector2.zero, Vector2.down, Vector2.right },
        };
        transforms = new List<Transform>() {
            transform.GetChild(0),
            transform.GetChild(1),
            transform.GetChild(2),
        };
        materials = new Material[] {
            transforms[0].GetComponent<SpriteRenderer>().material,
            transforms[1].GetComponent<SpriteRenderer>().material,
            transforms[2].GetComponent<SpriteRenderer>().material,
        };
    }

    void Update()
    {
        // Testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }
    }

    public override void Rotate()
    {
        // Loop through each piece
        for (int i = 0; i < 3; i++)
        {
            // Apply transform
            transforms[i].localPosition = rotatePoints[rotateCounter, i];
        }
        base.Rotate();
    }
}
