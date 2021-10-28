using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class NickScript : MonoBehaviourPunCallbacks
{
    public string playerNick;
    public GameObject confirmationAboutNewNick;
    // Start is called before the first frame update
    void Start()
    {
       foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player.IsLocal)
            {
                
                playerNick = PlayerPrefs.GetString("nick", "Player: " + player.ActorNumber.ToString());
                player.NickName = playerNick;
                GetComponentInParent<InputField>().text = playerNick;
                return;
            }
        }
      
    }

    public void prepareSetOffConfirmation()
    {
        Invoke("setOffConfirmation", 2f);
    }
    private void setOffConfirmation()
    {
        confirmationAboutNewNick.SetActive(false);
    }
}
