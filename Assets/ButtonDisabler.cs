using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    [SerializeField]
    Button button;

    public float timer=1f;

    public void disableButton1Second()
    {
        button.interactable = false;
        StartCoroutine(enableButton());

    }


    IEnumerator enableButton()
    {
        yield return new WaitForSeconds(timer);
        button.interactable = true;
    }
}
