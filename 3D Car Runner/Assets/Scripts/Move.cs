using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine.UIElements;
using UnityEditorInternal;

public class Move : MonoBehaviour
{
    public Positions playerPositions;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] Camera mainCam;

    //[SerializeField] Camera cam;
    [SerializeField] float animationSpeed = 1f;
    [SerializeField] float speed = 5f;

    [SerializeField] GameObject[] smoke;
    [SerializeField] GameObject[] fire;

    private Vector3 moveZ = Vector3.forward;
    private bool isMove = false;
    private float shift = 6f;
    int i = 1;
    private bool force = false;
    private float nSpeed;
    /*
    [Space(10)]
    [Header("Particle Effects")]
    [SerializeField] GameObject[] particle;*/




    private void Start()
    {
        nSpeed = speed;
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

    private void PlayerSlowFalse()
    {
        force = false;

    }

    private void PlayerForce()
    {
        while (speed <= nSpeed)
        {
            speed += 1 * Time.deltaTime;
        }
    }

    private void IsMoveFalse()
    {
        isMove = false;
    }

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
                        scoreText.text = ($"Score: {i}");
                        i++;
                        Destroy(other.gameObject);
                    }
                    break;
                case (CollectablesEnum.booster):
                    {

                        mainCam.fieldOfView += 50;
                        speed += 50;
                        gameObject.GetComponentInChildren<Collider>().enabled = false;
                        foreach (var item in smoke)
                        {
                            item.SetActive(false);
                        }
                        foreach (var item in fire)
                        {
                            item.SetActive(true);
                        }
                        Invoke("BoosterPowerUp", 5);
                    }
                    break;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (speed > 20 && !force)
            {
                force = true;
                speed -= 40;
                Invoke("PlayerForce", 2);
            }
            else
            {
                Destroy(gameObject);
            }
            Invoke("PlayerSlowFalse", 1);

                Destroy(collision.gameObject);

        }
    }
}
