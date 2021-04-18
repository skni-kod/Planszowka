using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineGameManager : MonoBehaviourPun
{
    public PlayerDataManager data_manager_reference;
    public OnClickMenu on_click_menu;
    public HexGrid grid_reference;
    public CameraScript camera_reference;
    public HexCell left_clicked_hex;

    void Start()
    {
        //GameObject newobj = PhotonNetwork.Instantiate("CameraHolder", new Vector3(0, 0, 0), Quaternion.identity, 0);
        //dodaj sie do gry
        data_manager_reference.AddPlayerDataRPC(PhotonNetwork.LocalPlayer.ActorNumber);
    }

    void Update()
    {
        HandleHexSelecting();
        HandleMenuShowing();
    }

    public void ShowMenu()
    {
        on_click_menu.SelectMenu(left_clicked_hex);
        on_click_menu.Show();
    }

    public void HideMenu()
    {
        on_click_menu.Hide();
    }

    public void HandleHexSelecting()
    {
        if (camera_reference.selected_object != null)
        {
            HexCell hex = camera_reference.selected_object.GetComponent<HexCell>();
            if (Input.GetMouseButtonDown(1))
            {
                if (left_clicked_hex != null)
                    left_clicked_hex.UnPermaHighligth();
                left_clicked_hex = hex;
                left_clicked_hex.Highligth(permament: true);
            }
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)
                || Input.mouseScrollDelta.y != 0)
                && !Input.GetMouseButtonDown(0))
            {
                if (left_clicked_hex != null)
                    left_clicked_hex.UnPermaHighligth();
                left_clicked_hex = null;
            }
        }
    }

    public void HandleMenuShowing()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)
            || Input.mouseScrollDelta.y != 0 || Input.GetMouseButtonUp(0)) 
            && !Input.GetMouseButtonDown(0))
        {
            HideMenu();
        }
        if (Input.GetMouseButtonDown(1))
            ShowMenu();
    }

    #region RPCHandlers

    //Nie widać tu odwołań bo unity dziwnie to łapie, ale są w edytorze

    public void RotateBuilding()
    {
        if (left_clicked_hex != null)
        {
            grid_reference.RotateBuildingRPC(left_clicked_hex.id);
        }
    }

    public void PlaceBuilding()
    {
        if (left_clicked_hex != null)
        {
            grid_reference.PlaceBuildingRPC(left_clicked_hex.id);
        }
    }

    public void ClaimHex()
    {
        if (left_clicked_hex != null)
        {
            grid_reference.ClaimHexRPC(left_clicked_hex.id, PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }

    public void ChangeType(string typee)
    {
        if (left_clicked_hex != null)
        {
            grid_reference.ChangeTypeRPC(left_clicked_hex.id, typee);
        }
    }

    #endregion

}
