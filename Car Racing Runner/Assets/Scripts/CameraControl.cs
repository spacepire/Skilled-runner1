using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.Timeline;
using DG.Tweening;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Positions cameraPositions;
    [SerializeField] Transform player;
    [SerializeField] float rotationSpeed = 5f;

    float playerShift = 2f;
    float rotate = 10;
    float calculate = 0;
    
    
    private void Start()
    {
        cameraPositions = Positions.onMiddle;
        calculate = player.position.z - transform.position.z;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - calculate);

        }

        CameraRotation();
    }

    private void CameraRotation()
    {

        if (player.transform.position.x > playerShift && cameraPositions != Positions.onRight)
        {
            cameraPositions = Positions.onRight;

            transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.y + rotate,
                transform.rotation.eulerAngles.z), rotationSpeed);
        }
        else if (player.transform.position.x < -playerShift && cameraPositions != Positions.onLeft)
        {
            cameraPositions = Positions.onLeft;

            transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.y - rotate,
                transform.rotation.eulerAngles.z), rotationSpeed);
        }
        else if (player.transform.position.x > -playerShift && player.transform.position.x < playerShift && cameraPositions != Positions.onMiddle)
        {
            cameraPositions = Positions.onMiddle;

            transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.eulerAngles.x, 0f,
                transform.rotation.eulerAngles.z), rotationSpeed);
        }
    }
}
