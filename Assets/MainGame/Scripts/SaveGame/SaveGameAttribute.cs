using System;
[AttributeUsage(AttributeTargets.Class)]
public class SaveGameAttribute : Attribute
{

	private bool persistPosition = false;
	public SaveGameAttribute(bool persistPosition)
	{
		this.persistPosition = persistPosition;
	}

	public bool PersistPosition
	{
		get
		{
			return persistPosition;
		}

		set
		{
			persistPosition = value;
		}
	}
}
