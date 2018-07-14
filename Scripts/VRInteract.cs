using UnityEngine;
using UnityEngine.UI;

public class VRInteract : MonoBehaviour
{
    public float range = 10f;
    public float fillSpeed = 2f;

    private RaycastHit theHit;
    private Vector3 theDirection;
    private Transform mainCamera;
    private Transform theReticle;
    private bool buttonSelected = false;
    private Image currentButton;
    private string currentButtonName = "";

    private PlanetsInformation informationScript;


    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        theReticle = mainCamera.transform.Find("Reticle");

        informationScript = gameObject.GetComponent<PlanetsInformation>();
    }

    private void FixedUpdate()
    {
        Laser();
    }

    private void Laser()
    {
        if (Physics.Raycast(theReticle.position, mainCamera.transform.forward, out theHit, range))
        {

            if (theHit.collider.CompareTag("Button"))
            {
                currentButtonName = theHit.collider.gameObject.name;
                currentButton = theHit.collider.gameObject.GetComponentInChildren<Image>();
                fillTheButton(currentButton);
                print("Chose " + currentButtonName);
            }
        }
        else
            ResetButtonFill();
    }

    private void ResetButtonFill()
    {
        if(currentButton && currentButton.fillAmount > 0)
            currentButton.fillAmount = 0;

        buttonSelected = false;
    }

    public void fillTheButton(Image anyButton)
    {
        anyButton.fillAmount += fillSpeed * Time.deltaTime;
        if (anyButton.fillAmount == 1 && !buttonSelected)
        {
          buttonSelected = true;
          ButtonAction(currentButtonName);
        }
    }

    public void ButtonAction(string buttonName)
    {
      switch(buttonName)
      {

        case "NextButton": informationScript.nextPlanet();
        break;

        case "StartButton": informationScript.StartGame();
        break;

        case "SoundButton": informationScript.SetAudio();
        break;

        case "FactsButton": informationScript.switchStatsToFacts();
        break;

        case "UnitButton": informationScript.switchUnit();
        break;

        case "MenuButton": informationScript.ToMenu();
        break;

        case "QuitButton": informationScript.EndGame();
        break;

        default:  Debug.LogError("Something is wrong!");
        break;
      }
    }
}
