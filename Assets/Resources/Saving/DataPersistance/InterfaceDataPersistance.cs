using UnityEngine;

public interface InterfaceDataPersistance
{

    void loadData(GameData data);
    void saveData(ref GameData data); 

}