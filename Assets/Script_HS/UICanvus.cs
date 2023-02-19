using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UICanvus : MonoBehaviour
{
    public GameObject okBtn;

    public void HideOKBtn() {
        okBtn.SetActive(false);
    }
    public void ShowOKBtn()
    {
        okBtn.SetActive(true);
    }
}
