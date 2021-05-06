using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOCK : MonoBehaviour
{
    public FLOCKMANAGER myManager; //pega o manager *prefab não pega
    private GameObject _FM; //intermediador para captar o manager caso o gameobject associado a esse script seja um prefab
    float speed; 

    private void Awake()
    {

        //originalmente os prefabs não tinha o MANAGER associado a eles, já que o MANAGER estava em CENA,
        //criei uma variável privada _FM que se identifiicar o myManager como nulo(1), associa a variável _FM ao manager(2) que anteriormente era impossível ser enxergado por um prefab
        //faz a captação do script do manager em CENA "GetComponent<FLOCKMANAGER>" (3)
        if (myManager == null) //(1)
        {
            _FM = GameObject.FindGameObjectWithTag("_FM"); //(2)
            myManager = _FM.GetComponent<FLOCKMANAGER>(); //(3)
        }
    }

    private void Start()
    {
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed); //velocide de swmming individual do flocking
    }

    private void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
