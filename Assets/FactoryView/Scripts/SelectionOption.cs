using System;

public class SelectionOption
{
    public TileConstructorBehavior Parent { get; private set; }

    public SelectionOption(string text, TileConstructorBehavior parent) {
        Parent = parent;
        Text = text;
    }

    public string Text { get; set; }

    public void Selected() {
        Parent.Select(Text);
    }
}