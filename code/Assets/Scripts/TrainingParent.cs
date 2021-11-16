using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingParent : MonoBehaviour
{
    private Vector3[] positions=
    {
        new Vector3(0.4f,0f,0.4f),
        new Vector3(0.4f,0f,-0.4f)
    };
    public GameObject targetStart;
    public GameObject targetParent;
    private int indexnow;
    private Vector3 center = new Vector3(0f,1.4f,0f);
    // Start is called before the first frame update
    void Start()
    {
        indexnow=0;
        transform.GetChild(0).position=positions[indexnow]+ center;
        // transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Next(){
        Transform transChild=transform.GetChild(0);
        transChild.gameObject.SetActive(false);
        indexnow=indexnow+1;
        if(indexnow>=positions.Length){
            TargetParent targetParentScript = targetParent.GetComponent<TargetParent>();
            targetParentScript.ChangeLatency();
            targetStart.SetActive(true);
            return;
        }
        transChild.position=positions[indexnow] + center;
        indexnow=indexnow+1;
        transChild.gameObject.SetActive(true);
        
    }
}
