using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOCKMANAGER : MonoBehaviour
{

    public GameObject fishPrefab; //prefabs para instanciar no flocking
    public int numFish = 20; //numero de swimmingers
    public GameObject[] allFishs;
    public Vector3 swinLimits = new Vector3(5, 5, 5);

    public Vector3 goalPos;

    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed; //velocidade miníma
    [Range(0.0f, 5.0f)]
    public float maxSpeed; //velocidade máxima
    [Range(1.0f, 10.0f)]
    public float neighbourDistance; //distancia entre instances
    [Range(0.0f, 5.0f)]
    public float rotationSpeed; //velocidade de rotação


    private void Start()
    {
        allFishs = new GameObject[numFish]; //tamanho do array
        for(int i = 0; i < numFish; i++)
        {
            //posição dentro dos limites para casa instancia
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));

            allFishs[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity); //instancia os prefabs
            allFishs[i].GetComponent<FLOCK>().myManager = this;
        }

        goalPos = this.transform.position;

    }

    void Update()
    {
        goalPos = this.transform.position;
        if (Random.Range(0, 100) < 10)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swinLimits.x,
            swinLimits.x),
            Random.Range(-swinLimits.y,
            swinLimits.y),
            Random.Range(-swinLimits.z,
            swinLimits.z));
        }
    }


}
