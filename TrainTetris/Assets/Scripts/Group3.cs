using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pivot is corner piece from prefab
public class Group3 : BlockGroup
{
    private Vector2[,] rotatePoints;
    private Vector2[][] colliderRotatePoints;
    public SpriteRenderer test;
    private PolygonCollider2D col;

    public override void Start()
    {
        col = GetComponent<PolygonCollider2D>();

        rotatePoints = new Vector2[,] {
            { Vector2.zero, Vector2.right, Vector2.up },
            { Vector2.right, Vector2.one, Vector2.zero },
            { Vector2.one, Vector2.up, Vector2.right },
            { Vector2.up, Vector2.zero, Vector2.one },
        };

        colliderRotatePoints = new Vector2[][]
        {
            new Vector2[] { Vector2.zero, new Vector2(0, 2), new Vector2(1, 2), Vector2.one, new Vector2(2, 1), new Vector2(2, 0) },
            new Vector2[] { Vector2.zero, Vector2.up, Vector2.one, new Vector2(1, 2), new Vector2(2, 2), new Vector2(2, 0) },
            new Vector2[] { Vector2.up, new Vector2(0, 2), new Vector2(2, 2), new Vector2(2, 0), Vector2.right, Vector2.one },
            new Vector2[] { Vector2.zero, new Vector2(0, 2), new Vector2(2, 2), new Vector2(2, 1), Vector2.one, Vector2.right }
        };

        children = new List<Transform>() {
            transform.GetChild(0),
            transform.GetChild(1),
            transform.GetChild(2),
        };

        GameManager.Instance.nextStopEvent += NextStop;
        ChangeColor();
        RandomRotation();
        UpdateGrid();
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
            children[i].localPosition = rotatePoints[rotateCounter, i];
            col.SetPath(0, colliderRotatePoints[rotateCounter]);
        }
        base.Rotate();
    }
}
