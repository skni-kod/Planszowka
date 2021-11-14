using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MultiplayerSceneController : MonoBehaviour
{
    public HexGrid hexGrid = null;



    private void Awake()
    {
        MouseInterface.RegisterOnClickRigth(SpawnChunkOnMouse);
        MouseInterface.RegisterOnClickRigth(MapInterface.SelectHexUnderMouse);
    }

    void SpawnChunkOnMouse()
    {
        hexGrid.GenerateChunk(HexCords.CheckWhichChunkPointBelongsTo(MouseInterface.mousePosOnMap.x, MouseInterface.mousePosOnMap.z).hex_id);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
