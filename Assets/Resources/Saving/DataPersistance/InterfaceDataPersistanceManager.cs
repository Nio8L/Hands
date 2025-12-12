using UnityEngine;

public interface InterfaceDataPersistanceManager
{

    void LoadData(GameData data);
    void SaveData(ref GameData data); 

}