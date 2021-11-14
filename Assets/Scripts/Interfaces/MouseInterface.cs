using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInterface : MonoBehaviour
{
    public static Vector3 mousePosOnMap { get; private set; } = Vector3.zero;
    public static HexCords hexUnderMouse { get; private set; } = new HexCords();
    public static bool isOnUI { get; private set; } = false;

    static Plane plane = new Plane(new Vector3(0, 1, 0).normalized, Vector3.zero);
    Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

    private static MouseInterface instance = null;

    private static List<Action> onClickFuncRegistry = new List<Action>();
    private static List<Action> onClickRigthFuncRegistry = new List<Action>();
    private static List<Action> onClickUIFuncRegistry = new List<Action>();


    private void Update()
    {
        if (!screenRect.Contains(Input.mousePosition))
            return;

        isOnUI = IsMouseOverUI();
        GetMousePosOnMap();
        if (isOnUI)
        {

        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                OnClick();
            else if (Input.GetMouseButtonDown(1))
                OnClickRigth();
        }

    }

    public static void RegisterOnClick(Action func)
    {
        onClickFuncRegistry.Add(func);
    }
    public static void RegisterOnClickRigth(Action func)
    {
        onClickRigthFuncRegistry.Add(func);
    }
    public static void RegisterOnClickUI(Action func)
    {
        onClickUIFuncRegistry.Add(func);
    }

    private static void OnClick()
    {
        foreach (Action func in onClickFuncRegistry)
            func.Invoke();
    }

    private static void OnClickRigth()
    {
        foreach (Action func in onClickRigthFuncRegistry)
            func.Invoke();
    }

    private static void OnClickUI()
    {
        foreach (Action func in onClickUIFuncRegistry)
            func.Invoke();
    }

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
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

    private static void GetMousePosOnMap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float enter = 0.0f;

        if (plane.Raycast(ray, out enter))
        {
            mousePosOnMap = ray.GetPoint(enter);
            hexUnderMouse = new HexCords(HexCoordinatesSystem.CartesianToHexCords(mousePosOnMap));
        }
    }

}
