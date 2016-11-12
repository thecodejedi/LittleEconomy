using System;
[AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
public class SaveGameValueAttribute : Attribute
	{
		public SaveGameValueAttribute()
		{
		}
	}
