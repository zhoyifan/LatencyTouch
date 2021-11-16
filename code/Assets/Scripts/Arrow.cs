using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject target=null;
    public GameObject viewer;
    public GameObject targetParent;
    // Start is called before the first frame update
    void Start()
    {
        // findTarget(out target);
    }

    // Update is called once per frame
    void Update()
    {
        if(target==null||target.activeSelf==false){
            if(!findTarget(out target)){
                return;
            }
        }
        transform.position=
        new Vector3(viewer.transform.position.x+viewer.transform.forward.x/3
        ,viewer.transform.position.y+viewer.transform.forward.y/3
        ,viewer.transform.position.z+viewer.transform.forward.z/3);
        transform.LookAt(target.transform);
    }
    bool findTarget(out GameObject target){
        GameObject[] targets=GameObject.FindGameObjectsWithTag("target");
        if(targets.Length!=1){
            if(!targetParent.GetComponent<TargetParent>().isFinished){
                Debug.Log("current target number is "+targets.Length+", wrong.");
            }
            target=null;
            return false;
        }
        target=targets[0];
        return true;
    }
}
