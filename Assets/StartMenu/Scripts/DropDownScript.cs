using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownScript : MonoBehaviour
{

    private TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        Debug.Log(GlobalControl.instance.difficulties);
        foreach (Difficulty d in GlobalControl.instance.difficulties) {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = d.name });
        }
    }
}
