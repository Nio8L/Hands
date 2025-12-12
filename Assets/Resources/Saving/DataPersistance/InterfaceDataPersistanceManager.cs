using UnityEngine;

public interface InterfaceDataPersistance
{

    void LoadData(GameData data);
    void SaveData(ref GameData data); 

}