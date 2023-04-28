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
    public bool goneRogue;

    // Start is called before the first frame update
    void Start()
    {
        collectedInvisibility = InvisibilityTerminal.collectedInvisibility;
        collectedXray = XRayTerminal.collectedXRay;
        receptionWon = WinReception.receptionWon;
        adminWon = WinAdministration.adminWon;
        labsWon = WinLaboratories.laboratoriesWon;
        informationDeleted += Player.informationDeleted;
        informationDeleted += Player.informationDeleted;
        goneRogue = OfficeEventManager.goneRogue;
    }

    // Update is called once per frame
    void Update()
    {
        collectedInvisibility = InvisibilityTerminal.collectedInvisibility;
        collectedXray = XRayTerminal.collectedXRay;
        receptionWon = WinReception.receptionWon;
        adminWon = WinAdministration.adminWon;
        labsWon = WinLaboratories.laboratoriesWon;
        goneRogue = OfficeEventManager.goneRogue;
    }
}
