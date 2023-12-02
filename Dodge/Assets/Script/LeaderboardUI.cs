using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardUI : MonoBehaviour
{

    public static LeaderboardUI Instance { get; private set; }


    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        Instance = this;

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Reset Position
        GetComponent<RectTransform>().sizeDelta = Vector2.zero; // Reset Size

        entryContainer = transform.Find("highscoreEntryContainer");
        if (entryContainer == null)
        {
            Debug.LogError("Entry container not found!");
            return;
        }

        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        if (entryTemplate == null)
        {
            Debug.LogError("Entry template not found!");
            return;
        }

        entryTemplate.gameObject.SetActive(false);


        entryTemplate.gameObject.SetActive(false);

        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    private void CreateHighscoreEntryTransform(LeaderboardSingle leaderboardSingle, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30.0f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = leaderboardSingle.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = leaderboardSingle.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;


        transformList.Add(entryTransform);
    }


    public void ShowLeaderboard(Leaderboard leaderboard)
    {
        gameObject.SetActive(true);

        // Sort entry list by Score
        for (int i = 0; i < leaderboard.leaderboardSingleList.Count; i++)
        {
            for (int j = i + 1; j < leaderboard.leaderboardSingleList.Count; j++)
            {
                if (leaderboard.leaderboardSingleList[j].score > leaderboard.leaderboardSingleList[i].score)
                {
                    // Swap
                    LeaderboardSingle tmp = leaderboard.leaderboardSingleList[i];
                    leaderboard.leaderboardSingleList[i] = leaderboard.leaderboardSingleList[j];
                    leaderboard.leaderboardSingleList[j] = tmp;
                }
            }
        }

        if (highscoreEntryTransformList != null)
        {
            foreach (Transform highscoreEntryTransform in highscoreEntryTransformList)
            {
                Destroy(highscoreEntryTransform.gameObject);
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (LeaderboardSingle leaderboardSingle in leaderboard.leaderboardSingleList)
        {
            CreateHighscoreEntryTransform(leaderboardSingle, entryContainer, highscoreEntryTransformList);
        }
    }

}
