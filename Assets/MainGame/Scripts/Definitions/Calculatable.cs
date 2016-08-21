using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Calculatable : MonoBehaviour, ICalculatable
{
	IList<Batch> availableBatches = new List<Batch> ();

	protected RoundCalculation roundCalculation;

	public IList<Batch> AvailableBatches {
		get {
			return availableBatches;
		}
	}

	IList<ICalculatable> dependencies = new List<ICalculatable> ();

	public string Name{ get { return name; } }

	public IList<ICalculatable> Dependencies {
		get {
			return dependencies;
		}
	}

	protected virtual void Start () {
		roundCalculation = GameObject.Find("RoundCalculation").GetComponent<RoundCalculation>();
	}

	public void NextRound(){
		
	}

	protected abstract void Pull (ICalculatable dependency);

	public abstract void Process();

	public void Pull(){
		foreach (ICalculatable dependency in Dependencies) {
			Pull (dependency);
		}

		IList<ICalculatable> calculatables = GetComponentsInChildren<ICalculatable> ();
		foreach (ICalculatable calc in calculatables) {
			if (calc == this)
				continue;
			calc.Pull ();
		}
	}

	protected static ResourceBatch CreateResourceBatch(){
		GameObject template = GameObject.Find ("ResourceBatchTemplate");
		GameObject copy = GameObject.Instantiate(template);

		ResourceBatch batch = copy.GetComponent<ResourceBatch>();
		return batch;
	}

	protected static ProductBatch CreateProductBatch(){
		GameObject template = GameObject.Find ("ProductBatchTemplate");
		GameObject copy = GameObject.Instantiate(template);

		ProductBatch batch = copy.GetComponent<ProductBatch>();
		return batch;
	}
}

