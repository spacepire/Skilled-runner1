using UnityEngine;

public class Deneme : MonoBehaviour
{
    
        [SerializeField] Transform player;

        float calculate;

        private void Start()
        {
            calculate = player.position.z - transform.position.z;
        }
        private void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - calculate);
        }

}
