using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTap : MonoBehaviour
{
    public States state;
    
    private void OnMouseDown()
    {
        
        GameManager.Insantance.paper.ExecuteCommands(state);
        transform.parent.gameObject.SetActive(false);

    }

}
