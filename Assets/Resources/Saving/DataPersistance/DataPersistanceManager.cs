using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;

public class NewMonoBehaviourScript : MonoBehaviour {
    private GameData gameData;
    private List<InterfaceDataPersistance> dataPersistance;

     
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static DataPersistanceManager instance {
        get; 
        private set;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistance Manager in the folder");
        }
        instance = this;
    }

    private void StartGame()
    {
        LoadGame();
    }

    private void OnAppQuit()
    {
        saveGame();
    }


    public void newGame()
    {
     this.gameData = new GameData();   
    }

    public void loadGame()
    {

        if (this.gameData == null)
        {
            newGame();
        }
        
    }

    public void saveGame()
    {
        
    }
}