using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;

    public bool isAI;

    private Ball ball;
    private BoxCollider2D col;

    private float randomYOffset;

    private Vector2 forwardDirection;
    private bool firstIncoming;

    public enum Side { Left, Right }
    [SerializeField] private Side side;

    [SerializeField] private float resetTime;
    private bool overridePosition;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
        col = GetComponent<BoxCollider2D>();

        if(side == Side.Left)
        {
            forwardDirection = Vector2.right;
        }
        else if(side == Side.Right)
        {
            forwardDirection = Vector2.left;    
        }


    }

    private void Update()
    {
        if (!overridePosition)
        {
            MovePaddle();
        }
        
    }

    private void MovePaddle()
    {
        float targetYPosition = GetNewYPosition();

        ClampPosition(ref targetYPosition);

        transform.position = new Vector3(transform.position.x, targetYPosition, transform.position.z);
    }

    private void ClampPosition(ref float yPosition)
    {
        float minY = Camera.main.ScreenToWorldPoint(new Vector3 (0,0)).y;
        float maxY = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height)).y;

        yPosition = Mathf.Clamp(yPosition, minY, maxY);
    }

    private float GetNewYPosition()
    {
        float result = transform.position.y;
        
        if (isAI)
        {
            if (BallIncoming())
            {

                if (firstIncoming)
                {
                    print("First");
                    firstIncoming = false;
                    randomYOffset = GetRandomOffset();
                }
                
                result = Mathf.MoveTowards(transform.position.y, ball.transform.position.y + randomYOffset, moveSpeed * Time.deltaTime);
            }
            else
            {
                firstIncoming = true;
            }
               
        }
        else
        {
            float movement = Input.GetAxisRaw("Vertical " + side.ToString()) * moveSpeed * Time.deltaTime;
            result = transform.position.y + movement;
        }

        return result;
    }

    private bool BallIncoming()
    {
        float dotP = Vector2.Dot(ball.velocity, forwardDirection);
        return dotP < 0f;
    }

    private float GetRandomOffset()
    {
        float maxOffSet = col.bounds.extents.y;
        return Random.Range(-maxOffSet,maxOffSet);
    }

    public void Reset()
    {
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        overridePosition = true;
        float startPosition = transform.position.y;
        for(float timer = 0; timer < resetTime; timer += Time.deltaTime)
        {
            float targetPosition = Mathf.Lerp(startPosition, 0f, timer / resetTime);
            transform.position = new Vector3(transform.position.x, targetPosition, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        overridePosition = false;
    }
}
