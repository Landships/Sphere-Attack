using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Init : NetworkBehaviour
{


    public bool isHost;
    new public bool isClient;
    new public bool isServer;
    public bool isLocal;


    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    GameObject cameraRig;
    GameObject cameraHead;
    GameObject cameraLeft;
    GameObject cameraRight;

    //NetworkedPlayerEvents Events;

    // Use this for initialization
    void Start()
    {

        CustomManager manager = GameObject.Find("NetworkManager").GetComponent<CustomManager>();
        isHost = manager.isHost;
        isClient = manager.isClient;
        isServer = manager.isServer;
        isLocal = isLocalPlayer;

        head = transform.GetChild(2);
        leftHand = transform.GetChild(0);
        rightHand = transform.GetChild(1);

        //Events = GetComponent<NetworkedPlayerEvents>();

        // Set up
        if (isLocalPlayer)
        {
            cameraRig = GameObject.Find("[CameraRig]");
            cameraRig.transform.position = transform.position;
            cameraRig.transform.rotation = transform.rotation;

            cameraHead = cameraRig.transform.GetChild(2).gameObject;
            cameraLeft = cameraRig.transform.GetChild(0).gameObject;
            cameraRight = cameraRig.transform.GetChild(1).gameObject;
            InitPlayer();
        }

        else
        {
            //InitListener();
        }
    }


    
    void InitPlayer()
    {
        GetComponent<Movement>().InitMovement(cameraHead, cameraLeft, cameraRight, head, leftHand, rightHand);
        //Events.AssignControllers(cameraLeft, cameraRight);
        //HandEvent[] handevents = GetComponentsInChildren<HandEvent>();
       // handevents[0].AssignEvents(Events);
        //handevents[1].AssignEvents(Events);
    }

    void InitListener()
    {
        GetComponent<Movement>().InitMovement(head, leftHand, rightHand);
        HandEvent[] handevents = GetComponentsInChildren<HandEvent>();
        handevents[0].AssignEvents(Events);
        handevents[1].AssignEvents(Events);
    }
    */

}
