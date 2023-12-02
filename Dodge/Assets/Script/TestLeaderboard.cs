using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;

public class TestLeaderboard : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            WebRequests.Get("https://tutorialleader.azurewebsites.net/api/GetLeaderboard?code=57xkQ1XKaMaPjE6jOvTn0owhmAVDi500m39tc03L5j0KAzFuzFet6A==",
                (string error) => {
                    Debug.Log("Error: " + error);
                },
                (string response) => {
                    Debug.Log("Response: " + response);

                    Leaderboard leaderboard = JsonConvert.DeserializeObject<Leaderboard>(response);

                    LeaderboardUI.Instance.ShowLeaderboard(leaderboard);
                });
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {

            LeaderboardSingle leaderboardSingle = new LeaderboardSingle
            {
                name = "Lee",
                score = 1
            };
            WebRequests.PostJson("https://tutorialleader.azurewebsites.net/api/AddScore?code=bMtTAJrA7deK6FJWik41zF14kX03nQQ54rXs1voCtaViAzFu1Nb-gQ==",
                JsonConvert.SerializeObject(leaderboardSingle),
                (string error) => {
                    Debug.Log("Error: " + error);
                },
                (string response) => {
                    Debug.Log("Response: " + response);
                });
        }
    }
}
