using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegationActor : MonoBehaviour
{
    DelegationManager delMnger;
    ActorManager actorMnger;
    public string actorName;
    [HideInInspector]
    public int uid; // uid = Unique Identifier
    public GameObject assignedLocation;
    public GameObject assignedAction;

    public static delegate void becameIdle(DelegationActor actor);
    public static event becameIdle BecameIdle;
    public static delegate void beganWorking(DelegationActor actor);
    public static event beganWorking BeganWorking;

    void Awake(){
        BecameIdle += actorMnger.registerIdleActor;
        BeganWorking += actorMnger.actorWorks;
    }

    void Start()
    {
        delMnger = GameObject.FindObjectOfType<DelegationManager>();
        actorMnger = GameObject.FindObjectOfType<ActorManager>();

        delMnger.Quit += stopDelegation;
    }

    void update(){
        if(this.isIdle){
            actorMnger.registerIdleActor(this);
        }
    }

    public void setAction(Action action){
        this.assignedAction = action;
    }

    public void setLocation(Location loc){
        this.assignedLocation = loc;
    }

    // subscribed method
    void stopDelegation(){
        assignedAction = null;
        assignedLocation = null;
    }


    public void beginAction(){

        // perform the action.  Implmentation of work should be in the action script!

        // notify the manager that you are working
        BeganWorking(this);
    }

    void actionCompleted(){
        // stop doing stuff
        this.assignedAction = null;
        this.assignedLocation = null;

        // notify the manager that you are available.
        BecameIdle(this);
    }

    void setAction(DelgationAction action){
        this.assignedAction = action;
    }

    void setLocation(DelegationLocation loc){
        this.assignedLocation = loc;
    }

    bool isAssignedLocation(){
        if(this.assignedLocation is null){
            return false;
        }

        return true;
    }

    bool isAssignedAction(){
        if(this.assignedAction is null){
            return false;
        }

        return true;
    }

    /// <summary>
    /// Creates a range of values from 0-2. 2 being completely ready and 1 not being ready at
    /// all. This has the purpose of notifying the user that someone has been assigned stuff
    /// but is not done.
    /// </summary>
    /// <returns></returns>
    int isReady(){
        int ready = 0;
        ready += isAssignedAction();
        ready += isAssignedLocation();
        return ready;
    }
    DelegationAction getAction(){
        return this.assignedAction;
    }
    DelegationLocation getLocation(){
        return this.assignedLocation;
    }
}