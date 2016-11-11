using System;
using UnityEngine;    // For Debug.Log, etc.

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Runtime.Serialization;
using System.Reflection;


// === This is required to guarantee a fixed serialization assembly name, which Unity likes to randomize on each compile
// Do not change this
public sealed class VersionDeserializationBinder : SerializationBinder 
{ 
	public override Type BindToType( string assemblyName, string typeName )
	{ 
		if ( !string.IsNullOrEmpty( assemblyName ) && !string.IsNullOrEmpty( typeName ) ) 
		{ 
			Type typeToDeserialize = null; 

			assemblyName = Assembly.GetExecutingAssembly().FullName; 

			// The following line of code returns the type. 
			typeToDeserialize = Type.GetType( String.Format( "{0}, {1}", typeName, assemblyName ) ); 

			return typeToDeserialize; 
		} 

		return null; 
	} 
}
