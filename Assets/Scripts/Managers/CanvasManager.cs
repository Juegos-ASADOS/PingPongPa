using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text _scoreText;


    public void SetScoreText(int score)
    {
        //Animacion??????
        _scoreText.text = score.ToString();
    }

    public void SetLeaderBoard(List<int> elements)
    {
        Debug.Log("TODO: mostrar leaderboard");
    }
}
