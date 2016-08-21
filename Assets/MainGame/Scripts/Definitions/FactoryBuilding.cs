﻿using System;
using UnityEngine;

public class FactoryBuilding : Calculatable
{
	public FactoryBuilding ()
	{
	}

	Batch workWith;

	protected override void Pull(ICalculatable dependency){
		workWith = null;
		if (dependency.AvailableBatches.Count > 0) {
			workWith = dependency.AvailableBatches [0];
			dependency.AvailableBatches.RemoveAt (0);
			workWith.gameObject.transform.parent = transform;
		}
	}

	public override void Process(){
		if (workWith != null) {
			ProductBatch prod = CreateProductBatch ();
			prod.Ammount = 1;
			prod.Type = "bla";
			AvailableBatches.Add (prod);
			prod.transform.parent = transform;
		}
		Debug.LogWarning ("Factory Building " + Name + " has " + AvailableBatches.Count + " batches");
	}
}

