﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockGroup : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 previousPos;
    protected int rotateCounter = 0;
    protected int stopsLeft;
    protected List<GameObject> children;
    protected List<Material> materials;

    private SpriteRenderer[] _renderers;
    [SerializeField] private TextMeshProUGUI stopsLeftText;

    public void Awake()
    {
        stopsLeft = Random.Range(1, 4);
        _renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    public virtual void Start()
    {
        children = new List<GameObject>() {
            transform.GetChild(0).gameObject
        };
        // materials = new List<Material>()
        // {
        //     transforms[0].GetComponent<SpriteRenderer>().material
        // };
        GameManager.Instance.nextStopEvent += NextStop;
        ChangeColor();
        RandomRotation();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        previousPos = transform.position;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = Train.RoundVector(curPosition);
    }

    private void OnMouseUp()
    {
        if (IsValidGridPosition())
        {
            UpdateGrid();
        }
        else
        {
            // Reset back
            transform.position = previousPos;
        }
    }

    public void ChangeColor()
    {
        // Set fill and border colours here
        // foreach (Material m in materials)
        // {
        //     m.SetColor("_Colour", GameManager.stopColours[stopsLeft - 1]);
        //     m.SetColor("_BorderColour", GameManager.stopBorderColours[stopsLeft - 1]);
        // }
        foreach (GameObject g in children)
        {
            g.GetComponent<SpriteRenderer>().color = GameManager.Instance.stopColours[stopsLeft - 1];
        }

        stopsLeftText.text = stopsLeft.ToString();
    }

    public void NextStop()
    {
        if (stopsLeft > 0)
        {
            stopsLeft -= 1;
            ChangeColor();
        }
        else
        {
            // Increase score by group size
            GameManager.Instance.UpdateScore(children.Count);
            // Remove from delegate
            GameManager.Instance.nextStopEvent -= NextStop;
            // Remove from train
            Destroy(gameObject);
        }
    }

    public bool IsValidGridPosition()
    {
        foreach (GameObject g in children)
        {
            // Round vector (Necessary?)
            Vector2 v = Train.RoundVector(g.transform.position);
            // If not inside the train borders
            if (!Train.InsideTrain(v))
                return false;
            // If not a free spot
            if (Train.grid[(int)v.x, (int)v.y] != null &&
            Train.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    public void UpdateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Train.length; y++)
            for (int x = 0; x < Train.width; x++)
                if (Train.grid[x, y] != null)
                    if (Train.grid[x, y].parent == transform)
                        Train.grid[x, y] = null;

        // Add new children to grid
        foreach (GameObject g in children)
        {
            Vector2 v = Train.RoundVector(g.transform.position);
            Train.grid[(int)v.x, (int)v.y] = g.transform;
        }
    }

    /// <summary>
    /// Rotate a group from its a prefab by 0 to 3 positions
    /// </summary>
    public void RandomRotation()
    {
        rotateCounter = Random.Range(0, 4);
        Rotate();
    }

    public virtual void Rotate()
    {
        rotateCounter++;
        if (rotateCounter > 3)
            rotateCounter = 0;
    }

    private void OnDestroy()
    {
        // foreach (Material m in materials)
        // {
        //     Destroy(m);
        // }
    }
}
