using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [SerializeField] private Paddle.Side paddleThatScored;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Ball ball = collider.GetComponent<Ball>();

        if (ball)
        {
            GameController.instance.Score(paddleThatScored);
        }
    }
}
