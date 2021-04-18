using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickMenu : MonoBehaviour
{
    public GameObject parent;
    public GameObject build_menu;
    public GameObject claim_menu;
    public GameObject forest_menu;
    public GameObject nothing_menu;
    public GameObject building_menu;
    
    void Awake()
    {
        parent = transform.gameObject;
    }

    public void CenterOnMouse()
    {
        transform.position = Input.mousePosition;
    }

    public void Show()
    {
        CenterOnMouse();
        parent.SetActive(true);
    }

    public void Hide()
    {
        parent.SetActive(false);
    }

    private void HideAllSubmenus()
    {
        for (int i=0; i < parent.transform.childCount; i++)
        {
            parent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SelectMenu(HexCell hex_cell)
    {
        HideAllSubmenus();
        if (hex_cell.has_building)
        {
            building_menu.SetActive(true);
        }
        else if (HexTypes.types[hex_cell.type].type == "grass")
        {
            build_menu.SetActive(true);
        }
        else if (HexTypes.types[hex_cell.type].type == "forest")
        {
            forest_menu.SetActive(true);
        }
        else
        {
            nothing_menu.SetActive(true);
        }
    }

}
