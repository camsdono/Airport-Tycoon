using System;
using UnityEngine;


public class CheckBuildPlacement : MonoBehaviour
{
    private BuildingManager _buildingManager;
    
    private void Start()
    {
        _buildingManager = GameObject.Find("Managers").GetComponent<BuildingManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Building") )
        {
            _buildingManager.canPlace = false;
        }

        if (other.gameObject.CompareTag("Boundary"))
        {
            _buildingManager.canPlace = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            _buildingManager.canPlace = true;
        }
        
        if (other.gameObject.CompareTag("Boundary"))
        {
            _buildingManager.canPlace = true;
        }
    }
}
