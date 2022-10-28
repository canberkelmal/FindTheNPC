using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    
    
    NavMeshAgent pMesh;
    bool isWalking;
    public GameObject enemy;
    public Vector3 target;
    public Text hitCounter;
    public LayerMask layerMask;
    Animator animator;
    float v;
    int isWalkingHash;
    bool attacked=false;
    bool attackStarted=false;

    void Start()
    {
        pMesh = GetComponent<NavMeshAgent>();
        hitCounter.text="0";
        animator=transform.GetChild(0).GetComponent<Animator>();
        isWalkingHash=Animator.StringToHash("isWalking");
        //target=new Vector3(6, 0.5f, 6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Debug.DrawRay(transform.position, Vector3.forward, Color.red);

        //Debug.Log(pMesh.velocity.sqrMagnitude);
        isWalking=animator.GetBool(isWalkingHash);
        v=pMesh.velocity.sqrMagnitude;
        if(v != 0f && !isWalking){
            animator.SetBool(isWalkingHash, true);
        }
        if(v == 0f && isWalking){
            animator.SetBool(isWalkingHash, false);
        }

        if(Input.GetMouseButton(0)){

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                pMesh.destination = hit.point;
                target=hit.point;
                //Debug.Log(hit.transform.name);
                //Debug.DrawRay(transform.position, Vector3.forward, Color.red);
            }
            if(Physics.Raycast(transform.position, Vector3.forward, out hit, 5f, layerMask)){
                //Debug.Log(hit.transform.name);
            }

            //target=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //target.z=0.5f;
            //pMesh.destination=target;
        }
    
        
    
    }

    void Update(){
        if(attacked && animator.GetCurrentAnimatorStateInfo(0).fullPathHash==1130333774){
            attackStarted=true;
        }
        
        if(attackStarted &&  attacked && animator.GetCurrentAnimatorStateInfo(0).fullPathHash!=1130333774){
            attacked=false;
            attackStarted=false;
            enemy.gameObject.transform.position=new Vector3(Random.Range(-7,7), 0.5f, Random.Range(-7,7));
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Enemy" && !attackStarted){
            enemy=other.gameObject;
            Debug.Log("hit");
            hitCounter.text=(int.Parse(hitCounter.text)+1).ToString();
            animator.SetTrigger("Attack");
            attacked=true;
            
        }
    }
}
