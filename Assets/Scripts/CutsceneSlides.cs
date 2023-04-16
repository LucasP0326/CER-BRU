using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneSlides : MonoBehaviour
{
    public GameObject cutsceneManager;
    public RawImage cutscene;
    public Texture introImage0;
    public Texture introImage1;
    public Texture introImage2;
    public Texture introImage3;
    public Texture introImage4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cutscene = GetComponent<RawImage>();
        if (cutsceneManager.GetComponent<CutsceneManager>().slideNumber == 0)
            cutscene.texture = introImage0;
        if (cutsceneManager.GetComponent<CutsceneManager>().slideNumber == 1)
            cutscene.texture = introImage1;
        if (cutsceneManager.GetComponent<CutsceneManager>().slideNumber == 2)
            cutscene.texture = introImage2;
        if (cutsceneManager.GetComponent<CutsceneManager>().slideNumber == 3)
            cutscene.texture = introImage3;
        if (cutsceneManager.GetComponent<CutsceneManager>().slideNumber == 4)
            cutscene.texture = introImage4;
    }
}
