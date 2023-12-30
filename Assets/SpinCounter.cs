using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpinCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textField;
    // Start is called before the first frame update
    private int counter;
    
    public void Count()
    {
        counter++;
        textField.text = "" + counter;
    }
}
