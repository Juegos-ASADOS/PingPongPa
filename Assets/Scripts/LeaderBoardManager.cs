using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoardComparer : IComparer<LeaderBoardElement>
{
    public int Compare(LeaderBoardElement x, LeaderBoardElement y)
    {
        if (x.score > y.score)
            return 1;
        if (x.score < y.score)
            return -1;
        else
            return 0;
    }
}

public class LeaderBoardElement
{
    public string name;
    public int score;
}

public class LeaderBoardManager : MonoBehaviour
{

    [SerializeField]
    int numPlayersToShow;

    Dictionary<string, int> nameScore = new Dictionary<string, int>();

    static string SCOREKEY = "SCORE_";

    SortedSet<LeaderBoardElement> leaderBoard = new SortedSet<LeaderBoardElement>(new LeaderBoardComparer());

    public List<LeaderBoardElement> GetElements()
    {
        return leaderBoard.ToList();
    }

    private void Awake()
    {
        LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        List<string> names = LoadNames();

        int index = 0;
        foreach (string name in names)
        {
            string scoreKey = index.ToString();
            scoreKey += name;

            int score = PlayerPrefs.GetInt(scoreKey, -1);
            if (score >= 0)
            {
                LeaderBoardElement element = new LeaderBoardElement();
                element.name = name;
                element.score = score;
                leaderBoard.Add(element);
            }
            index++;
        }
    }
    private List<string> LoadNames()
    {
        var list = new List<string>();

        for (int i = 0; i < numPlayersToShow; i++)
        {
            string loadedName = PlayerPrefs.GetString(i.ToString(), string.Empty);

            if (loadedName == string.Empty)
                break;

            list.Add(loadedName);
        }

        return list;
    }
}
