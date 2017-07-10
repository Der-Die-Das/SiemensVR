using UnityEngine;

/// <summary>
/// Thje Script is attached to a Controller
/// Script to grab Objects with a Rigidbody
/// </summary>
public class ControllerGrabObject : MonoBehaviour
{
    /// <summary>
    /// Event which gets fires when we relrease a object.
    /// Gameobject is the object we release.
    /// </summary>
    public System.Action<GameObject> ObjectReleased;

    private SteamVR_TrackedObject trackedObj;

    /// <summary>
    /// When our controller collide with a object we save it here
    /// </summary>
    private GameObject collidingObject;
    /// <summary>
    /// When we grab a object we save it here
    /// </summary>
    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;
        objectInHand.transform.position = Vector3.Lerp(transform.position,objectInHand.transform.position, 0.8f);
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        if (ObjectReleased != null)
        {
            ObjectReleased.Invoke(objectInHand);
        }
        objectInHand = null;
    }
}
