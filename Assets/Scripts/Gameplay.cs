using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private Color highlightColor = Color.white;
    
    // GUI for VR mode
    public GameObject PanelMistakeFound;
    public GameObject PanelFoundAll;
    public GameObject PanelSwitchPlayer;
    public TextMesh ScoreText;
    public TextMesh MistakeFound;
    public Text MistakeInfo;

    // GUI for Desktop mode
    public GameObject PanelMistakeFoundDT;
    public GameObject PanelFoundAllDT;
    public GameObject PanelSwitchPlayerDT;
    public Text ScoreOutsider;
    public Text MistakeFoundOutsider;
    public Text MistakeInfoDT;

    public GameObject FPSController;
    public GameObject OVRPlayerController;
    Transform cameraTransform = null;

    int score = 0;
    int counter = 0;
    string minfo;

    void Awake()
    {
        Screen.fullScreen = false;
        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";

        if (model.IndexOf("Rift") >= 0)
        {
            cameraTransform = GameObject.FindWithTag("MainCamera").transform; // for Raycast
        }
        else
        {
            cameraTransform = GameObject.FindWithTag("MainCameraDT").transform; // for Raycast
        }

        PanelMistakeFound = GameObject.Find("PanelMistakeFound");
        PanelFoundAll = GameObject.Find("PanelFoundAll");
        PanelSwitchPlayer = GameObject.Find("PanelSwitchPlayer");

        PanelMistakeFoundDT = GameObject.Find("PanelMistakeFoundDT");
        PanelFoundAllDT = GameObject.Find("PanelFoundAllDT");
        PanelSwitchPlayerDT = GameObject.Find("PanelSwitchPlayerDT");

        ScoreText = GameObject.Find("Score").GetComponent<TextMesh>();
        MistakeFound = GameObject.Find("MistakeFound").GetComponent<TextMesh>();

        ScoreOutsider = GameObject.Find("ScoreOutsider").GetComponent<Text>();
        MistakeFoundOutsider = GameObject.Find("MistakeFoundOutsider").GetComponent<Text>();

        MistakeInfo = GameObject.Find("MistakeInfo").GetComponent<Text>();
        MistakeInfoDT = GameObject.Find("MistakeInfoDT").GetComponent<Text>();
    }

    private void Start()
    {
        //Screen.SetResolution(2560, 1600, true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PanelMistakeFound.gameObject.SetActive(false);
        PanelSwitchPlayer.gameObject.SetActive(false);
        PanelFoundAll.gameObject.SetActive(false);

        PanelMistakeFoundDT.gameObject.SetActive(false);
        PanelSwitchPlayerDT.gameObject.SetActive(false);
        PanelFoundAllDT.gameObject.SetActive(false);


        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";

        if (model.IndexOf("Rift") >= 0)
        {
            Debug.Log("Occulus connected");
            OVRPlayerController.gameObject.SetActive(true);
            FPSController.gameObject.SetActive(false);
            GameObject.Find("/UIOutsider/Crosshair").GetComponent<Image>().gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("None connected");
            FPSController.gameObject.SetActive(true);
            OVRPlayerController.gameObject.SetActive(false);
            GameObject.Find("/UIOutsider/Crosshair").GetComponent<Image>().gameObject.SetActive(true);

        }
    }

    private void ObjectHighlighting()
    {
        RaycastHit hit;
        Vector3 rayDirection = cameraTransform.TransformDirection(Vector3.forward);
        Vector3 rayStart = cameraTransform.position + rayDirection;

        if (Physics.Raycast(rayStart, rayDirection, out hit))
        {
            Debug.Log("object that was hit: " + hit.collider.name);
            if (hit.collider.gameObject.CompareTag("Mistake")
                && hit.collider.GetComponent<Renderer>().material.color != highlightColor
                && (!PanelMistakeFound.activeInHierarchy && !PanelMistakeFoundDT.activeInHierarchy)
                && (!PanelSwitchPlayer.activeInHierarchy && !PanelSwitchPlayerDT.activeInHierarchy)
                )
            {
                switch (hit.collider.name)
                {
                    case "Mistake-1":
                        minfo = "This is a coordination mistake as the handrail should attach itself to the ramp.";
                        score += 15;
                        break;
                    case "Mistake-2":
                        minfo = "This is a spatial layout mistake  as the ground should not be on top of the ramp.";
                        score += 20;
                        break;
                    case "Mistake-3":
                        minfo = "This is a spatial layout as the ground should align with the concrete step.";
                        score += 20;
                        break;
                    case "Mistake-4":
                        minfo = "This is a coordination mistake as the handrail should attach itself to the concrete floor.";
                        score += 15;
                        break;
                    case "Mistake-5":
                        minfo = "This is a spatial layout mistake as the ground should stop at the retaining wall.";
                        score += 20;
                        break;
                    case "Mistake-6":
                        minfo = "This is a coordination mistake as the pole should be attached and aligned to the concrete floor.";
                        score += 15;
                        break;
                    case "Mistake-7":
                        minfo = "This is a missing element mistake as there should be a handrail.";
                        score += 10;
                        break;
                    case "Mistake-9":
                        minfo = "This is a spatial layout and material mistake as the stairs should be up to architectural code and should be made of wood.";
                        score += 20 + 5;
                        break;
                    case "Mistake-10-1":
                    case "Mistake-18":
                        minfo = "This is a coordination mistake as the stair needs to be attached to the wall as they are load bearing.";
                        score += 15;
                        break;
                    case "Mistake-11":
                        minfo = "This is a missing element mistake as there should be a handrail at the edge of the floor.";
                        score += 5;
                        break;
                    case "Mistake-12-1":
                    case "Mistake-12-2":
                        minfo = "This is a spatial layout mistake as there should not be a door that is not operable.";
                        score += 20;
                        break;
                    case "Mistake-13":
                        minfo = "This is a coordination mistake as the stair should be attached to the floor.";
                        score += 15;
                        break;
                    case "Mistake-14":
                        minfo = "This is a spatial layout mistake as the highlighted step should be up to architectural code.";
                        score += 20;
                        break;
                    case "Mistake-15-1":
                    case "Mistake-15-2":
                    case "Mistake-15-3":
                        minfo = "This is a coordination and material mistake as the column should not be inside the room and should be made of concrete.";
                        score += 15 + 5;
                        break;
                    case "Mistake-16-1":
                    case "Mistake-16-2":
                    case "Mistake-16-3":
                    case "Mistake-17-1":
                    case "Mistake-17-2":
                    case "Mistake-17-3":
                        minfo = "This is a spatial layout mistake as the door should be wider.";
                        score += 20;
                        break;
                    case "Mistake-19":
                        minfo = "This is a coordination mistake as the handrail should be attached and aligned to the balcony floor.";
                        score += 15;
                        break;
                    case "Mistake-21-1":
                    case "Mistake-21-2":
                    case "Mistake-21-3":
                        minfo = "This is a spatial layout mistake as there should not be a concrete step and the opening is not wide enough.";
                        score += 20;
                        break;
                    case "Mistake-22":
                        minfo = "This is a coordination mistake as the building elements should align to each other.";
                        score += 15;
                        break;
                    case "Mistake-23":
                        minfo = "This is a coordination mistake as the pole should be attached and aligned with the concrete floor.";
                        score += 15;
                        break;
                    case "Mistake-24":
                        minfo = "This is a spatial layout mistake as there should not be a gap in between the floor and the wall.";
                        score += 20;
                        break;
                    default:
                        minfo = "Unknown mistake";
                        break;
                }

                counter++;
                hit.collider.GetComponent<Renderer>().material.SetColor("_Color", highlightColor);
                
                string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";

                if (model.IndexOf("Rift") >= 0)
                {
                    MistakeInfo.text = minfo;
                    ScoreText.text = "Score: " + score.ToString();
                    ScoreOutsider.text = "Score: " + score.ToString();
                    MistakeFound.text = "Mistakes found: " + counter.ToString();
                    MistakeFoundOutsider.text = "Mistakes found: " + counter.ToString();
                    PanelMistakeFound.gameObject.SetActive(true);
                }
                else
                {
                    MistakeInfoDT.text = minfo;
                    ScoreOutsider.text = "Score: " + score.ToString();
                    MistakeFoundOutsider.text = "Mistakes found: " + counter.ToString();
                    PanelMistakeFoundDT.gameObject.SetActive(true);
                }
            }
            else
            {
                string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";

                if (model.IndexOf("Rift") >= 0)
                {
                    PanelMistakeFound.gameObject.SetActive(false);
                    if (PanelSwitchPlayer.activeInHierarchy)
                        PanelSwitchPlayer.gameObject.SetActive(false);
                    else
                    {
                        if (counter % 5 == 0 && counter != 0)
                            PanelSwitchPlayer.gameObject.SetActive(true);
                    }
                }
                else
                {
                    PanelMistakeFoundDT.gameObject.SetActive(false);
                    if (PanelSwitchPlayerDT.activeInHierarchy)
                        PanelSwitchPlayerDT.gameObject.SetActive(false);
                    else
                    {
                        if (counter % 5 == 0 && counter != 0)
                            PanelSwitchPlayerDT.gameObject.SetActive(true);
                    }
                }
               
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
            ObjectHighlighting();

        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";

        if (model.IndexOf("Rift") >= 0)
        {
            if (counter == 31 && !PanelMistakeFound.activeInHierarchy)
                PanelFoundAll.gameObject.SetActive(true);
        }
        else
        {
            if (counter == 31 && !PanelMistakeFoundDT.activeInHierarchy)
                PanelFoundAllDT.gameObject.SetActive(true);
        }         
    }
}
