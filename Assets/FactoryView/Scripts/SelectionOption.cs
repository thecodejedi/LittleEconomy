using System;

public class SelectionOption
{
    public TileConstructorBehavior TileConstructor { get; private set; }

    public SelectionOption(string text, TileConstructorBehavior tileConstructor) {
        TileConstructor = tileConstructor;
        Text = text;
    }

    public string Text { get; set; }

    public void Selected() {
        TileConstructor.Select(Text);
    }
}