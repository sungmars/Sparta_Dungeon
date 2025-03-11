using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private InputAction interactAction;

    void Start()
    {
        camera = Camera.main;

        // InputAction 초기화
        interactAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
        interactAction.performed += ctx => OnInteract();
        interactAction.Enable();
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                Debug.Log($"[Interactable] 감지된 오브젝트: {hit.collider.gameObject.name}");

                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();

                    if (curInteractable != null)
                    {
                        Debug.Log($"[Interactable] {curInteractGameObject.name}에서 IInteractable 감지됨!");
                    }

                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        if (curInteractable != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }


    public void OnInteract()
    {
        if (curInteractable != null)
        {
            Debug.Log($"[Interactable] {curInteractGameObject.name}과 상호작용 시도!");

            if (curInteractGameObject != null)
            {
                Item itemComponent = curInteractGameObject.GetComponent<Item>();
                if (itemComponent != null)
                {
                    GetItem getItem = curInteractGameObject.GetComponent<GetItem>();
                    if (getItem != null)
                    {
                        getItem.SetItemData(itemComponent.data);
                    }
                }
            }
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
