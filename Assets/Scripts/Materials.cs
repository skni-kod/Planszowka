using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    [System.Serializable]
 public struct material
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int amount;// {  get; }
        [SerializeField]
        private Text UI;
        public material(string _name,int _amount,Text _UI)
        {
            name =_name;
            amount = _amount;
            UI = _UI;
            UI.text= name + ": " + amount.ToString();
        }
        public void update(int _amount)
        {
            amount += _amount;
            if (amount < 0) amount = 0;
            UI.text = name+": "+ amount.ToString();
        }
        public int getAmount()
        {
            return amount;
        }
    }
    [Header("resources")]
    public material Gold;
    public material Wood;
    [Header("For test ,gold=(g button is -1 ,h button is +1),wood=(V button is -1 ,B button is +1)")]
    public bool test;
    private void Start()
    {
        Gold.update(0);
        
        Wood.update(0);
    }
    public void Update()
    {
        if(test)
        {
            if (Input.GetKeyDown(KeyCode.G)) { Gold.update(-1); }
            if (Input.GetKeyDown(KeyCode.H)) { Gold.update(1); }
            if (Input.GetKeyDown(KeyCode.V)) { Wood.update(-1); }
            if (Input.GetKeyDown(KeyCode.B)) { Wood.update(1); }
        }
    }
}
