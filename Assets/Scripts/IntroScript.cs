using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    Transform cameraTransform = null;

    public GameObject OVRCamera;
    public GameObject ViewerCamera;

    public GameObject About;
    public GameObject MainMenu;

    void Awake()
    {
        Screen.fullScreen = false;
        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
            cameraTransform = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start()
    {
        //Screen.SetResolution(2560, 1600, true);
        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            OVRCamera.gameObject.SetActive(true);
            ViewerCamera.gameObject.SetActive(false);
        }
        else
        {
            OVRCamera.gameObject.SetActive(false);
            ViewerCamera.gameObject.SetActive(true);
        }
    }

    public void LoadGameScene()
    {
        string model = UnityEngine.XR.XRDevice.model != null ? UnityEngine.XR.XRDevice.model : "";
        if (model.IndexOf("Rift") >= 0)
        {
            RaycastHit hit;
            Vector3 rayDirection = cameraTransform.TransformDirection(Vector3.forward);
            Vector3 rayStart = cameraTransform.position + rayDirection;

            if (Physics.Raycast(rayStart, rayDirection, out hit))
            {

                Debug.Log("object that was hit: " + hit.collider.name);
                if (hit.collider.name == "StrCollider")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (hit.collider.name == "AbtCollider")
                {
                    About.SetActive(true);
                    MainMenu.SetActive(false);
                }
                if (hit.collider.name == "BackCollider")
                {
                    About.SetActive(false);
                    MainMenu.SetActive(true);
                }
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "StrCollider")
                {
                    Debug.Log("Clicked");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if (hit.collider.name == "AbtCollider")
                {
                    About.SetActive(true);
                    MainMenu.SetActive(false);
                }
                if (hit.collider.name == "BackCollider")
                {
                    About.SetActive(false);
                    MainMenu.SetActive(true);
                }
            }
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
        {
            LoadGameScene();
        }
    }
}
