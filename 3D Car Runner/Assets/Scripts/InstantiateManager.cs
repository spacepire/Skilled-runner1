using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [Header("Objects")]
    [SerializeField] GameObject roads;
    [SerializeField] GameObject terrain;
    [SerializeField] GameObject[] coinOrBuild;
    [SerializeField] GameObject powerUp;


    private float roadLenght = 690;
    private float roadM = 30;

    private float terrainLenght = 684;
    private float terrainM = 350;

    private float coinLenght = 50;

    private void Update()
    {
        EnviromentsInstantiate();
        CollettibleInstantiate();
    }


    private void EnviromentsInstantiate()
    {
        if (player.position.z >= roadLenght / 5)
        {
            Instantiate(roads, transform.position + new Vector3(0, 0, roadLenght), Quaternion.identity);
            roadLenght += roadM;
        }
        if (player.position.z >= terrainLenght / 2)
        {
            Instantiate(terrain, transform.position + new Vector3(9, 0, terrainLenght), Quaternion.identity);
            Instantiate(terrain, transform.position + new Vector3(-109, 0, terrainLenght), Quaternion.identity);
            terrainLenght += terrainM;
        }
    }

    private void CollettibleInstantiate()
    {
        if (player.position.z >= coinLenght / 5)
        {
            List<int> usedIndices = new List<int>();

            Vector3[] positions = new Vector3[]
            {
                new Vector3(0, 0, coinLenght),
                new Vector3(6, 0, coinLenght),
                new Vector3(-6, 0, coinLenght)
            };

            foreach (Vector3 pos in positions)
            {
                int objectIndex;

                do
                {
                    objectIndex = Random.Range(0, coinOrBuild.Length);
                }
                while (usedIndices.Contains(objectIndex));

                usedIndices.Add(objectIndex);

                Instantiate(coinOrBuild[objectIndex], transform.position + pos, Quaternion.identity);
            }

            coinLenght += 60;
        }

    }
}

