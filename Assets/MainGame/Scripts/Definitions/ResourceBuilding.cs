using System;
using UnityEngine;
public class ResourceBuilding : Calculatable
{
	public string ResourceType;

	protected override void Pull(ICalculatable dependency){
	}

	protected override void ProcessInternal(){
		ResourceBatch batch = CreateResourceBatch ();
		batch.Ammount = 1;
		batch.Type = ResourceType;
		AvailableBatches.Add (batch);
		Debug.LogWarning ("Resource Building " + Name + " has "+ AvailableBatches.Count +" batches");
		batch.transform.parent = transform;
	}
}

