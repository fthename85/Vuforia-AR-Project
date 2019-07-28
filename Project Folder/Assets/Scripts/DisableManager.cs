using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToDisable;
    public void DisableChildOfLogo()
    {
        for(int i = 0; i < gameObjectsToDisable.Length; i++)
        {
            gameObjectsToDisable[i].SetActive(false);
        }
        if(CustomBtnFunction.tower4 != null)
            CustomBtnFunction.tower4.SetActive(false);
    }
}
