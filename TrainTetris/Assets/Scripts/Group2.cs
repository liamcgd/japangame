using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Derived Groups to handle individual rotation
// Pivot will be left most piece in prefab
public class Group2 : BlockGroup
{
    private Vector2[] rotatePoints;

    public override void Start()
    {
        rotatePoints = new Vector2[] {
            Vector2.right,
            Vector2.up,
            Vector2.right,
            Vector2.up
        };
        children = new List<Transform>() {
            transform.GetChild(0),
            transform.GetChild(1),
        };
        GameManager.Instance.nextStopEvent += NextStop;
        ChangeColor();
        RandomRotation();
        UpdateGrid();
    }

    void Update()
    {

    }

    public override void Rotate()
    {
        // Rotate the piece and increment the counter
        children[1].localPosition = rotatePoints[rotateCounter];
        base.Rotate();
    }
}
