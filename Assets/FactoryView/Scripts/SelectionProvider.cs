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
        gameObject.SetActive(true);
    }

    private void CreateMenuEntry(SelectionOption option)
    {
        var panel = transform.FindChild("Canvas").FindChild("Panel");
        var button = panel.GetChild(panel.childCount - 1);
        var duplicateButton = Instantiate(button);
        duplicateButton.transform.parent = panel;
        duplicateButton.gameObject.SetActive(true);
        duplicateButton.GetComponentInChildren<Text>().text = option.Text;

        var rectTransform = duplicateButton.GetComponent<RectTransform>();
        rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + rectTransform.rect.height, rectTransform.position.z);

        duplicateButton.GetComponent<Button>().onClick.AddListener(() => { option.Selected(); });
    }
}
