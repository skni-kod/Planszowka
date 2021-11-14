using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInterface : MonoBehaviour
{
    HexGrid hexgrid;
    public CameraScript camera_ref;
    public GameObject highligtht_model;
    public static GameObject highlight_reference;
    public GameObject select_model;
    public static GameObject select_reference;
    private static MapInterface instance = null;

    #region UnderscoreVariables
    static bool _highligthVisible;
    static bool _selectVisible;
    static HexCords _selectedHex;
    #endregion



    public bool highligthVisible
    {
        get { return _highligthVisible; }
        set { HighlightSetter(value); }
    }
    public static bool selectVisible
    {
        get { return _selectVisible; }
        set { SelectSetter(value); }
    }

    public static HexCords selectedHex
    {
        get { return _selectedHex; }
        set { SelectedHexSetter(value); }
    }
    public static HexCords highlightedHex = new HexCords(Vector3.zero);



    private void HighlightSetter(bool value)
    {
        _highligthVisible = value;
        MakeHighligthVisible(value);
    }
    private static void SelectSetter(bool value)
    {
        _selectVisible = value;
        MakeSelectVisible(value);
    }
    private static void SelectedHexSetter(HexCords value)
    {
        _selectedHex = value;
        if (value != null)
            selectVisible = true;
        else
            selectVisible = false;
    }

    public static void SelectHexUnderMouse()
    {
        selectedHex = MouseInterface.hexUnderMouse;
        Vector3 positionn = HexCoordinatesSystem.HexToCartesianCords(MouseInterface.hexUnderMouse.hex_crds);
        select_reference.transform.position = positionn + new Vector3(0, 0.001f, 0);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        highlight_reference = Instantiate(highligtht_model);
        select_reference = Instantiate(select_model);
        //selectVisible = true;
    }

    private static void MakeHighligthVisible(bool value)
    {
        highlight_reference.SetActive(value);
    }

    private static void MakeSelectVisible(bool value)
    {
        select_reference.SetActive(value);
    }


    void UpdateHighlight()
    {
        highlightedHex = MouseInterface.hexUnderMouse;
        Vector3 positionn =  HexCoordinatesSystem.HexToCartesianCords(MouseInterface.hexUnderMouse.hex_crds);
        highlight_reference.transform.position = positionn + new Vector3(0, 0.001f, 0);
    }

    private void Update()
    {
        if (!MouseInterface.isOnUI)
        {
            UpdateHighlight();
        }
    }
}
