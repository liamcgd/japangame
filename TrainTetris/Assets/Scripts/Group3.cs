﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pivot is corner piece from prefab
public class Group3 : BlockGroup
{
    private Vector2[,] rotatePoints;
    public SpriteRenderer test;

    public override void Start()
    {
        rotatePoints = new Vector2[,] {
            { Vector2.zero, Vector2.right, Vector2.up },
            { Vector2.zero, Vector2.up, Vector2.left },
            { Vector2.zero, Vector2.left, Vector2.down },
            { Vector2.zero, Vector2.down, Vector2.right },
        };
        children = new List<GameObject>() {
            transform.Find("Block").gameObject,
            transform.GetChild(1).gameObject,
            transform.GetChild(2).gameObject,
        };
        // materials = new List<Material>() {
        //     children[0].GetComponent<SpriteRenderer>().material,
        //     children[1].GetComponent<SpriteRenderer>().material,
        //     children[2].GetComponent<SpriteRenderer>().material,
        // };
        ChangeColor();
        RandomRotation();
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
            children[i].transform.localPosition = rotatePoints[rotateCounter, i];
        }
        base.Rotate();
    }
}
