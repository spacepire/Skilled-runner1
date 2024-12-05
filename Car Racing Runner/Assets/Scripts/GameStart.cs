using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStart : MonoBehaviour
{
    [SerializeField] PlayerController playerScp;
    [SerializeField] Material Material;
    [SerializeField] int startTime;
    void Start()
    {
        StartCoroutine(StartLight());
    }
    IEnumerator StartLight()
    {
            Material.color = Color.red;
            yield return new WaitForSeconds(startTime);
            Material.color = Color.yellow;
            yield return new WaitForSeconds(startTime);
            Material.color = Color.green;
        playerStart();
    }

    private void playerStart()
    {
        playerScp.enabled = true;
    }


}
