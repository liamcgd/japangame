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
        children = new List<GameObject>() {
            transform.GetChild(0).gameObject,
            transform.GetChild(1).gameObject,
        };
        // materials = new List<Material>() {
        //     children[0].GetComponent<SpriteRenderer>().material,
        //     children[1].GetComponent<SpriteRenderer>().material,
        // };
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
        children[1].transform.localPosition = rotatePoints[rotateCounter];
        base.Rotate();
    }
}
