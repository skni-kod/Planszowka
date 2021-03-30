using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text myText;

    [SerializeField]
    private ButtonListManager buttons_list;

    public void SetText(string textString)
    {
        myText.text = textString;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        buttons_list.OnButtonClick(myText.text);
    }
}
