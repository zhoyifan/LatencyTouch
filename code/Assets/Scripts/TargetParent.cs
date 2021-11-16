using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetParent : MonoBehaviour
{
    //number of child target.
    private int n_child;
    private int indexnow;
    private int[] order;
    private int[] orderPosInLayer;  
    public bool isFinished;//only used in arrow
    public GameObject targetStart;
    public GameObject stopWatch;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public GameObject head;
    // Start is called before the first frame update
    private Vector3[,] directions = new Vector3[,]//x coordinate difference.
    {
         {
             new Vector3(0.7f,0f,0f),new Vector3(0.7f,0f,0.3f),new Vector3(0.7f,0f,-0.3f)
             ,new Vector3(0.7f,0.3f,0f),new Vector3(0.7f,0.3f,0.3f),new Vector3(0.7f,0.3f,-0.3f)
             ,new Vector3(0.7f,-0.3f,0f),new Vector3(0.7f,-0.3f,0.3f),new Vector3(0.7f,-0.3f,-0.3f)
         },{
              new Vector3(1f,0f,0f),new Vector3(1f,0f,0.3f),new Vector3(1f,0f,-0.3f)
             ,new Vector3(1f,0.3f,0f),new Vector3(1f,0.3f,0.3f),new Vector3(1f,0.3f,-0.3f)
             ,new Vector3(1f,-0.3f,0f),new Vector3(1f,-0.3f,0.3f),new Vector3(1f,-0.3f,-0.3f)
         },{
             new Vector3(1.3f,0f,0f),new Vector3(1.3f,0f,0.3f),new Vector3(1.3f,0f,-0.3f)
             ,new Vector3(1.3f,0.3f,0f),new Vector3(1.3f,0.3f,0.3f),new Vector3(1.3f,0.3f,-0.3f)
             ,new Vector3(1.3f,-0.3f,0f),new Vector3(1.3f,-0.3f,0.3f),new Vector3(1.3f,-0.3f,-0.3f)
         }
    };
    //private Vector3[,] directions = new Vector3[,]//x coordinate difference.
    //{
    //     {
    //         new Vector3(0.5f,0f,0f),new Vector3(0.5f,0f,0.5f),new Vector3(0.5f,0f,-0.5f)
    //         ,new Vector3(0.5f,0.5f,0f),new Vector3(0.5f,0.5f,0.5f),new Vector3(0.5f,0.5f,-0.5f)
    //         ,new Vector3(0.5f,-0.5f,0f),new Vector3(0.5f,-0.5f,0.5f),new Vector3(0.5f,-0.5f,-0.5f)
    //     },{
    //          new Vector3(1f,0f,0f),new Vector3(1f,0f,0.5f),new Vector3(1f,0f,-0.5f)
    //         ,new Vector3(1f,0.5f,0f),new Vector3(1f,0.5f,0.5f),new Vector3(1f,0.5f,-0.5f)
    //         ,new Vector3(1f,-0.5f,0f),new Vector3(1f,-0.5f,0.5f),new Vector3(1f,-0.5f,-0.5f)
    //     },{
    //         new Vector3(1.5f,0f,0f),new Vector3(1.5f,0f,0.5f),new Vector3(1.5f,0f,-0.5f)
    //         ,new Vector3(1.5f,0.5f,0f),new Vector3(1.5f,0.5f,0.5f),new Vector3(1.5f,0.5f,-0.5f)
    //         ,new Vector3(1.5f,-0.5f,0f),new Vector3(1.5f,-0.5f,0.5f),new Vector3(1.5f,-0.5f,-0.5f)
    //     }
    //};
    //private Vector3[,] directions=new Vector3[,]//z coordinate difference.
    //{
    //    {
    //        new Vector3(0f,0f,0.5f),new Vector3(0.5f,0f,0.5f),new Vector3(-0.5f,0f,0.5f)
    //        ,new Vector3(0f,0.5f,0.5f),new Vector3(0.5f,0.5f,0.5f),new Vector3(-0.5f,0.5f,0.5f)
    //        ,new Vector3(0f,-0.5f,0.5f),new Vector3(0.5f,-0.5f,0.5f),new Vector3(-0.5f,-0.5f,0.5f)
    //    },{
    //        new Vector3(0f,0f,1f),new Vector3(0.5f,0f,1f),new Vector3(-0.5f,0f,1f)
    //        ,new Vector3(0f,0.5f,1f),new Vector3(0.5f,0.5f,1f),new Vector3(-0.5f,0.5f,1f)
    //        ,new Vector3(0f,-0.5f,1f),new Vector3(0.5f,-0.5f,1f),new Vector3(-0.5f,-0.5f,1f)
    //    },{
    //        new Vector3(0f,0f,1.5f),new Vector3(0.5f,0f,1.5f),new Vector3(-0.5f,0f,1.5f)
    //        ,new Vector3(0f,0.5f,1.5f),new Vector3(0.5f,0.5f,1.5f),new Vector3(-0.5f,0.5f,1.5f)
    //        ,new Vector3(0f,-0.5f,1.5f),new Vector3(0.5f,-0.5f,1.5f),new Vector3(-0.5f,-0.5f,1.5f)
    //    }
    //};
    private Vector3 center = new Vector3(0f, 1.4f, 0f);
    private long[] latenciesHead = {0, 100, 200 };
    private long[] latenciesController = {0, 200, 400};
    private float[] scales = {0.025f, 0.05f, 0.1f };
    //private float[] scales={0.05f,0.1f,0.2f};
    // private float[] scales={0.5f,1f,1.5f};
    private Combination[] combinations;
    private class Combination{
        public int layer;
        public int orderPosInLayer;
        public long latencyHead;
        public long latencyController;
        public Vector3 direction;
        public float scale;
        public Combination(int l,int OPIL, long latH, long latC, Vector3 dir, float s){
            layer=l;
            orderPosInLayer=OPIL;
            latencyHead=latH;
            latencyController=latC;
            direction=dir;
            scale=s;
        }
        public void print(GameObject head){
            Debug.Log("Layer:"+layer+"  orderPosInLayer:"+orderPosInLayer+"  headLatency:"+latencyHead+"  latencyController:"+latencyController
            +"  direction:"+direction+"  scale:"+scale
            +".   While hitting the target, head is at "+head.transform.position);
        }
    } 
    void Start()
    {
        Debug.Log("start target position:"+targetStart.transform.position);
        n_child= directions.GetLength(0)*latenciesHead.Length* latenciesController.Length
        *scales.Length;
        Debug.Log("n_child:"+n_child);
        order=Enumerable.Range(0, n_child).ToArray();
        // assign position order from 0 to number of positions in a layer.
        orderPosInLayer=Enumerable.Range(0, directions.GetLength(1)).ToArray();
        //repeat position order for several times.
        int nRepeat=latenciesHead.Length*latenciesController.Length*scales.Length
        /directions.GetLength(1);
        for(int i=0;i<nRepeat;i++){
            orderPosInLayer=orderPosInLayer.Concat(orderPosInLayer).ToArray();
        }
        //shuffle the order;
        System.Random r = new System.Random();
        order=order.OrderBy(x => r.Next()).ToArray();
        combinations=new Combination[n_child];
        int iCombination=0;
        for(int iLayer=0;iLayer<directions.GetLength(0);iLayer++){
            orderPosInLayer = orderPosInLayer.OrderBy(x => r.Next()).ToArray();
            int iOrder=0;
            for(int iHead=0;iHead< latenciesHead.Length;iHead++){
                for(int iController=0;iController< latenciesController.Length;iController++){
                    for(int iScale=0;iScale<scales.Length;iScale++){
                        combinations[iCombination]=new Combination(
                            iLayer, orderPosInLayer[iOrder]
                            , latenciesHead[iHead],latenciesController[iController]
                            ,directions[iLayer,orderPosInLayer[iOrder]],scales[iScale]
                        );
                        iOrder=iOrder+1;
                        iCombination=iCombination+1;
                    }
                }
            }
        }
        //initialize the transparent symbols.
        int countSymbol = 1;
        for (int i=0;i<directions.GetLength(0);i++) {
            if (i == 1) {
                continue;
            }
            int[] index = { 4,5,7,8};
            for (int j =0;j<index.Length;j++) {
                Transform transChild = transform.GetChild(countSymbol);
                countSymbol = countSymbol + 1;
                transChild.position = directions[i,index[j]]+ center + new Vector3(-1f, 0f, 0f);
                transChild.gameObject.SetActive(true);
            }
        }
        isFinished=false;
        //start from 0 th(not start), as the getchild function parameter.
        indexnow=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //TargetStart reappears, timing ends and displays, go back to starting point
    public void GoBack(){
        stopWatch.GetComponent<StopWatch>().Pause();
        combinations[order[indexnow]].print(head);
        Debug.Log(indexnow+". "+order[indexnow]+"th combination ends, at time "+stopWatch.GetComponent<UnityEngine.UI.Text>().text);
        // Debug.Log("Head is at "+head.transform.position);
        indexnow=indexnow+1;
        if(indexnow>=n_child){
            isFinished=true;
            Debug.Log("Finish!");
            return;
        }
        this.ChangeLatency();
        targetStart.SetActive(true);
    }
    //next target appears, start timing again.
    public void ReadyGo(){
        targetStart.SetActive(false);
        
        if(indexnow>=n_child){
            isFinished=true;
            Debug.Log("Finish!");
            // StartCoroutine(EndProgram());
        }else{
            Transform transChild=transform.GetChild(0);
            //change position and scale.
            transChild.position =
            combinations[order[indexnow]].direction
            //+targetStart.transform.position;
            + center+new Vector3(-1f,0f,0f);
            transChild.localScale=
            new Vector3(combinations[order[indexnow]].scale,combinations[order[indexnow]].scale,combinations[order[indexnow]].scale);
            transChild.gameObject.SetActive(true);
            stopWatch.GetComponent<StopWatch>().Timing();
            Debug.Log(indexnow+". "+order[indexnow]+"th combination starts");
            // combinations[order[indexnow]].print(head);
        }
    }
    public void ChangeLatency() {
        //change head and controller latency.
        Valve.VR.SteamVR_Behaviour_Pose controllerScript = controllerRight.GetComponent<Valve.VR.SteamVR_Behaviour_Pose>();
        controllerScript.ChangeLatency(combinations[order[indexnow]].latencyController);
        FollowWithLag headScript = head.GetComponent<FollowWithLag>();
        headScript.ChangeLatency(combinations[order[indexnow]].latencyHead);
    }
    // IEnumerator EndProgram(){
    //     yield return new WaitForSeconds(5);
    //     Application.Quit();
    // }
}
