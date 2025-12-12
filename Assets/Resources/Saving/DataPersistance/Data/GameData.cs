using UnityEngine;
using System.Collections.Generic;
using System.Collections;


[System.Serializable]
public class GameData {
    public string[,] tiles;

    public GameData() {
        this.tiles = new string[0,0];

    }  

    
}