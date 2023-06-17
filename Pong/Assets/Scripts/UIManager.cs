using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftScoreText;
    [SerializeField] private TextMeshProUGUI rightScoreText;

    [SerializeField] private FadeableUI menuUI;
    [SerializeField] private FadeableUI gameOverUI;

    [SerializeField] private TextMeshProUGUI winnerText;



    private void Start()
    {
        menuUI.FadeIn(true);
        gameOverUI.FadeOut(true);
    }


    public void UpdateScoreText(int leftScore, int rightScore)
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();

    }

    public void OnGameStart()
    {
        menuUI.FadeOut(false);
        gameOverUI.FadeOut(false);
    }

    public void ShowMenu()
    {
        gameOverUI.FadeOut(false);
        menuUI.FadeIn(false);
    }

    public void ShowGameOver(Paddle.Side side)
    {
        gameOverUI.FadeIn(false);

        if (side == Paddle.Side.Left)
            winnerText.text = "PLAYER 1";
        else if (side == Paddle.Side.Right)
            winnerText.text = "PLAYER 2";

    }

}
