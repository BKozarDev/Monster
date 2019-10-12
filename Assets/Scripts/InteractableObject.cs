using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public abstract void OnGrabAction();
    public abstract void UnGrabAction();
    //public abstract void InteractAction();
}
