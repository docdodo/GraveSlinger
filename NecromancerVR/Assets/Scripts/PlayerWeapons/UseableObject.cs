using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class UseableObject : MonoBehaviour
{
    //put variables each object will have here
    Mesh mesh;


    // Start is called before the first frame update
    

    // Update is called once per frame
   
    public virtual void Use()
    {
        //changes depending on the object
    }
    public virtual void AltUse()
    {
        //changes depending on the object
    }
}
