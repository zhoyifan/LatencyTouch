using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetStart : MonoBehaviour
{
    private bool hasCollided;
    public GameObject targetParent;
    private void OnEnable() {
        hasCollided=false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(this.hasCollided == true){ return; }
        hasCollided=true;
        targetParent.GetComponent<TargetParent>().ReadyGo();
        gameObject.SetActive(false);
    }
}
