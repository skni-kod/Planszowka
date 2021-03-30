using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    public Lobby lobby_reference;

    public Dictionary<string, GameObject> buttons = new Dictionary<string, GameObject>();

    public void CreateButton(string name)
    {
        if (!buttons.ContainsKey(name))
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonListButton>().SetText(name);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(name, button);
        }
    }

    public void DeleteButton(string name)
    {
        Destroy(buttons[name]);
        buttons.Remove(name);
    }

    public void ClearButtonList()
    {
        foreach (GameObject button in buttons.Values)
        {
            Destroy(button);
        }
        buttons.Clear();
    }

    public void OnButtonClick(string name)
    {
        if (lobby_reference != null)
            lobby_reference.roomListButtonsOnClick(name);
    }
}

