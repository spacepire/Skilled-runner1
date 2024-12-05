using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine.UIElements;
using UnityEditorInternal;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Positions playerPositions;

    [Header("PlayerInput Value")]
    [SerializeField] float animationSpeed = 1f;
    [SerializeField] public float speed = 5f;

    [Header("Objects")]
    [SerializeField] Camera mainCam;

    private Vector3 moveZ = Vector3.forward;
    private bool isMove = false;
    private float shift = 4f;
    private float MaxSpeed;

    private void Start()
    {
        playerPositions = Positions.onMiddle;
        isMove = false;
    }
    private void Update()
    {
        InputControl();
        PlayerMove();
    }

    private void PlayerMove()
    {
        transform.Translate(moveZ * speed * Time.deltaTime);
    }

    private void IsMoveFalse()
    {
        isMove = false;
    }

    private void InputControl()
    {
        if (!isMove)
        {
            if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -3f)
            {
                if (playerPositions == Positions.onMiddle)
                    playerPositions = Positions.onLeft;
                else if (playerPositions == Positions.onRight)
                    playerPositions = Positions.onMiddle;

                transform.DOMoveX(transform.position.x - shift, animationSpeed).OnComplete(IsMoveFalse);
                isMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 3f)
            {
                if (playerPositions == Positions.onMiddle)
                    playerPositions = Positions.onRight;
                else if (playerPositions == Positions.onLeft)
                    playerPositions = Positions.onMiddle;

                transform.DOMoveX(transform.position.x + shift, animationSpeed).OnComplete(IsMoveFalse);
                isMove = true;
            }
        }
    }
}
