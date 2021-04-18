using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerDataManager : MonoBehaviourPun
{
    public Dictionary<int, PlayerData> players_data = new Dictionary<int, PlayerData>();

    #region RPC Calls

    public void AddPlayerDataRPC(int player_id)
	{
		photonView.RPC("_addPlayerData", RpcTarget.All, player_id);
	}

	#endregion


	#region Manager Methods

    [PunRPC]
	private void _addPlayerData(int player_id)
    {
        PlayerData player_data_instance = new PlayerData
        {
            id = player_id
        };
        players_data.Add(player_id, player_data_instance);
    }

	#endregion

}
