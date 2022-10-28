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
    bool isWalking;
    float v;
    int isWalkingHash;
    bool eDeath=false;
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
        //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).fullPathHash);

        isWalking=animator.GetBool(isWalkingHash);
        v=enMesh.velocity.sqrMagnitude;
        if(v != 0f && !isWalking&&!eDeath){
            animator.SetBool(isWalkingHash, true);
        }
        if(v == 0f && isWalking&&!eDeath){
            animator.SetBool(isWalkingHash, false);
        }

        if(enMesh.remainingDistance<2.1f&&!eDeath){
            RandomPos();
            enMesh.destination=dest;            
        }

        if(!eDeath && animator.GetCurrentAnimatorStateInfo(0).fullPathHash==-1546996312){
            eDeath=true;
            enMesh.destination=transform.position;
        }
        
        if(eDeath && animator.GetCurrentAnimatorStateInfo(0).fullPathHash!=-1546996312){
            eDeath=false;
        }

        if(dest!=marker.transform.position && !eDeath){
            enMesh.destination =marker.transform.position;
        }
    }

    public void RandomPos(){
        Debug.Log("RandomPos");
        dest=new Vector3(Random.Range(targetObject1.transform.position.x,targetObject2.transform.position.x), 0.5f, Random.Range(targetObject1.transform.position.z,targetObject2.transform.position.z));
        marker.transform.position=dest;        
        reachCounter.text=(int.Parse(reachCounter.text)+1).ToString();
    }



    
}
