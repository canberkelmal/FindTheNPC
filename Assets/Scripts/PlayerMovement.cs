using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    
    
    NavMeshAgent pMesh;
    public Vector3 target;
    public Text hitCounter;
    public LayerMask layerMask;

    void Start()
    {
        pMesh = GetComponent<NavMeshAgent>();
        hitCounter.text="0";
        //target=new Vector3(6, 0.5f, 6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector3.forward, Color.red);

        if(Input.GetMouseButton(0)){

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                pMesh.destination = hit.point;
                target=hit.point;
                Debug.Log(hit.transform.name);
                Debug.DrawRay(transform.position, Vector3.forward, Color.red);
            }
            if(Physics.Raycast(transform.position, Vector3.forward, out hit, 5f, layerMask)){
                Debug.Log(hit.transform.name);
            }

            //target=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //target.z=0.5f;
            //pMesh.destination=target;
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Enemy"){
            Debug.Log("hit");
            other.gameObject.transform.position=new Vector3(Random.Range(-7,7), 0.5f, Random.Range(-7,7));
            hitCounter.text=(int.Parse(hitCounter.text)+1).ToString();
        }
    }
}
