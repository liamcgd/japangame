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
            Vector2.left,
            Vector2.down
        };
        transforms = new List<Transform>() {
            transform.GetChild(0),
            transform.GetChild(1),
        };
        materials = new List<Material>() {
            transforms[0].GetComponent<SpriteRenderer>().material,
            transforms[1].GetComponent<SpriteRenderer>().material,
        };
    }

    void Update()
    {

    }

    public override void Rotate()
    {
        // Rotate the piece and increment the counter
        transforms[1].localPosition = transforms[0].localPosition - (Vector3)rotatePoints[rotateCounter];
        base.Rotate();
    }
}
