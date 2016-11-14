using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class TileConstructorBehavior : MonoBehaviour, IMouseEventReceiver {

    // Use this for initialization
    private void Start()
    {
        rend = GetComponentsInChildren<Renderer>();

        SetupMenu();

        empty = Instantiate(EmptyPrefab);
        empty.transform.parent = transform;

        empty.transform.localPosition = empty.transform.position + gameObject.transform.FindChild("Center").transform.localPosition;
        empty.SetActive(true);
    }

    private void SetupMenu() {
        menu = Instantiate(MenuPrefab);
        menu.transform.parent = transform;
        ContentFactory.AllEntries.Select(item => new SelectionOption(item, this))
                      .ToList()
                      .ForEach(item => menu.AddSelection(item));
        menu.gameObject.SetActive(false);
    }

    public void MouseEnter() {
    }

    public void MouseExit() {
    }

    public void MouseOver() {
    }

    public void MouseDown() {
        print("clicked");
        menu.OpenSelection();
    }

    public GameObject EmptyPrefab;

    public Factory ContentFactory;

    public SelectionProvider MenuPrefab;

    private Renderer[] rend;
    private SelectionProvider menu;
    private GameObject empty;

    private void Build(string factoryName)
    {
        print("building Factory");

        GameObject component = ContentFactory.Create(factoryName);

        component.transform.parent = transform;
        component.transform.localPosition = component.transform.position + gameObject.transform.FindChild("Center").transform.localPosition;
        empty.SetActive(false);
        gameObject.transform.FindChild("HitPane").gameObject.SetActive(false);
    }

    public void Select(string text) {
        print(text);
    }
}
