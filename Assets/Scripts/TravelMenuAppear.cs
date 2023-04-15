using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelMenuAppear : MonoBehaviour
{
    public GameObject levelTerminal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (levelTerminal.GetComponent<SecurityOfficeTerminal>().levelSelected == 1)
            {
                SceneManager.LoadScene("Reception");
            }
            if (levelTerminal.GetComponent<SecurityOfficeTerminal>().levelSelected == 2)
            {
                SceneManager.LoadScene("Administration");
            }
            if (levelTerminal.GetComponent<SecurityOfficeTerminal>().levelSelected == 3)
            {
                SceneManager.LoadScene("Laboratory");
            }
        }
    }

}
