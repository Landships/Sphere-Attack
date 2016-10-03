using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Movement : NetworkBehaviour
{

    /// <summary>
    /// At the moment, the owner updates the server constantly and the server lerps to the new position constantly.
    /// Other clients also receive updates constantly (rate of owner update = rate of client updates = rate of CmdSendUpdates())
    /// 
    /// When clients receive updates, the keepThreshold is used to determine whether to discard the new updates.
    /// Lerping right now is done with one frame buffer (the last received and kept sync value)
    /// </summary>
    /// 
    GameObject cameraHead;
    GameObject cameraLeft;
    GameObject cameraRight;

    Transform head;
    Transform leftHand;
    Transform rightHand;

    //Lerping
    public float headLerpPosRate;
    public float headLerpRotRate;
    public float handLerpPosRate;
    public float handLerpRotRate;

    //Determines client refresh threshold
    public float keepThresholdPos;
    public float keepThresholdRot;

    Vector3 syncHeadPos;
    public Vector3 syncLeftPos;
    public Vector3 syncRightPos;
    Quaternion syncHeadRot;
    Quaternion syncLeftRot;
    Quaternion syncRightRot;


    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {

        if (isLocalPlayer && cameraHead != null)
        {

            ReadCameraRig();
            CmdSendUpdates(head.position, head.rotation, leftHand.position, leftHand.rotation, rightHand.position, rightHand.rotation);

        }

        else
        {
            LerpTransforms();
        }

    }


    void ReadCameraRig()
    {
        head.position = cameraHead.transform.position;
        head.rotation = cameraHead.transform.rotation;
        leftHand.position = cameraLeft.transform.position;
        leftHand.rotation = cameraLeft.transform.rotation;
        rightHand.position = cameraRight.transform.position;
        rightHand.rotation = cameraRight.transform.rotation;

    }


    void LerpTransforms()
    {
        head.position = Vector3.Lerp(head.position, syncHeadPos, Time.fixedDeltaTime * headLerpPosRate);

        head.rotation = Quaternion.Lerp(head.rotation, syncHeadRot, Time.fixedDeltaTime * headLerpRotRate);

        leftHand.position = Vector3.Lerp(leftHand.position, syncLeftPos, Time.fixedDeltaTime * handLerpPosRate);

        leftHand.rotation = Quaternion.Lerp(leftHand.rotation, syncLeftRot, Time.fixedDeltaTime * handLerpRotRate);

        rightHand.position = Vector3.Lerp(rightHand.position, syncRightPos, Time.fixedDeltaTime * handLerpPosRate);

        rightHand.rotation = Quaternion.Lerp(rightHand.rotation, syncRightRot, Time.fixedDeltaTime * handLerpRotRate);

    }

    [Command]
    void CmdSendUpdates(Vector3 headpos, Quaternion headrot, Vector3 leftpos, Quaternion leftrot, Vector3 rightpos, Quaternion rightrot)
    {
        syncHeadPos = headpos;
        syncHeadRot = headrot;
        syncLeftPos = leftpos;
        syncLeftRot = leftrot;
        syncRightPos = rightpos;
        syncRightRot = rightrot;

        RpcSendUpdates(headpos, headrot, leftpos, leftrot, rightpos, rightrot);
    }


    [ClientRpc]
    void RpcSendUpdates(Vector3 headpos, Quaternion headrot, Vector3 leftpos, Quaternion leftrot, Vector3 rightpos, Quaternion rightrot)
    {
        if (Vector3.Distance(syncHeadPos, headpos) > keepThresholdPos)
        {
            syncHeadPos = headpos;
        }

        if (Quaternion.Angle(syncHeadRot, headrot) > keepThresholdRot)
        {
            syncHeadRot = headrot;
        }

        if (Vector3.Distance(syncLeftPos, leftpos) > keepThresholdPos)
        {
            syncLeftPos = leftpos;
        }

        if (Quaternion.Angle(syncLeftRot, leftrot) > keepThresholdRot)
        {
            syncLeftRot = leftrot;
        }

        if (Vector3.Distance(syncRightPos, rightpos) > keepThresholdPos)
        {
            syncRightPos = rightpos;
        }

        if (Quaternion.Angle(syncRightRot, rightrot) > keepThresholdRot)
        {
            syncRightRot = rightrot;
        }
    }

    public void InitMovement(GameObject camerahead, GameObject cameraleft, GameObject cameraright, Transform _head, Transform _left, Transform _right)
    {
        head = _head;
        leftHand = _left;
        rightHand = _right;
        cameraHead = camerahead;
        cameraLeft = cameraleft;
        cameraRight = cameraright;
    }

    public void InitMovement(Transform _head, Transform _left, Transform _right)
    {
        head = _head;
        leftHand = _left;
        rightHand = _right;
    }




}
