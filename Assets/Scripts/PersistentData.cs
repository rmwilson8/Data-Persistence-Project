using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;

    [field: SerializeField] public string CurrentName {  get; private set; } // field not necessary for these variables. Just here for testing purposes
    [field: SerializeField]public string HighScoreName { get; private set; }
    [field: SerializeField] public int HighScore {  get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than one PersistenData in scene. Destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        RestoreGameData(); // Loads the daved data
    }

    private void Start()
    {
        SaveGameData();
    }

    public void SetCurrentName(string inputName)
    {
        CurrentName = inputName;
    }

    public void SetHighScore(int highScore)
    {
        if(highScore > HighScore)
        {
            HighScore = highScore;
            HighScoreName = CurrentName;
        }
    }

    public void StartGame(string inputText)
    {
        SetCurrentName(inputText);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    public class SaveData
    {
        public string HighScoreName;
        public int HighScore;
    }

    public void SaveGameData()
    {
        SaveData mySaveData = new SaveData();
        mySaveData.HighScoreName = HighScoreName;
        mySaveData.HighScore = HighScore;

        string jsonData = JsonUtility.ToJson(mySaveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonData);

        Debug.Log($"Saving game at the path {Application.persistentDataPath}/savefile.json");
    }

    private void RestoreGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            SaveData mySaveData = JsonUtility.FromJson<SaveData>(jsonData);

            HighScoreName = mySaveData.HighScoreName;
            HighScore = mySaveData.HighScore;
        }

        else
        {
            HighScoreName = null;
            HighScore = 0;
        }
    }

    public void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            HighScore = 0;
            HighScoreName = null;
            File.Delete(path);
            Debug.Log("Deleting save");

            SaveGameData(); // creates empty game file
        }
    }
}
