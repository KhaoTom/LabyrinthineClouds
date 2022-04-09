using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public enum Movement { None = -1, Forward, Backward, TurnLeft, TurnRight };

    public float moveDuration = 0.1f;

    public UnityEvent<string> OnMoveFinished;
    public UnityEvent<string> OnTurnFinished;

    public KeyCode[] MoveForwardKeys = { KeyCode.UpArrow, KeyCode.W };
    public KeyCode[] MoveBackwardKeys = { KeyCode.DownArrow, KeyCode.S };
    public KeyCode[] TurnLeftKeys = { KeyCode.LeftArrow, KeyCode.A };
    public KeyCode[] TurnRightKeys = { KeyCode.RightArrow, KeyCode.D };

    public bool isMoving = false;
    public bool isTurning = false;

    public float timeElapsed = 0;

    public Movement queuedMovement = Movement.None;
    public bool movementIsSelected = false;

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
        if (isMoving || isTurning)
        {
            QueueMovement();
        }
        else
        {
            QueueMovement();
            HandleMovement();
        }
    }

    private void QueueMovement()
    {
        if (Input.anyKeyDown)
        {
            if (InputIsMoveForward())
            {
                SelectMoveForward();
            }
            else if (InputIsMoveBackward())
            {
                SelectMoveBackward();
            }
            else if (InputIsTurnLeft())
            {
                SelectTurnLeft();
            }
            else if (InputIsTurnRight())
            {
                SelectTurnRight();
            }
        }
    }

    private bool InputIsTurnRight()
    {
        return InputInKeys(TurnRightKeys);
       
    }

    private bool InputIsTurnLeft()
    {
        return InputInKeys(TurnLeftKeys);
    }

    private bool InputIsMoveForward()
    {
        return InputInKeys(MoveForwardKeys);
    }

    private bool InputIsMoveBackward()
    {
        return InputInKeys(MoveBackwardKeys);
    }

    private bool InputInKeys(KeyCode[] keys)
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    private void HandleMovement()
    {
        string did_move = "";
        string did_turn = "";
        if (queuedMovement == Movement.Forward)
        {
            if (CanMoveForward())
            {
                StartCoroutine(LerpPosition(transform.position, transform.position + transform.forward * 2));
                did_move = "forward";
            }

        }
        else if (queuedMovement == Movement.Backward)
        {
            if (CanMoveBackward())
            {
                StartCoroutine(LerpPosition(transform.position, transform.position - transform.forward * 2));
                did_move = "backward";
            }
        }
        else if (queuedMovement == Movement.TurnRight)
        {
            if (CanTurnRight())
            {
                StartCoroutine(LerpLookAt(transform.position + transform.forward, transform.position + transform.right));
                did_turn = "right";
            }
        }
        else if (queuedMovement == Movement.TurnLeft)
        {
            if (CanTurnLeft())
            { 
                StartCoroutine(LerpLookAt(transform.position + transform.forward, transform.position - transform.right));
                did_turn = "left";
            }
        }

        if (did_move != "")
        {
            OnMoveFinished.Invoke(did_move);
            ClearMovement();
        }

        if (did_turn != "")
        {
            OnTurnFinished.Invoke(did_turn);
            ClearMovement();
        }
    }

    private bool CanTurnLeft()
    {
        Debug.LogWarning("CanTurnLeft Not Implemented!", this);
        return true;
    }

    private bool CanTurnRight()
    {
        Debug.LogWarning("CanTurnRight Not Implemented!", this);
        return true;
    }

    private bool CanMoveBackward()
    {
        Debug.LogWarning("CanMoveBackward Not Implemented!", this);
        return true;
    }

    private bool CanMoveForward()
    {
        Debug.LogWarning("CanMoveForward Not Implemented!", this);
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
