using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public Text timeText;
    public Text recordText;

    private float surviveTime;
    private bool isGameover;
    
    void Start()
    {
        surviveTime = 0;
        isGameover = false;
    }

    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time: " + (int)surviveTime;
        }
        else 
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if(surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }

        recordText.text = "Best Time: " + (int)bestTime;

        UI_InputWindow.Show_Static("Name", "", "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ -.,;:_", 50,
            () => { UI_Blocker.Hide_Static(); },
            (string inputString) => {

                // Submit score: Name: inputString; Score: score

                LeaderboardSingle leaderboardSingle = new LeaderboardSingle
                {
                    name = inputString,
                    score = (int)surviveTime
                };

                WebRequests.PostJson("https://tutorialleader.azurewebsites.net/api/AddScore?code=bMtTAJrA7deK6FJWik41zF14kX03nQQ54rXs1voCtaViAzFu1Nb-gQ==",
                    JsonConvert.SerializeObject(leaderboardSingle),
                    (string error) => {
                        Debug.Log("Error: " + error);
                    },
                    (string response) => {
                        Debug.Log("Response: " + response);

                        Leaderboard leaderboard = JsonConvert.DeserializeObject<Leaderboard>(response);

                        LeaderboardUI.Instance.ShowLeaderboard(leaderboard);
                    });

            });
    }
}
