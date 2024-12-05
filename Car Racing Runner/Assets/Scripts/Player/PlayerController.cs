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
    //[SerializeField] TMP_Text scoreText;
    [SerializeField] Camera mainCam;
/*
    [Header("SFX")] 
    [SerializeField] GameObject[] smoke;
    [SerializeField] GameObject[] fire;*/

    private Vector3 moveZ = Vector3.forward;
    private bool isMove = false;
    private float shift = 4f;
    int i = 1;
    private bool force = false;
    private float nSpeed;
    float acceleration = 100f;
    bool destroyFalse = true;

    private void Start()
    {
        nSpeed = speed;
        speed = 20f;
        //StartCoroutine(PlayerVelocity());
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

    #region Şimdilik işe yaramaz
    /*
    private void PlayerSlowFalse()
    {
        force = false;
    }*/


    /*
    private void PlayerForce()
    {
        while (speed <= nSpeed)
        {
            speed += 1 * Time.deltaTime;
        }
    }*/

    /*
private void BoosterPowerUp()
{
    mainCam.fieldOfView -= 50;
    speed -= 50;
    gameObject.GetComponentInChildren<Collider>().enabled = true;
    foreach (var item in smoke)
    {
        item.SetActive(true);
    }
    foreach (var item in fire)
    {
        item.SetActive(false);
    }
}*/
    #endregion

    private void IsMoveFalse()
    {
        isMove = false;
    }

    IEnumerator PlayerVelocity()
    {
        while (speed <= nSpeed)
        {
            speed += 10 * Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator PlayerForce()
    {
        destroyFalse = false;
        while (speed <= nSpeed)
        {
            speed += acceleration * Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
        destroyFalse = true;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectables"))
        {
            CollectablesEnum otherEnum = other.gameObject.GetComponent<CollectablesMaterials>().collect;
            switch (otherEnum)
            {
                case (CollectablesEnum.coin):
                    {
                        //scoreText.text = ($"Score: {i}");
                        i++;
                        Destroy(other.gameObject);
                    }
                    break;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && destroyFalse == true)
        {
            if (speed > 20 && !force)
            {
                force = true;
                speed -= 40;
                StartCoroutine(PlayerForce());
            }
            else
            {
                StopAllCoroutines();
                speed = 0;
                isMove = true; 
            }

            Destroy(collision.gameObject);
        }
    }
}
