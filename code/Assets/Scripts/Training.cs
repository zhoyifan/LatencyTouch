using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training : MonoBehaviour
{
    private bool hasCollided;
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
        transform.parent.gameObject.GetComponent<TrainingParent>().Next();
        
    }
}
