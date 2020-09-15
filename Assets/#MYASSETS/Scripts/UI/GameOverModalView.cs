using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverModalView : MonoBehaviour
{
    private List<Transform> modalChildrenList = new List<Transform>();

    private void Start()
    {
        foreach(Transform child in gameObject.transform){
            modalChildrenList.Add(child);
        }
    }

    public void ShowModal()
    {
        foreach (var modalChildren in modalChildrenList)
        {
            modalChildren.gameObject.SetActive(true);
        }
    }

    public void CloseModal()
    {
        foreach (var modalChildren in modalChildrenList)
        {
            modalChildren.gameObject.SetActive(false);
        }
    }
}
