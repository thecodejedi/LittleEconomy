using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class SelectionProvider : MonoBehaviour {
    List<SelectionOption> options = new List<SelectionOption>();

	public void AddSelection(SelectionOption option) {
        options.Add(option);
    }

    public void OpenSelection() {
        options.ForEach(CreateMenuEntry);
    }

    private void CreateMenuEntry(SelectionOption option) {
        Transform panel = transform.FindChild("Canvas").FindChild("Panel");
        Transform button = panel.GetChild(panel.childCount - 1);
        Transform duplicateButton = (Transform)Instantiate(button, panel, false);
        duplicateButton.gameObject.SetActive(true);
        duplicateButton.GetComponentInChildren<Text>().text = option.Text;

        RectTransform rectTransform = duplicateButton.GetComponent<RectTransform>();
        Vector3 position = duplicateButton.localPosition;
        position.y -= rectTransform.rect.height;
        duplicateButton.localPosition = position;

        duplicateButton.GetComponent<Button>().onClick.AddListener(option.Selected);
    }
}
