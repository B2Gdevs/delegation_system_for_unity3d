using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 /// <summary>
 /// DelEventManager is the event manager specifically for the delegation 
 /// system. 
 /// When creating a full application use a different event manager for global 
 /// global events. Even if DelegationActors are subscribed to those events.
 /// </summary>
public class DelEventManager : MonoBehaviour
{
    // Static class needs no instances since this will just be references to 
    // events.
    public delegate void autoAssignMethod();
    public static event autoAssignMethod AutoAssignEvent;

    /// <summary>
    /// The method to reference in the master controller to initiate an event.
    /// </summary>
    public static void autoAssign(){
        AutoAssignEvent();
    }

}
