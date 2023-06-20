using UnityEngine;
using UnityEngine.UIElements;

public class BuildToolbar : MonoBehaviour
{
    [SerializeField] private World theWorld;
    [SerializeField] private GameObject houseGridObject;

    // TODO: Switch to a Toggle/ToggleButton
    private Button _houseButton;
    private bool houseModeOn = false;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        _houseButton = uiDocument.rootVisualElement.Q("houseBtn") as Button;

        _houseButton.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void Awake()
    {
        if (theWorld != null)
        {
            theWorld.GridObjectClicked += OnGridObjectClicked;
        }
    }

    private void OnGridObjectClicked(Vector3Int coord, GameObject gobj)
    {
        if (houseModeOn)
        {
            // Change to a House, no way to know if there's already a house there.
            theWorld.ChangeObjectAtCoord(coord, houseGridObject);
        }
    }

    private void PrintClickMessage(ClickEvent evt)
    {
        houseModeOn = !houseModeOn;
        Debug.Log($"House mode selection is [{houseModeOn}]");
    }
}
