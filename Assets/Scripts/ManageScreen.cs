using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ManageScreen : MonoBehaviour
{
    [SerializeField] public GameObject endScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] public float score = 0;
    [SerializeField] public float bestScore = 0;
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text endText;
    [SerializeField] private Text endTextNum;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text bestScoreTextStart;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject activeScore;
    [SerializeField] private GameObject playingButtons;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject reapeatButton;

    public object GameRoot { get; private set; }

    private void Start()
    {
        load();
        endScreen.SetActive(false);
        activeScore.SetActive(false);
        startScreen.SetActive(true);
        healthBar.SetActive(false);
        playingButtons.SetActive(false);
        spawner.SetActive(false);
        Time.timeScale = 0;
        bestScoreTextStart.text = "Your Best Score: " + bestScore; 
    }
    private void Update()
    {
        Show();
        scoreDisplay.text = "Score: " + score;
        endText.text = "Your Score!";
        endTextNum.text = "" + score;
        bestScoreText.text = "Your Best Score: " + bestScore;
        bestScoreTextStart.text = "Your Best Score: " + bestScore;
        if (Input.GetKeyDown("x"))
        {
            score++;
        }
        if(score > bestScore)
        {
            bestScore = score;
        }
        JSONClass JSON = new JSONClass();
        Debug.Log(JSON.score);
    }
    private void Show()
    {

        if (GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth <= 0)
        {
            endScreen.SetActive(true);
            healthBar.SetActive(false);
            activeScore.SetActive(false);
            Time.timeScale = 0;
        }      
    }
    public void StartButton()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
        activeScore.SetActive(true);
        healthBar.SetActive(true);
        playingButtons.SetActive(true);
        spawner.SetActive(true);
        score = 0;
    }
    public void RepeatButton()
    {
        endScreen.SetActive(false);
        //reapeatButton.SetActive(false);
        activeScore.SetActive(false);
        startScreen.SetActive(true);
        healthBar.SetActive(false);
        playingButtons.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerMove>().currentPlayerHealth = GameObject.Find("Player").GetComponent<PlayerMove>().playerMaxHealth;
        Time.timeScale = 0;
        score = 0;
        save();
    }

    public void save()
    {
        JSONClass JSON = new JSONClass();
        JSON.score = bestScore;    
        string json = JsonUtility.ToJson(JSON);

        File.WriteAllText(Application.dataPath + "saveFile.json", json);
        Debug.Log(json + "saved");
    }
    public void load()
    {
        if (File.Exists(Application.dataPath + "saveFile.json"))
        {
            JSONClass JSON = new JSONClass();
            string json = File.ReadAllText(Application.dataPath + "saveFile.json");
            JSONClass loaded = JsonUtility.FromJson<JSONClass>(json);
            bestScore = loaded.score;        
            Debug.Log("score: " + loaded.score);    
        }
    }

    [Serializable]
    public class JSONClass
    {
        public float score = 0;    
    }
}
