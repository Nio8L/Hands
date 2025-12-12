using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;

public class DataPersistanceManager {
    private GameData gameData;
    private List<InterfaceDataPersistance> dataPersistance()
    {
        IEnumerable<InterfaceDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<InterfaceDataPersistance>();
        return new List<InterfaceDataPersistance>(dataPersistanceObjects);
    }

     
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
        loadGame();
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
        foreach (InterfaceDataPersistance dataPersistanceObj in dataPersistance())
        {
            dataPersistanceObj.loadData(gameData);
        }
    }

    public void saveGame()
    {
        foreach (InterfaceDataPersistance dataPersistanceObj in dataPersistance())
        {
            dataPersistanceObj.saveData(ref gameData);
        } 
        
    }
}