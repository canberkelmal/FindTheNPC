using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent enMesh;
    Animator animator;
    public GameObject targetObject1;
    public GameObject targetObject2;
    public GameObject marker;
    public Vector3 dest;
    public Text reachCounter;
    public bool mrk=true;
    bool isWalking;
    float v;
    int isWalkingHash;
    void Start()
    {
        enMesh = GetComponent<NavMeshAgent>();
        dest=Vector3.one;
        RandomPos();
        enMesh.destination=dest;
        reachCounter.text="0";
        animator=transform.GetChild(0).GetComponent<Animator>();
        isWalkingHash=Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        isWalking=animator.GetBool(isWalkingHash);
        v=enMesh.velocity.sqrMagnitude;
        if(v != 0f && !isWalking){
            animator.SetBool(isWalkingHash, true);
        }
        if(v == 0f && isWalking){
            animator.SetBool(isWalkingHash, false);
        }

        if(enMesh.velocity.sqrMagnitude == 0f && mrk){
            RandomPos();
            enMesh.destination=dest;
            mrk=false;
            reachCounter.text=(int.Parse(reachCounter.text)+1).ToString();
        }
        if(enMesh.velocity.sqrMagnitude >0.5f){
            mrk=true;
        }
    }

    public void RandomPos(){
        dest=new Vector3(Random.Range(targetObject1.transform.position.x,targetObject2.transform.position.x), 0.5f, Random.Range(targetObject1.transform.position.z,targetObject2.transform.position.z));
        marker.transform.position=dest;
    }

    
}
