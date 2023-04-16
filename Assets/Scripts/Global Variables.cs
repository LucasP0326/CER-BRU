using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public bool receptionWon;
    public bool adminWon;
    public bool labsWon;
    public bool collectedInvisibility;
    public bool collectedXray;
    public float informationDeleted;
    public float informationSaved;

    // Start is called before the first frame update
    void Start()
    {
        collectedInvisibility = InvisibilityTerminal.collectedInvisibility;
        receptionWon = WinReception.receptionWon;
        adminWon = WinAdministration.adminWon;
        informationDeleted += Player.informationDeleted;
        informationDeleted += Player.informationDeleted;
    }

    // Update is called once per frame
    void Update()
    {
        collectedInvisibility = InvisibilityTerminal.collectedInvisibility;
        receptionWon = WinReception.receptionWon;
        adminWon = WinAdministration.adminWon;
    }
}