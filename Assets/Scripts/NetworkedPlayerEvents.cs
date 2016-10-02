using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;


public struct NetworkedControllerClickedEventArgs
{
    public uint controllerIndex;
    public float buttonPressure;
    public Vector2 touchpadAxis;
}

public struct NetworkedGestureEventArgs
{
    public uint controllerIndex;
    public Vector3 velocity;
    public Transform transform;
}



public delegate void NetworkedControllerClickedEventHandler(object sender, NetworkedControllerClickedEventArgs e);
public delegate void NetworkedGestureEventHandler(object sender, NetworkedGestureEventArgs e);

public class NetworkedPlayerEvents : NetworkBehaviour
{

    public enum handedness { left, right };

    public event NetworkedControllerClickedEventHandler LeftTriggerClicked;
    public event NetworkedControllerClickedEventHandler LeftTriggerUnclicked;

    public event NetworkedControllerClickedEventHandler LeftTriggerAxisChanged;

    public event NetworkedControllerClickedEventHandler LeftApplicationMenuClicked;
    public event NetworkedControllerClickedEventHandler LeftApplicationMenuUnclicked;

    public event NetworkedControllerClickedEventHandler LeftGripClicked;
    public event NetworkedControllerClickedEventHandler LeftGripUnclicked;

    public event NetworkedControllerClickedEventHandler LeftTouchpadClicked;
    public event NetworkedControllerClickedEventHandler LeftTouchpadUnclicked;

    public event NetworkedControllerClickedEventHandler LeftTouchpadTouched;
    public event NetworkedControllerClickedEventHandler LeftTouchpadUntouched;

    public event NetworkedControllerClickedEventHandler LeftTouchpadAxisChanged;

    public event NetworkedControllerClickedEventHandler RightTriggerClicked;
    public event NetworkedControllerClickedEventHandler RightTriggerUnclicked;

    public event NetworkedControllerClickedEventHandler RightTriggerAxisChanged;

    public event NetworkedControllerClickedEventHandler RightApplicationMenuClicked;
    public event NetworkedControllerClickedEventHandler RightApplicationMenuUnclicked;

    public event NetworkedControllerClickedEventHandler RightGripClicked;
    public event NetworkedControllerClickedEventHandler RightGripUnclicked;

    public event NetworkedControllerClickedEventHandler RightTouchpadClicked;
    public event NetworkedControllerClickedEventHandler RightTouchpadUnclicked;

    public event NetworkedControllerClickedEventHandler RightTouchpadTouched;
    public event NetworkedControllerClickedEventHandler RightTouchpadUntouched;

    public event NetworkedControllerClickedEventHandler RightTouchpadAxisChanged;


    public event NetworkedGestureEventHandler LeftUpwardGesture;
    public event NetworkedGestureEventHandler LeftDownwardGesture;
    public event NetworkedGestureEventHandler LeftOutwardGesture;
    public event NetworkedGestureEventHandler LeftInwardGesture;
    public event NetworkedGestureEventHandler LeftTwistGesture;

    public event NetworkedGestureEventHandler RightUpwardGesture;
    public event NetworkedGestureEventHandler RightDownwardGesture;
    public event NetworkedGestureEventHandler RightOutwardGesture;
    public event NetworkedGestureEventHandler RightInwardGesture;
    public event NetworkedGestureEventHandler RightTwistGesture;

    GameObject leftHand;
    GameObject rightHand;
    SteamVR_ControllerEvents leftEvents;
    SteamVR_ControllerEvents rightEvents;
    GestureEvents leftGestures;
    GestureEvents rightGestures;



    void Start()
    {

    }


    public void AssignControllers(GameObject left, GameObject right)
    {
        leftHand = left;
        rightHand = right;

        AddSpellListeners();
        //leftEvents.TriggerUnclicked += refresh;
        //rightEvents.TriggerUnclicked += refresh;

    }

    void refresh(object sender, ControllerClickedEventArgs e)
    {
        AddSpellListeners();
    }



    NetworkedControllerClickedEventArgs SetButtonEvent(ControllerClickedEventArgs e)
    {
        NetworkedControllerClickedEventArgs f;
        f.controllerIndex = e.controllerIndex;
        f.buttonPressure = e.buttonPressure;
        f.touchpadAxis = e.touchpadAxis;
        return f;
    }

    NetworkedGestureEventArgs SetGestureEvent(GestureEventArgs e)
    {
        NetworkedGestureEventArgs f;
        f.controllerIndex = e.controllerIndex;
        f.velocity = e.velocity;
        f.transform = e.transform;
        return f;
    }



    //////////////////////////////////////////////////////////////////////////////////////////////////


    void EventLeftTriggerClicked(object sender, ControllerClickedEventArgs e)
    {
        OnTriggerClicked(SetButtonEvent(e), handedness.left);
    }

    void EventRightTriggerClicked(object sender, ControllerClickedEventArgs e)
    {
        OnTriggerClicked(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnTriggerClicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            Debug.Log("NetworkedEventTrigger");

            if (h == handedness.left)
            {
                if (LeftTriggerClicked != null)
                {
                    LeftTriggerClicked(this, e);
                    CmdOnTriggerClicked(e, h);
                }
            }
            else
            {
                if (RightTriggerClicked != null)
                {
                    RightTriggerClicked(this, e);
                    CmdOnTriggerClicked(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnTriggerClicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftTriggerClicked(this, e);
            RpcOnTriggerClicked(e, h);
        }
        else
        {
            RightTriggerClicked(this, e);
            RpcOnTriggerClicked(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnTriggerClicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftTriggerClicked(this, e);
            }
            else
            {
                RightTriggerClicked(this, e);
            }
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftTriggerUnclicked(object sender, ControllerClickedEventArgs e)
    {
        OnTriggerUnclicked(SetButtonEvent(e), handedness.left);
    }

    void EventRightTriggerUnclicked(object sender, ControllerClickedEventArgs e)
    {
        OnTriggerUnclicked(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnTriggerUnclicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftTriggerUnclicked != null)
                {
                    LeftTriggerUnclicked(this, e);
                    CmdOnTriggerUnclicked(e, h);
                }
            }
            else
            {
                if (RightTriggerUnclicked != null)
                {
                    RightTriggerUnclicked(this, e);
                    CmdOnTriggerUnclicked(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnTriggerUnclicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftTriggerUnclicked(this, e);
            RpcOnTriggerUnclicked(e, h);
        }
        else
        {
            RightTriggerUnclicked(this, e);
            RpcOnTriggerUnclicked(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnTriggerUnclicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftTriggerUnclicked(this, e);
            }
            else
            {
                RightTriggerUnclicked(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////


    void EventLeftTriggerAxisChanged(object sender, ControllerClickedEventArgs e)
    {
        OnTriggerAxisChanged(SetButtonEvent(e), handedness.left);
    }

    void EventRightTriggerAxisChanged(object sender, ControllerClickedEventArgs e)
    {
        OnTriggerAxisChanged(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnTriggerAxisChanged(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftTriggerAxisChanged != null)
                {
                    LeftTriggerAxisChanged(this, e);
                    CmdOnTriggerAxisChanged(e, h);
                }
            }
            else
            {
                if (RightTriggerAxisChanged != null)
                {
                    RightTriggerAxisChanged(this, e);
                    CmdOnTriggerAxisChanged(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnTriggerAxisChanged(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftTriggerAxisChanged(this, e);
            RpcOnTriggerAxisChanged(e, h);
        }
        else
        {
            RightTriggerAxisChanged(this, e);
            RpcOnTriggerAxisChanged(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnTriggerAxisChanged(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftTriggerAxisChanged(this, e);
            }
            else
            {
                RightTriggerAxisChanged(this, e);
            }
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////

    /*

public virtual void OnApplicationMenuClicked(ControllerClickedEventArgs e)
{
    if (ApplicationMenuClicked != null)
        ApplicationMenuClicked(this, e);
}


public virtual void OnApplicationMenuUnclicked(ControllerClickedEventArgs e)
{
    if (ApplicationMenuUnclicked != null)
        ApplicationMenuUnclicked(this, e);
}
*/
    //
    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftGripClicked(object sender, ControllerClickedEventArgs e)
    {
        OnGripClicked(SetButtonEvent(e), handedness.left);
    }

    void EventRightGripClicked(object sender, ControllerClickedEventArgs e)
    {
        OnGripClicked(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnGripClicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftGripClicked != null)
                {
                    LeftGripClicked(this, e);
                    CmdOnGripClicked(e, h);
                }
            }
            else
            {
                if (RightGripClicked != null)
                {
                    RightGripClicked(this, e);
                    CmdOnGripClicked(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnGripClicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftGripClicked(this, e);
            RpcOnGripClicked(e, h);
        }
        else
        {
            RightGripClicked(this, e);
            RpcOnGripClicked(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnGripClicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftGripClicked(this, e);
            }
            else
            {
                RightGripClicked(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////


    void EventLeftGripUnclicked(object sender, ControllerClickedEventArgs e)
    {
        OnGripUnclicked(SetButtonEvent(e), handedness.left);
    }

    void EventRightGripUnclicked(object sender, ControllerClickedEventArgs e)
    {
        OnGripUnclicked(SetButtonEvent(e), handedness.right);
    }


    public virtual void OnGripUnclicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftGripUnclicked != null)
                {
                    LeftGripUnclicked(this, e);
                    CmdOnGripUnclicked(e, h);
                }
            }
            else
            {
                if (RightGripUnclicked != null)
                {
                    RightGripUnclicked(this, e);
                    CmdOnGripUnclicked(e, h);
                }
            }
        }
    }


    [Command]
    public virtual void CmdOnGripUnclicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftGripUnclicked(this, e);
            RpcOnGripUnclicked(e, h);
        }
        else
        {
            RightGripUnclicked(this, e);
            RpcOnGripUnclicked(e, h);
        }
    }


    [ClientRpc]
    public virtual void RpcOnGripUnclicked(NetworkedControllerClickedEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftGripUnclicked(this, e);
            }
            else
            {
                RightGripUnclicked(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////

    /*

public virtual void OnTouchpadClicked(NetworkedControllerClickedEventArgs e)
{
    if (TouchpadClicked != null)
        TouchpadClicked(this, e);
}

public virtual void OnTouchpadUnclicked(NetworkedControllerClickedEventArgs e)
{
    if (TouchpadUnclicked != null)
        TouchpadUnclicked(this, e);
}

public virtual void OnTouchpadTouched(NetworkedControllerClickedEventArgs e)
{
    if (TouchpadTouched != null)
        TouchpadTouched(this, e);
}

public virtual void OnTouchpadUntouched(NetworkedControllerClickedEventArgs e)
{
    if (TouchpadUntouched != null)
        TouchpadUntouched(this, e);
}

public virtual void OnTouchpadAxisChanged(NetworkedControllerClickedEventArgs e)
{
    if (TouchpadAxisChanged != null)
        TouchpadAxisChanged(this, e);
}
*/

    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftUpwardGesture(object sender, GestureEventArgs e)
    {
        OnUpwardGesture(SetGestureEvent(e), handedness.left);
    }

    void EventRightUpwardGesture(object sender, GestureEventArgs e)
    {
        OnUpwardGesture(SetGestureEvent(e), handedness.right);
    }

    public virtual void OnUpwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftUpwardGesture != null)
                {
                    LeftUpwardGesture(this, e);
                    //CmdOnUpwardGesture(e, h);
                }
            }
            else
            {
                if (RightUpwardGesture != null)
                {
                    RightUpwardGesture(this, e);
                    //CmdOnUpwardGesture(e, h);
                }
            }
        }
    }

    [Command]
    public virtual void CmdOnUpwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftUpwardGesture(this, e);
            RpcOnUpwardGesture(e, h);
        }
        else
        {
            RightUpwardGesture(this, e);
            RpcOnUpwardGesture(e, h);
        }
    }

    [ClientRpc]
    public virtual void RpcOnUpwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftUpwardGesture(this, e);
            }
            else
            {
                RightUpwardGesture(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftDownwardGesture(object sender, GestureEventArgs e)
    {
        OnDownwardGesture(SetGestureEvent(e), handedness.left);
    }

    void EventRightDownwardGesture(object sender, GestureEventArgs e)
    {
        OnDownwardGesture(SetGestureEvent(e), handedness.right);
    }

    public virtual void OnDownwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftDownwardGesture != null)
                {
                    LeftDownwardGesture(this, e);
                    //CmdOnDownwardGesture(e, h);
                }
            }
            else
            {
                if (RightDownwardGesture != null)
                {
                    RightDownwardGesture(this, e);
                    //CmdOnDownwardGesture(e, h);
                }
            }
        }
    }

    [Command]
    public virtual void CmdOnDownwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftDownwardGesture(this, e);
            RpcOnDownwardGesture(e, h);
        }
        else
        {
            RightDownwardGesture(this, e);
            RpcOnDownwardGesture(e, h);
        }
    }

    [ClientRpc]
    public virtual void RpcOnDownwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftDownwardGesture(this, e);
            }
            else
            {
                RightDownwardGesture(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftOutwardGesture(object sender, GestureEventArgs e)
    {
        OnOutwardGesture(SetGestureEvent(e), handedness.left);
    }

    void EventRightOutwardGesture(object sender, GestureEventArgs e)
    {
        OnOutwardGesture(SetGestureEvent(e), handedness.right);
    }

    public virtual void OnOutwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftOutwardGesture != null)
                {
                    LeftOutwardGesture(this, e);
                    //CmdOnOutwardGesture(e, h);
                }
            }
            else
            {
                if (RightOutwardGesture != null)
                {
                    RightOutwardGesture(this, e);
                    //CmdOnOutwardGesture(e, h);
                }
            }
        }
    }

    [Command]
    public virtual void CmdOnOutwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftOutwardGesture(this, e);
            RpcOnOutwardGesture(e, h);
        }
        else
        {
            RightOutwardGesture(this, e);
            RpcOnOutwardGesture(e, h);
        }
    }

    [ClientRpc]
    public virtual void RpcOnOutwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftOutwardGesture(this, e);
            }
            else
            {
                RightOutwardGesture(this, e);
            }
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftInwardGesture(object sender, GestureEventArgs e)
    {
        OnInwardGesture(SetGestureEvent(e), handedness.left);
    }

    void EventRightInwardGesture(object sender, GestureEventArgs e)
    {
        OnInwardGesture(SetGestureEvent(e), handedness.right);
    }

    public virtual void OnInwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftInwardGesture != null)
                {
                    LeftInwardGesture(this, e);
                    //CmdOnInwardGesture(e, h);
                }
            }
            else
            {
                if (RightInwardGesture != null)
                {
                    RightInwardGesture(this, e);
                    //CmdOnInwardGesture(e, h);
                }
            }
        }
    }

    [Command]
    public virtual void CmdOnInwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftInwardGesture(this, e);
            RpcOnInwardGesture(e, h);
        }
        else
        {
            RightInwardGesture(this, e);
            RpcOnInwardGesture(e, h);
        }
    }

    [ClientRpc]
    public virtual void RpcOnInwardGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftInwardGesture(this, e);
            }
            else
            {
                RightInwardGesture(this, e);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////

    void EventLeftTwistGesture(object sender, GestureEventArgs e)
    {
        OnTwistGesture(SetGestureEvent(e), handedness.left);
    }

    void EventRightTwistGesture(object sender, GestureEventArgs e)
    {
        OnTwistGesture(SetGestureEvent(e), handedness.right);
    }

    public virtual void OnTwistGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (isLocalPlayer)
        {
            if (h == handedness.left)
            {
                if (LeftTwistGesture != null)
                {
                    LeftTwistGesture(this, e);
                    //CmdOnTwistGesture(e, h);
                }
            }
            else
            {
                if (RightTwistGesture != null)
                {
                    RightTwistGesture(this, e);
                    //CmdOnTwistGesture(e, h);
                }
            }
        }
    }

    [Command]
    public virtual void CmdOnTwistGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (h == handedness.left)
        {
            LeftTwistGesture(this, e);
            RpcOnTwistGesture(e, h);
        }
        else
        {
            RightTwistGesture(this, e);
            RpcOnTwistGesture(e, h);
        }
    }

    [ClientRpc]
    public virtual void RpcOnTwistGesture(NetworkedGestureEventArgs e, handedness h)
    {
        if (!isLocalPlayer)
        {
            if (h == handedness.left)
            {
                LeftTwistGesture(this, e);
            }
            else
            {
                RightTwistGesture(this, e);
            }
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////

    void AddSpellListeners()
    {
        leftEvents = leftHand.GetComponent<SteamVR_ControllerEvents>();
        rightEvents = rightHand.GetComponent<SteamVR_ControllerEvents>();
        leftGestures = leftHand.GetComponent<GestureEvents>();
        rightGestures = rightHand.GetComponent<GestureEvents>();

        leftEvents.TriggerClicked += new ControllerClickedEventHandler(EventLeftTriggerClicked);
        leftEvents.TriggerUnclicked += new ControllerClickedEventHandler(EventLeftTriggerUnclicked);
        leftEvents.GripClicked += new ControllerClickedEventHandler(EventLeftGripClicked);
        leftEvents.GripUnclicked += new ControllerClickedEventHandler(EventLeftGripUnclicked);
        leftEvents.TriggerAxisChanged += new ControllerClickedEventHandler(EventLeftTriggerAxisChanged);

        rightEvents.TriggerClicked += new ControllerClickedEventHandler(EventRightTriggerClicked);
        rightEvents.TriggerUnclicked += new ControllerClickedEventHandler(EventRightTriggerUnclicked);
        rightEvents.GripClicked += new ControllerClickedEventHandler(EventRightGripClicked);
        rightEvents.GripUnclicked += new ControllerClickedEventHandler(EventRightGripUnclicked);
        rightEvents.TriggerAxisChanged += new ControllerClickedEventHandler(EventRightTriggerAxisChanged);

        leftGestures.UpwardGesture += new GestureEventHandler(EventLeftUpwardGesture);
        leftGestures.DownwardGesture += new GestureEventHandler(EventLeftDownwardGesture);
        leftGestures.OutwardGesture += new GestureEventHandler(EventLeftOutwardGesture);
        leftGestures.InwardGesture += new GestureEventHandler(EventLeftInwardGesture);
        leftGestures.TwistGesture += new GestureEventHandler(EventLeftTwistGesture);


        rightGestures.UpwardGesture += new GestureEventHandler(EventRightUpwardGesture);
        rightGestures.DownwardGesture += new GestureEventHandler(EventRightDownwardGesture);
        rightGestures.OutwardGesture += new GestureEventHandler(EventRightOutwardGesture);
        rightGestures.InwardGesture += new GestureEventHandler(EventRightInwardGesture);
        rightGestures.TwistGesture += new GestureEventHandler(EventRightTwistGesture);

    }

    void removeSpellListeners()
    {
        leftEvents.TriggerClicked -= new ControllerClickedEventHandler(EventLeftTriggerClicked);
        leftEvents.TriggerUnclicked -= new ControllerClickedEventHandler(EventLeftTriggerUnclicked);
        leftEvents.GripClicked -= new ControllerClickedEventHandler(EventLeftGripClicked);
        leftEvents.GripUnclicked -= new ControllerClickedEventHandler(EventLeftGripUnclicked);
        leftEvents.TriggerAxisChanged -= new ControllerClickedEventHandler(EventLeftTriggerAxisChanged);

        rightEvents.TriggerClicked -= new ControllerClickedEventHandler(EventRightTriggerClicked);
        rightEvents.TriggerUnclicked -= new ControllerClickedEventHandler(EventRightTriggerUnclicked);
        rightEvents.GripClicked -= new ControllerClickedEventHandler(EventRightGripClicked);
        rightEvents.GripUnclicked -= new ControllerClickedEventHandler(EventRightGripUnclicked);
        rightEvents.TriggerAxisChanged -= new ControllerClickedEventHandler(EventRightTriggerAxisChanged);

        leftGestures.UpwardGesture -= new GestureEventHandler(EventLeftUpwardGesture);
        leftGestures.DownwardGesture -= new GestureEventHandler(EventLeftDownwardGesture);
        leftGestures.OutwardGesture -= new GestureEventHandler(EventLeftOutwardGesture);
        leftGestures.InwardGesture -= new GestureEventHandler(EventLeftInwardGesture);
        leftGestures.TwistGesture -= new GestureEventHandler(EventLeftTwistGesture);


        rightGestures.UpwardGesture -= new GestureEventHandler(EventRightUpwardGesture);
        rightGestures.DownwardGesture -= new GestureEventHandler(EventRightDownwardGesture);
        rightGestures.OutwardGesture -= new GestureEventHandler(EventRightOutwardGesture);
        rightGestures.InwardGesture -= new GestureEventHandler(EventRightInwardGesture);
        rightGestures.TwistGesture -= new GestureEventHandler(EventRightTwistGesture);
    }
}