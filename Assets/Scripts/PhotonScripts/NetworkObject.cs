using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkObject : MonoBehaviourPun, IPunObservable
{
    public int players = 0;


    public void IncreasePlayersCounter(int amount)
    {
        //photonView.RPC("IncPlayersCounter", RpcTarget.All, amount);
    }

    [PunRPC]
    private void IncPlayersCounter(int amount)
    {
        //players += amount;
    }


    // used as Observed component in a PhotonView, this only reads/writes the position
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            int data = players;
            stream.Serialize(ref data);
        }
        else
        {
            int data = 0;
            stream.Serialize(ref data);  // pos gets filled-in. must be used somewhere
            players = data;
        }
    }
}
