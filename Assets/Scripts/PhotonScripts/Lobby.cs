using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Lobby : MonoBehaviourPunCallbacks
{
    public ButtonListManager roomListButtons;
    public Text roomToCreateName;
    //public List<Player> players = new List<Player>();


    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomToCreateName.text);
    }

    public void roomListButtonsOnClick(string name)
    {
        JoinRoom(name);
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("In room");
        PhotonNetwork.AutomaticallySyncScene = true;
        SceneManager.LoadScene("room_lobby");
        //PhotonNetwork.Instantiate("MyPrefabName", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //roomListButtons.ClearButtonList();
        Debug.Log("Romm list updated");
        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList && room.IsVisible)
            {
                roomListButtons.CreateButton(room.Name);
            }
            else
            {
                roomListButtons.DeleteButton(room.Name);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby() was called by PUN.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(string.Format("OnJoinRoomFailed() was called by PUN. {0}, {1}", returnCode, message));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

}
