using System;
using System.Collections.Generic;

public interface ICalculatable {

	string Name{ get; }
	void Pull();
	void Process();

	IList<Batch> AvailableBatches { get;}

}

