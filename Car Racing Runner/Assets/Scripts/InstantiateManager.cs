using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class InstantiateManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [Header("Objects")]
    [SerializeField] GameObject roads;
    [SerializeField] GameObject[] splines;

    [SerializeField] GameObject[] coinOrBuild;

    private float roadLenght = 681.5f;
    private float roadM = 30;

    public float terLenght = 681.5f;

    [SerializeField] float denemef = 1f;


    private float coinLenght = 50;

    private void Update()
    {

        if (player != null)
        {
                EnviromentsInstantiate();

            CollettibleInstantiate();
        }
    }


    private void EnviromentsInstantiate()
    {
        if (player.position.z >= roadLenght / 2)
        {
            Instantiate(roads, transform.position + new Vector3(0, 0, roadLenght), Quaternion.identity);
            roadLenght += roadM;
        }
        if (player.position.z >= terLenght / 2)
        {
            //splines
            Instantiate(splines[Random.Range(0, splines.Length)], transform.position + new Vector3(21f, 0, roadLenght), Quaternion.identity);
            terLenght += denemef;
            terLenght -= denemef;
            
            Instantiate(splines[Random.Range(0, splines.Length)], transform.position + new Vector3(-21f, 0, roadLenght), Quaternion.identity);
            terLenght += denemef;
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
                new Vector3(4, 0, coinLenght),
                new Vector3(-4, 0, coinLenght)
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

