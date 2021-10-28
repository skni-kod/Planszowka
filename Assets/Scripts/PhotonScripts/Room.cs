using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Room : MonoBehaviourPunCallbacks
{
    public ButtonListManager roomListButtons;
    public List<Player> players = new List<Player>();
    public GameObject start_button;
    public GameObject waitin_text;
    private int timer = 19;


    void Start()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            start_button.SetActive(true);
        }
        else
        {
            waitin_text.SetActive(true);
        }
    }

    void BackToLobby()
    {
        //SceneManager.LoadSceneAsync("multiplayer_lobby");
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("online_game");
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    void Update()
    {
        if (timer > -1)
        {
            if (timer > 0)
            {
                timer -= 1;
            }
            else
            {
                UpdatePlayerList();
            }
        }
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void UpdatePlayerList()
    {
        players.Clear();
        roomListButtons.ClearButtonList();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!player.IsLocal)
            {
                players.Add(player);
                roomListButtons.CreateButton(player.NickName==""?player.ActorNumber.ToString():player.NickName);
            }
        }
    }
    public void UpdatePlayerNickName()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsLocal) 
            { 
               
                player.NickName = FindObjectOfType<InputField>().text;
                PlayerPrefs.SetString("nick", player.NickName);
            }

        }
    }
    
}
