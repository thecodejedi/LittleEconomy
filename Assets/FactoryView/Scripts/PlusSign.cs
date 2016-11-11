using UnityEngine;
using System.Collections;

public class PlusSign : MonoBehaviour {
    public GameObject Menu;

    public GameObject ConnectionPrefab;

    public GameObject FactoryComponentPrefab;


    private Renderer[] rend;
    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
    }
    void OnMouseEnter()
    {
        foreach (Renderer renderer in rend) {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseOver()
    {
        foreach (Renderer renderer in rend)
        {
            renderer.material.color -= new Color(0.5F, 0, 0) * Time.deltaTime;
        }
    }
    void OnMouseExit()
    {
        foreach (Renderer renderer in rend)
        {
            renderer.material.color = Color.white;
        }
    }

    void OnMouseDown()
    {
        Menu.SetActive(true);
    }

    void BuildConnection() {
        Menu.SetActive(false);
        print("building Connection");
        GameObject connection = (GameObject)Instantiate(ConnectionPrefab, transform);
        connection.transform.localScale = new Vector3(4,4,4);
        connection.transform.position = transform.position;
        connection.transform.Rotate(Vector3.back, 180);
        //gameObject.SetActive(false);
    }

    void BuildFactory()
    {
        Menu.SetActive(false);
        print("building Factory");

        GameObject component = (GameObject)Instantiate(FactoryComponentPrefab, transform);
        component.transform.localScale = new Vector3(4, 4, 4);
        component.transform.position = transform.position;
        component.transform.Rotate(Vector3.back, 180);
        //gameObject.SetActive(false);
    }

}
