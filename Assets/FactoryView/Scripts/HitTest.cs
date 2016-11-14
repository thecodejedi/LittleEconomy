using UnityEngine;
using System.Collections;

public class HitTest : MonoBehaviour {

    public TileConstructorBehavior ParentTile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnMouseDown()
    {
        ParentTile.MouseDown();
    }

    private void OnMouseEnter()
    {
        ParentTile.MouseEnter();
    }

    private void OnMouseExit()
    {
        ParentTile.MouseExit();
    }

    private void OnMouseOver()
    {
        ParentTile.MouseOver();
    }
}

public interface IMouseEventReceiver
{
    void MouseDown();
    void MouseEnter();
    void MouseExit();
    void MouseOver();
}
