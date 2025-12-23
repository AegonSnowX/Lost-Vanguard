using UnityEngine;

public class ElectricalInteractable : MonoBehaviour,IInteractable
{

    public void Focus()
    {
      //  throw new System.NotImplementedException();
    }

    public void Interact(PlayerStateController player)
    {
        if (ControlRoomManager.Instance.IsMainPowerOn)
        {
            Debug.Log("Main power is already on.");
            return;
        }else{
       ControlRoomManager.Instance.RestoreMainPower();}

    }

    public void Unfocus()
    {
        //throw new System.NotImplementedException();
    }

    
}
