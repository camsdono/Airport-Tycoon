using System;
using UnityEngine;
using TMPro;


public class SelectionManager : MonoBehaviour
{
    public GameObject selectionPanel;
    public GameObject selectedObject;
    public TextMeshProUGUI objNameText;
    private BuildingManager _buildingManager;

    private void Start()
    {
        _buildingManager = GameObject.Find("Managers").GetComponent<BuildingManager>();
        selectionPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Building"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && selectedObject != null)
        {
            Deselect();
        }
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObject) return;
        if(selectedObject != null) Deselect();
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null) obj.AddComponent<Outline>();
        else outline.enabled = true;
        objNameText.text = obj.name;
        selectedObject = obj;
        selectionPanel.SetActive(true);
    }

    private void Deselect()
    {
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
        objNameText.text = "";
        selectionPanel.SetActive(false);
    }

    public void Move()
    {
        _buildingManager.pendingObject = selectedObject;
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
        selectedObject = null;
        objNameText.text = "";
    }
}
