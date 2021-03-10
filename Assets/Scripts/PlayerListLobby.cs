using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using multiplayerNamespace;
using Photon.Pun;
public class PlayerListLobby : MonoBehaviour
{

    public int number_of_players = 0;

    // Start is called before the first frame update
    void Start()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        children.ForEach(child => child.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != number_of_players)
        {
            number_of_players = (int)PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log("Player count changed");
        }
    }
}
