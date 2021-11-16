using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
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
        transform.parent.gameObject.GetComponent<TargetParent>().GoBack();
        gameObject.SetActive(false);
    }
}
