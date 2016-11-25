using System.Linq;

using UnityEngine;

public class TileConstructorBehavior : MonoBehaviour, IMouseEventReceiver {
    #region Fields

    public Factory ContentFactory;

    public GameObject EmptyPrefab;

    public SelectionProvider MenuPrefab;

    private GameObject empty;

    private SelectionProvider menu;

    #endregion

    #region Public Properties

    public GameObject ActiveComponent { get; private set; }

    public GameObject HitPane {
        get {
            return gameObject.transform.FindChild("HitPane").gameObject;
        }
    }

    #endregion

    #region Properties

    private GameObject Empty {
        get {
            return empty ?? (empty = CreateEmpty());
        }
    }

    private SelectionProvider Menu {
        get {
            return menu ?? (menu = CreateMenu());
        }
    }

    #endregion

    #region Public Methods and Operators

    public void MouseDown() {
        print("OpenMenu");
        Menu.OpenSelection();
        SwitchToMenuOpenState();
    }

    public void MouseEnter() {
    }

    public void MouseExit() {
    }

    public void MouseOver() {
    }

    public void Select(string componentName) {
        print("building " + componentName + "...");

        BuildComponent(componentName);
        SwitchToBuiltState();
    }

    #endregion

    #region Methods

    private void BuildComponent(string text) {
        ActiveComponent = ContentFactory.Create(text, transform);
        CenterComponent(ActiveComponent);
    }

    private void CenterComponent(GameObject component) {
        component.transform.localPosition = component.transform.localPosition
                                            + gameObject.transform.FindChild("Center").transform.localPosition;
    }

    private GameObject CreateEmpty() {
        GameObject obj = (GameObject)Instantiate(EmptyPrefab, transform, false);
        CenterComponent(obj);
        return obj;
    }

    private SelectionProvider CreateMenu() {
        SelectionProvider selectionMenu = (SelectionProvider)Instantiate(MenuPrefab, transform, false);
        ContentFactory.AllEntries.Select(item => new SelectionOption(item, this))
                      .ToList()
                      .ForEach(item => selectionMenu.AddSelection(item));
        return selectionMenu;
    }

    // Use this for initialization
    private void Start() {
        SwitchToEmptyState();
    }

    private void SwitchToBuiltState() {
        UpdateState(false, false, true, false);
    }

    private void SwitchToEmptyState() {
        UpdateState(true, true, false, false);
    }

    private void SwitchToMenuOpenState() {
        UpdateState(false, false, false, true);
    }

    private void UpdateState(bool emptyState, bool hitPaneState, bool activeComponentState, bool menuState) {
        if (Empty != null) {
            Empty.SetActive(emptyState);
        }
        if (HitPane != null) {
            HitPane.SetActive(hitPaneState);
        }
        if (ActiveComponent != null) {
            ActiveComponent.SetActive(activeComponentState);
        }
        if (Menu != null) {
            Menu.gameObject.SetActive(menuState);
        }
    }

    #endregion
}