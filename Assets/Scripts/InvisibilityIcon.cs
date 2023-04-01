using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvisibilityIcon : MonoBehaviour
{
    public Sprite invisibility_ready;
    public Sprite invisibility_cooldown;
    public void Ready()
    {
        GetComponent<Image>().sprite = invisibility_ready;
    }

    public void CoolDown()
    {
        GetComponent<Image>().sprite = invisibility_cooldown;
    }
}
