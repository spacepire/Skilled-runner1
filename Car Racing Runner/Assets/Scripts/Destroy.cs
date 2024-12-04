using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] Transform player;

    float calculate;

    private void Start()
    {
        calculate = player.position.z - transform.position.z;
    }
    private void Update()
    {
        if (gameObject != null)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - calculate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Road") || other.gameObject.CompareTag("Collectables") || other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
