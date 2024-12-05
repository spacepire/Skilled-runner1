using UnityEngine;

public class CollectablesControler : MonoBehaviour
{
    [SerializeField] int score = 1;

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
                        score++;
                        Destroy(other.gameObject);
                    }
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
