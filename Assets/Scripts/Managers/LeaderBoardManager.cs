using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LeaderBoardManager : MonoBehaviour
{

    [SerializeField]
    int numPlayersToShow;

    static string SCOREKEY = "SCORE_";

    [SerializeField]
    List<int> leaderBoard = new List<int>();

    public List<int> GetElements()
    {
        return leaderBoard;
    }

    private void Awake()
    {
        //LoadLeaderboard();
    }

    public void TryToAddScore(int score)
    {
        if (leaderBoard.Last() >= score)
            return;

        leaderBoard.Add(score);
        leaderBoard.Sort();
        leaderBoard.Reverse();

        if (leaderBoard.Count > numPlayersToShow)
        {
            leaderBoard.RemoveAt(numPlayersToShow);
        }
    }

    private void OnApplicationQuit()
    {
        SaveLeaderBoard();
    }

    public void SaveLeaderBoard()
    {
        for (int i = 0; i < numPlayersToShow; i++)
        {
            PlayerPrefs.DeleteKey(SCOREKEY + i.ToString());
        }
        if (leaderBoard.Count >= numPlayersToShow)
        {
            leaderBoard.RemoveRange(numPlayersToShow, leaderBoard.Count - numPlayersToShow - 1);
        }
        int index = 0;
        foreach (int i in leaderBoard)
        {
            PlayerPrefs.SetInt(SCOREKEY + index.ToString(), i);
            index++;
        }
    }

    public void LoadLeaderboard()
    {
        for (int i = 0; i < numPlayersToShow; i++)
        {
            int score = PlayerPrefs.GetInt(SCOREKEY + i.ToString(), -1);
            if (score >= 0)
            {
                leaderBoard.Add(score);
            }
        }
        leaderBoard.Sort();
        leaderBoard.Reverse();
    }
}
