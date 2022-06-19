using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    [SerializeField] private Material[] materials;
    public GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    public float gridSize;
    private bool gridOn = true;

    public float rotateAmount;

    public bool canPlace;
    public float offset;

    
    void Update()
    {
        if (pendingObject != null)
        {
            UpdateMaterials();
            if (gridOn)
            {
                pendingObject.transform.position = new Vector3(
                        RoundToNearestGrid(pos.x),
                        RoundToNearestGrid(pos.y) + offset,
                        RoundToNearestGrid(pos.z)
                    );
            }
            else
            {
                pendingObject.transform.position = pos;
            }

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
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

    void UpdateMaterials()
    {
        if (canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        else
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void PlaceObject()
    {
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        pendingObject = null;
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
        pendingObject.gameObject.name = objects[index].gameObject.name;
    }

    public void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
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
