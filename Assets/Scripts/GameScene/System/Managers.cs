using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Managers : MonoBehaviour
{
    public static Managers s_instance = null;
    public static Managers Instance {  get { return s_instance; } }

    private static QuestManager s_questManager = new QuestManager();
    private static ScoreManager s_scoreManager = new ScoreManager();
    private static GameManager s_gameManager = new GameManager();
    private static UIManager s_uiManager = new UIManager();
    private static ResourceManager s_resourceManager = new ResourceManager();

    public static QuestManager Quest { get { Init(); return s_questManager; } }
    public static ScoreManager Score { get {  Init(); return s_scoreManager; } }
    public static GameManager Game {  get {  Init(); return s_gameManager; } }
    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static ResourceManager Resource {  get { Init(); return s_resourceManager; } }


    private void Start()
    {
        Init();
    }

    public static void Init()
    {   
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };
            s_instance = Utils.GetOrAddComponent<Managers>(go);

            DontDestroyOnLoad(go);

            s_questManager.Init();
            s_gameManager.Init();
            s_scoreManager.Init();

            Application.targetFrameRate = 60;
        }
    }
}

