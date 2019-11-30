using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockGroup : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private Vector3 previousPos;
    private int stopsLeft;
    private SpriteRenderer[] _renderers;
    private bool isValidMove;
    private int collisionCount;
    
    public void Awake()
    {
        stopsLeft = Random.Range(1, 4);
        _renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        previousPos = transform.position;
        isValidMove = true;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        if (collisionCount > 0)
        {
            transform.position = previousPos;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        collisionCount++;
        Debug.Log("Collision");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        collisionCount--;
    }

    public void ChangeColor()
    {
        if (stopsLeft == 3){
            foreach (var r in _renderers)
            {
                r.color = Color.blue;

            }
        }else if (stopsLeft == 2){
            foreach (var r in _renderers)
            {
                r.color = Color.grey;

            }
        }else if (stopsLeft == 1){
            foreach (var r in _renderers)
            {
                r.color = Color.yellow;

            }
        }else if (stopsLeft == 0){
            foreach (var r in _renderers)
            {
                r.color = Color.red;

            }
        }
    }

    public void NextStop()
    {
        if (stopsLeft > 0)
        {
            stopsLeft -= 1;
        }
    }
}
