using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSwordGrab : OVRGrabbable
{
    public bool DualWield;
    public bool firstGrab;
     public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        
        m_grabbedBy = hand;
        m_grabbedCollider = grabPoint;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if(DualWield)
        {
            gameObject.GetComponent<IceSword>().isHeld = true;
        }
        if(firstGrab)
        {
            gameObject.GetComponent<IceSword>().isHeld = true;
            firstGrab = false;
        }
       
    }
     public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
         rb.isKinematic = m_grabbedKinematic;
         rb.velocity = linearVelocity * 1.5f;
         rb.angularVelocity = angularVelocity * 1.5f;
         m_grabbedBy = null;
         m_grabbedCollider = null;

        gameObject.GetComponent<IceSword>().isHeld = false;


    }
}
