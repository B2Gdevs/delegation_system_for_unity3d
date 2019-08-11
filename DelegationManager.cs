using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The MasterController is the engine of the delegation system. It assigns
/// the actions, locations, and selects the DelegationActor.
/// </summary>
public class DelegationManager : MonoBehaviour
{
    // only one DelegationManager at a time.
    private static DelegationManager _instance;
    public static DelegationManager Instance { get { return _instance; } }
    ActorManager actorManager;
    public int assigneeId = -1;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void start(){
        actorManager = GameObject.FindObjectOfType<ActorManager>();
    }

    // should be subscribed to an event.  Like a button!
    public Selection(GameObject target){
        switch(target.tag){

            case "untagged":
                    if(assigneeId == -1){
                        Debug.Log("Character select menu!");
                        // this.newAssignee = characterSelectMenuWhenAvailable();
                    }
                    else if(!actorManager.actorMap[assigneeId].isAssignedLocation()){
                        Debug.Log("Location select menu!");
                        // this.newAssignee.setLocation(locationSelectMenuWhenAvailable());
                    }
                    else if(!actorManager.actorMap[assigneeId].isAssignedAction()){
                        Debug.Log("Action select menu!");
                        // this.newAssignee.setLocation(actionSelectMenuWhenAvailable());
                    }
                    break;
            case "actor":
                    Debug.log("Assigned Character Manually");
                    assigneeId = target.GetComponent<DelegationActor>().uid;
                    break;
            case "location":
                    if(assigneeId == -1){
                        assigneeId = actorManager.getIdleActor().uid;
                    }
                    actorManager.actorMap[assigneeId].setLocation(target);
                    break;
            case "action":
                    if(assigneeId == -1){
                        assigneeId = actorManager.getIdleActor().uid;
                    }
                    actorManager.actorMap[assigneeId].setAction(target);
        }

        if(actorManager.actorMap[assigneeId].isReady() == 2){
            Debug.log("All assignments have been set.  Begin action!");
            actorManager.actorMap[assigneeId].beginAction();
        }

    }
}
