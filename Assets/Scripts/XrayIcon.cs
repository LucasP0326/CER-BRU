using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XrayIcon : MonoBehaviour
{
    public Sprite xray_ready;
    public Sprite xray_cooldown;
    public void Ready()
    {
        GetComponent<Image>().sprite = xray_ready;
    }

    public void CoolDown()
    {
        GetComponent<Image>().sprite = xray_cooldown;
    }
}
