﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject selected_object;
    public Camera cameraa;
    public HexGrid grid_reference;
    public Transform camera_holder;

    public int current_model_to_place;


    void Start()
    {
        cameraa = GetComponent<Camera>();
        current_model_to_place = RandomModelID();
    }


    void Update()
    {
        PickHex();
        HandleMouseAndGridInteraction();
        HandleMovingCamera();
    }

    void HandleMouseAndGridInteraction()
    {
        //Check if we have hex in selected object container
        if (selected_object != null)
        {
            HexCell hex = selected_object.GetComponent<HexCell>();
            //Then we check if it's set, and if not -> we show model
            if (!hex.settled)
            {
                hex.ShowModel(current_model_to_place);
                //Now we place the hex on mouse_left_button, and reroll next hex model to place
                if (Input.GetMouseButtonDown(0))
                {
                    hex.PlaceHex();
                    current_model_to_place = RandomModelID();
                }
                //Or rotate on mouse_right_button
                if (Input.GetMouseButtonDown(1))
                {
                    hex.RotateRight(1);
                }
            }
        }
    }


    void PickHex()
    {
        //We scan enviroment for objects with 'Hex' tag, if we find one we place it in the selected_object
        RaycastHit hit;
        Ray ray = cameraa.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Hex")
                selected_object = hit.collider.gameObject;
        }
        else
        {
            //Important!!
            selected_object = null;
        }
    }

    void HandleMovingCamera()
    {
        transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y * 3), Space.Self);
        if (Input.GetKey(KeyCode.W))
            camera_holder.Translate(camera_holder.forward * Time.deltaTime * 50, Space.World);
        if (Input.GetKey(KeyCode.S))
            camera_holder.Translate(-camera_holder.forward * Time.deltaTime * 50, Space.World);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.A))
                camera_holder.RotateAround(camera_holder.position, new Vector3(0, 1, 0), 50 * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
                camera_holder.RotateAround(camera_holder.position, new Vector3(0, 1, 0), -50 * Time.deltaTime);
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
                camera_holder.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * 50, Space.Self);
            if (Input.GetKey(KeyCode.D))
                camera_holder.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 50, Space.Self);
        }
    }

    public int RandomModelID()
    {
        return Random.Range(0, grid_reference.models.Length);
    }
}
