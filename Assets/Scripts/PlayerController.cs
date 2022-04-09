using System;
using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum Movement { None = -1, Forward, Backward, TurnLeft, TurnRight };

    public float moveDuration = 0.2f;
    public float inputQueueWindow = 0.15f;

    public bool isMoving = false;
    public bool isTurning = false;

    public float timeElapsed = 0;

    public Movement queuedMovement = Movement.None;
    public bool movementIsSelected = false;

    public KeyCode[] MoveForwardKeys = { KeyCode.UpArrow, KeyCode.W };
    public KeyCode[] MoveBackwardKeys = { KeyCode.DownArrow, KeyCode.W };
    public KeyCode[] TurnLeftKeys = { KeyCode.LeftArrow, KeyCode.A };
    public KeyCode[] TurnRightKeys = { KeyCode.RightArrow, KeyCode.D };

    public void SelectTurnRight()
    {
        queuedMovement = Movement.TurnRight;
        movementIsSelected = true;
    }

    public void SelectTurnLeft()
    {
        queuedMovement = Movement.TurnLeft;
        movementIsSelected = true;
    }

    public void SelectMoveForward()
    {
        queuedMovement = Movement.Forward;
        movementIsSelected = true;
    }

    public void SelectMoveBackward()
    {
        queuedMovement = Movement.Backward;
        movementIsSelected = true;
    }

    private void Update()
    {
        
        if ((isMoving || isTurning) && WithinMoveQueueWindow())
        {
            QueueMovement();
        }
        else
        {
            QueueMovement();
            HandleMovement();
        }
    }

    private bool WithinMoveQueueWindow()
    {
        return moveDuration - timeElapsed < inputQueueWindow;
    }

    private void QueueMovement()
    {
        if (Input.anyKeyDown)
        {
            if (InputMoveForward())
            {
                SelectMoveForward();
            }
            else if (InputMoveBackward())
            {
                SelectMoveBackward();
            }
            else if (InputTurnLeft())
            {
                SelectTurnLeft();
            }
            else if (InputTurnRight())
            {
                SelectTurnRight();
            }
        }
    }

    private bool InputTurnRight()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    }

    private bool InputTurnLeft()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
    }

    private bool InputMoveForward()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    private bool InputMoveBackward()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S);
    }

    private void HandleMovement()
    {
        bool did_move = false;
        bool did_turn = false;
        if (queuedMovement == Movement.Forward)
        {
            if (CanMoveForward())
            {
                StartCoroutine(LerpPosition(transform.position, transform.position + transform.forward * 2));
                did_move = true;
            }

        }
        else if (queuedMovement == Movement.Backward)
        {
            if (CanMoveBackward())
            {
                StartCoroutine(LerpPosition(transform.position, transform.position - transform.forward * 2));
                did_move = true;
            }
        }
        else if (queuedMovement == Movement.TurnRight)
        {
            if (CanTurnRight())
            {
                StartCoroutine(LerpLookAt(transform.position + transform.forward, transform.position + transform.right));
                did_move = true;
            }
        }
        else if (queuedMovement == Movement.TurnLeft)
        {
            if (CanTurnLeft())
            { 
                StartCoroutine(LerpLookAt(transform.position + transform.forward, transform.position - transform.right));
                did_move = true;
            }
        }

        if (did_move)
        {
            // TODO: invoke step event
            ClearMovement();
        }

        if (did_turn)
        {
            // TODO: invoke turn event
            ClearMovement();
        }
    }

    private bool CanTurnLeft()
    {
        Debug.LogWarning("Not Implemented!", this);
        return true;
    }

    private bool CanTurnRight()
    {
        Debug.LogWarning("Not Implemented!", this);
        return true;
    }

    private bool CanMoveBackward()
    {
        Debug.LogWarning("Not Implemented!", this);
        return true;
    }

    private bool CanMoveForward()
    {
        Debug.LogWarning("Not Implemented!", this);
        return true;
    }

    private void ClearMovement()
    {
        queuedMovement = Movement.None;
        movementIsSelected = false;
    }

    private IEnumerator LerpLookAt(Vector3 from, Vector3 to)
    {
        isTurning = true;
        timeElapsed = 0f;
        float startTime = Time.time;
        while (timeElapsed < moveDuration)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            Vector3 lv = Vector3.Lerp(from, to, (Time.time - startTime) / moveDuration);
            transform.LookAt(lv);
        }
        isTurning = false;
    }

    private IEnumerator LerpPosition(Vector3 from, Vector3 to)
    {
        isMoving = true;
        timeElapsed = 0f;
        float startTime = Time.time;
        while (timeElapsed < moveDuration)
        {
            yield return null;
            timeElapsed += Time.deltaTime;
            Vector3 lv = Vector3.Lerp(from, to, (Time.time - startTime) / moveDuration);
            transform.position = lv;
        }
        isMoving = false;
    }
}
