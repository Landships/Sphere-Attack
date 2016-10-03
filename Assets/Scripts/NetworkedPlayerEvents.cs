using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using VRTK;


public struct NetworkedControllerInteractionEventArgs
{
    public uint controllerIndex;
    public float buttonPressure;
    public Vector2 touchpadAxis;
    public float touchpadAngle;
}

public delegate void NetworkedControllerInteractionEventHandler(object sender, NetworkedControllerInteractionEventArgs e);

public class NetworkedPlayerEvents : NetworkBehaviour
{

    public enum handedness { left, right };

    public event NetworkedControllerInteractionEventHandler LeftTriggerPressed;
    public event NetworkedControllerInteractionEventHandler LeftTriggerReleased;

    public event NetworkedControllerInteractionEventHandler RightTriggerPressed;
    public event NetworkedControllerInteractionEventHandler RightTriggerReleased;

    public event NetworkedControllerInteractionEventHandler LeftGripPressed;
    public event NetworkedControllerInteractionEventHandler LeftGripReleased;

    public event NetworkedControllerInteractionEventHandler RightGripPressed;
    public event NetworkedControllerInteractionEventHandler RightGripReleased;


    GameObject leftHand;
    GameObject rightHand;
    VRTK_ControllerEvents leftEvents;
    VRTK_ControllerEvents rightEvents;


    public void AssignControllers(GameObject left, GameObject right)
    {
        leftHand = left;
        rightHand = right;
        AddEventListeners();
        //leftEvents.TriggerReleased += refresh;
        //rightEvents.TriggerReleased += refresh;

    }


    NetworkedControllerInteractionEventArgs SetButtonEvent( ControllerInteractionEventArgs e)
    {
        NetworkedControllerInteractionEventArgs f;
        f.controllerIndex = e.controllerIndex;
        f.buttonPressure = e.buttonPressure;
        f.touchpadAxis = e.touchpadAxis;
        f.touchpadAngle = e.touchpadAngle;

        return f;
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////


    void EventLeftTriggerPressed(object sender,  ControllerInteractionEventArgs e)
    {
        OnTriggerPressed(SetButtonEvent(e), handedness.left);
    }

    void EventRightTriggerPressed(object sender,  ControllerInteractionEventArgs e)
    {
        OnTriggerPressed(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnTriggerPressed(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            Debug.Log("NetworkedEventTrigger");

            if (h == handedness.left)
            {
                if (LeftTriggerPressed != null)
                {
                    LeftTriggerPressed(this, e);
                    CmdOnTriggerPressed(e, h);
                }
            }
            else
            {
                if (RightTriggerPressed != null)
                {
                    RightTriggerPressed(this, e);
                    CmdOnTriggerPressed(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnTriggerPressed(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftTriggerPressed(this, e);
            RpcOnTriggerPressed(e, h);
        }
        else
        {
            RightTriggerPressed(this, e);
            RpcOnTriggerPressed(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnTriggerPressed(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftTriggerPressed(this, e);
            }
            else
            {
                RightTriggerPressed(this, e);
            }
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftTriggerReleased(object sender,  ControllerInteractionEventArgs e)
    {
        OnTriggerReleased(SetButtonEvent(e), handedness.left);
    }

    void EventRightTriggerReleased(object sender,  ControllerInteractionEventArgs e)
    {
        OnTriggerReleased(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnTriggerReleased(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftTriggerReleased != null)
                {
                    LeftTriggerReleased(this, e);
                    CmdOnTriggerReleased(e, h);
                }
            }
            else
            {
                if (RightTriggerReleased != null)
                {
                    RightTriggerReleased(this, e);
                    CmdOnTriggerReleased(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnTriggerReleased(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftTriggerReleased(this, e);
            RpcOnTriggerReleased(e, h);
        }
        else
        {
            RightTriggerReleased(this, e);
            RpcOnTriggerReleased(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnTriggerReleased(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftTriggerReleased(this, e);
            }
            else
            {
                RightTriggerReleased(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////


    void EventLeftGripPressed(object sender,  ControllerInteractionEventArgs e)
    {
        OnGripPressed(SetButtonEvent(e), handedness.left);
    }

    void EventRightGripPressed(object sender,  ControllerInteractionEventArgs e)
    {
        OnGripPressed(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnGripPressed(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftGripPressed != null)
                {
                    LeftGripPressed(this, e);
                    CmdOnGripPressed(e, h);
                }
            }
            else
            {
                if (RightGripPressed != null)
                {
                    RightGripPressed(this, e);
                    CmdOnGripPressed(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnGripPressed(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftGripPressed(this, e);
            RpcOnGripPressed(e, h);
        }
        else
        {
            RightGripPressed(this, e);
            RpcOnGripPressed(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnGripPressed(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftGripPressed(this, e);
            }
            else
            {
                RightGripPressed(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////


    void EventLeftGripReleased(object sender,  ControllerInteractionEventArgs e)
    {
        OnGripReleased(SetButtonEvent(e), handedness.left);
    }

    void EventRightGripReleased(object sender,  ControllerInteractionEventArgs e)
    {
        OnGripReleased(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnGripReleased(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftGripReleased != null)
                {
                    LeftGripReleased(this, e);
                    CmdOnGripReleased(e, h);
                }
            }
            else
            {
                if (RightGripReleased != null)
                {
                    RightGripReleased(this, e);
                    CmdOnGripReleased(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnGripReleased(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftGripReleased(this, e);
            RpcOnGripReleased(e, h);
        }
        else
        {
            RightGripReleased(this, e);
            RpcOnGripReleased(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnGripReleased(NetworkedControllerInteractionEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftGripReleased(this, e);
            }
            else
            {
                RightGripReleased(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////////////////////////////////

    void AddEventListeners()
    {
        leftEvents = leftHand.GetComponent<VRTK_ControllerEvents>();
        rightEvents = rightHand.GetComponent<VRTK_ControllerEvents>();

        leftEvents.TriggerPressed += new ControllerInteractionEventHandler(EventLeftTriggerPressed);
        leftEvents.TriggerReleased += new ControllerInteractionEventHandler(EventLeftTriggerReleased);
        leftEvents.GripPressed += new ControllerInteractionEventHandler(EventLeftGripPressed);
        leftEvents.GripReleased += new ControllerInteractionEventHandler(EventLeftGripReleased);


        rightEvents.TriggerPressed += new ControllerInteractionEventHandler(EventRightTriggerPressed);
        rightEvents.TriggerReleased += new ControllerInteractionEventHandler(EventRightTriggerReleased);
        rightEvents.GripPressed += new ControllerInteractionEventHandler(EventRightGripPressed);
        rightEvents.GripReleased += new ControllerInteractionEventHandler(EventRightGripReleased);

    }

    void removeEventListeners()
    {
        leftEvents.TriggerPressed -= new ControllerInteractionEventHandler(EventLeftTriggerPressed);
        leftEvents.TriggerReleased -= new ControllerInteractionEventHandler(EventLeftTriggerReleased);
        leftEvents.GripPressed -= new ControllerInteractionEventHandler(EventLeftGripPressed);
        leftEvents.GripReleased -= new ControllerInteractionEventHandler(EventLeftGripReleased);


        rightEvents.TriggerPressed -= new ControllerInteractionEventHandler(EventRightTriggerPressed);
        rightEvents.TriggerReleased -= new ControllerInteractionEventHandler(EventRightTriggerReleased);
        rightEvents.GripPressed -= new ControllerInteractionEventHandler(EventRightGripPressed);
        rightEvents.GripReleased -= new ControllerInteractionEventHandler(EventRightGripReleased);


    }
}