using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Scratch.Regression;

namespace Tests
{
	public class ModelAnalysisTests
	{
		[Test]
		public void ModelAnalysis_RMSE_Simple()
		{
			var data = new (decimal x, decimal y)[]
			{
				(1m, 1m),
				(2m, 3m),
				(4m, 3m),
				(3m, 2m),
				(5m, 5m)
			};

			var solution = Regression.Solve(data);

			(decimal predicted, decimal actual)[] predicted =
				data.Select(data => 
					(
						predicted: Regression.Predict(
							data.x,
							solution.b1,
							solution.b0),
						actual: data.y
					))
				.ToArray();

			var predictions = predicted.Select(p => p.predicted).ToArray();

			decimal rmse = ModelAnalysis.RMSE(predicted);

			Assert.AreEqual(
				new decimal[]
				{
					1.2m,
					2.0m,
					3.6m,
					2.8m,
					4.4m
				},
				predictions);

			Assert.AreEqual(
				0.692820323027551m,
				rmse);
		}

		[Test]
		public void ModelAnalysis_R2_Simple()
		{
			var data = new (decimal x, decimal y)[]
			{
				(3.600m, 79),
				(1.800m, 54),
				(3.333m, 74),
				(2.283m, 62),
				(4.533m, 85),
				(2.883m, 55)
			};

			var solution = Regression.Solve(data);

			(decimal predicted, decimal actual)[] predicted =
				data.Select(data =>
					(
						predicted: Regression.Predict(
							data.x,
							solution.b1,
							solution.b0),
						actual: data.y
					))
				.ToArray();

			var predictions = predicted.Select(p => p.predicted).ToArray();

			decimal rmse = ModelAnalysis.RMSE(predicted);
			decimal r2 = ModelAnalysis.R2(predicted);

			Assert.AreEqual(12.024839143342135994814248709m, solution.b1);
			Assert.AreEqual(31.226360818319624890597294633m, solution.b0);

			Assert.AreEqual(0.8123688809965797287108716811m, r2);
		}
	}
}
