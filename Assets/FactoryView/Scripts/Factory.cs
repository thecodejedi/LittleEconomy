using System.Collections.Generic;

using UnityEngine;

public class Factory : MonoBehaviour {
    #region Public Properties

    public IEnumerable<string> AllEntries {
        get {
            for (int i = 0; i < transform.childCount; i++) {
                yield return transform.GetChild(i).name;
            }
        }
    }

    #endregion

    #region Public Methods and Operators

    public GameObject Create(string childName) {
        return Instantiate(transform.FindChild(childName).gameObject);
    }

    #endregion
}