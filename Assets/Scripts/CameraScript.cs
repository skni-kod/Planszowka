using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject selected_object;
    public Camera cameraa;
    public HexGrid grid_reference;

    public int current_model_to_place;


    void Start()
    {
        cameraa = GetComponent<Camera>();
        current_model_to_place = RandomModelID();
    }


    public int RandomModelID()
    {
        return Random.Range(0, grid_reference.models.Length);
    }


    void Update()
    {
        Pickup();
        if (selected_object != null)
        {
            if (!selected_object.GetComponent<HexCell>().settled)
                selected_object.GetComponent<HexCell>().PickUp(current_model_to_place);
            if (Input.GetMouseButtonDown(0))
            {
                if (!selected_object.GetComponent<HexCell>().settled)
                {
                    selected_object.GetComponent<HexCell>().PlaceHex();
                    current_model_to_place = RandomModelID();
                }

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!selected_object.GetComponent<HexCell>().settled)
                    selected_object.GetComponent<HexCell>().RotateRight(1);
            }
        }
    }

    void Pickup()
    {
        RaycastHit hit;
        Ray ray = cameraa.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Hex")
                selected_object = hit.collider.gameObject;
        }
        else
            selected_object = null;
    }
}
