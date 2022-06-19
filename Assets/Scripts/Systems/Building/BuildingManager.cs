using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    public float gridSize;
    private bool gridOn = true;

    
    void Update()
    {
        if (pendingObject != null)
        {
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                    RoundToNearestGrid(pos.x),
                    RoundToNearestGrid(pos.y),
                    RoundToNearestGrid(pos.z)
                    );
            }
            else
            {
                pendingObject.transform.position = pos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void PlaceObject()
    {
        pendingObject = null;
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }
    

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }

        return pos;
    }
}
