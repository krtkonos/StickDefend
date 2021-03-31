using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// copied from previous project, isn used in this
public enum SaveType
{
    MainData,

}

public class SavingController : MonoBehaviour
{
    private const string MAIN_DATA = "Maindata.json";
    private const string OPTIONS_DATA = "Options.json";


    private MainData _Data = null;
    public MainData MainData
    {
        get { return _Data; }
    }

 

    public void Init()
    {
        _Data = TryLoadData();

        if (_Data == null)
        {
            _Data = new MainData();
            TrySaveData(SaveType.MainData);
        }

        
    }

    private MainData TryLoadData()
    {
        string mainData = LoadData(GetFileName(SaveType.MainData));

        if (string.IsNullOrEmpty(mainData)) return null;

        MainData data = null;
        try
        {

            data = JsonUtility.FromJson<MainData>(mainData);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return data;
    }



    public void TrySaveData(SaveType pSaveType)
    {
        string fileName = GetFileName(pSaveType);

        string content = "";

        switch (pSaveType)
        {
            case SaveType.MainData:
                content = JsonUtility.ToJson(_Data);
                break;
            
        }


        SaveData(fileName, content);
    }

    private void SaveData(string pFileName, string pOutput)
    {
        string path = GetPath(pFileName);
        File.WriteAllText(path, pOutput);
    }

    private string LoadData(string pFileName)
    {
        string path = GetPath(pFileName);
        string data = string.Empty;
        if (File.Exists(path))
        {
            data = File.ReadAllText(path);
        }

        return data;
    }

    private string GetPath(string pFileName)
    {
        string dirPath = null;
#if UNITY_EDITOR
        dirPath = Path.Combine(Application.dataPath, "StreamingAssetsEditor");
#else
        dirPath = Application.persistentDataPath;
#endif
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
        return Path.Combine(dirPath, pFileName);
    }

    private string GetFileName(SaveType pSaveType)
    {
        string fileName = "";

        switch (pSaveType)
        {
            case SaveType.MainData:
                fileName = MAIN_DATA;
                break;
            
        }

        return fileName;
    }
}

[Serializable]
public class MainData
{
    public float _GamePlayTime;
    public int _Highscore;
}


