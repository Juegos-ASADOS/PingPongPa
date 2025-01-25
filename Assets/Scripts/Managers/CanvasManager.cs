using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text _scoreText;

    [SerializeField]
    RectTransform _leaderBoardElementsParent;

    [SerializeField]
    BoardElement _elementPrefab;


    public void SetScoreText(int score)
    {
        //Animacion??????
        _scoreText.text = score.ToString();
    }

    public void SetLeaderBoard(List<int> elements)
    {
        int index = 1;
        foreach (var element in elements)
        {
            var elementInstance = Instantiate(_elementPrefab, _leaderBoardElementsParent);
            elementInstance.Position.text = index.ToString() + "º";
            elementInstance.Score.text = element.ToString();
            index++;
        }
    }
}
