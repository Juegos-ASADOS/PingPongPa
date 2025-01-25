using System.Collections.Generic;
using UnityEngine;


public class LeaderBoardManager : MonoBehaviour
{

    [SerializeField]
    int numPlayersToShow;

    static string SCOREKEY = "SCORE_";

    List<int> leaderBoard = new List<int>();

    public List<int> GetElements()
    {
        return leaderBoard;
    }

    private void Awake()
    {
        //LoadLeaderboard();
    }

    public void SaveLeaderBoard()
    {
        for (int i = 0; i < numPlayersToShow; i++)
        {
            PlayerPrefs.DeleteKey(SCOREKEY + i.ToString());
        }
        leaderBoard.RemoveRange(numPlayersToShow, 10);
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
                leaderBoard.Add(i);
            }
        }
        leaderBoard.Sort();
    }
}
