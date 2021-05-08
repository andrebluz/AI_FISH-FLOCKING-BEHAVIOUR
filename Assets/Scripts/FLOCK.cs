using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOCK : MonoBehaviour
{
    public FLOCKMANAGER myManager; //pega o manager *prefab não pega
    private GameObject _FM; //intermediador para captar o manager caso o gameobject associado a esse script seja um prefab
    float speed;

    public bool turning = false; //retorna a origem

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
        //transform.Translate(0, 0, Time.deltaTime * speed);

        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);
        RaycastHit hit = new RaycastHit();
        Vector3 direction = myManager.transform.position - transform.position;
        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
            turning = false;
        if (turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed,
                myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFishs;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance = 0; ;
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
            }
            if (nDistance <= myManager.neighbourDistance)
            {
                vcentre += go.transform.position;
                groupSize++;
                if (nDistance < 1.0f)
                {
                    vavoid = vavoid + (this.transform.position - go.transform.position);
                }
                FLOCK anotherFlock = go.GetComponent<FLOCK>();
                gSpeed = gSpeed + anotherFlock.speed;
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalPos - this.transform.position);
            speed = gSpeed / groupSize;
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
