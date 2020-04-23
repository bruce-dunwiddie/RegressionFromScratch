using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Scratch.Regression;

namespace Tests
{
	public class RegressionTests
	{
		[Test]
		public void Regression_Solve_Simple()
		{
			// https://machinelearningmastery.com/implement-simple-linear-regression-scratch-python/

			var solution = Regression.Solve(
				(1m, 1m),
				(2m, 3m),
				(4m, 3m),
				(3m, 2m),
				(5m, 5m));

			Assert.AreEqual(0.8, solution.b1);
			Assert.AreEqual(0.4, solution.b0);
		}

		[Test]
		public void Regression_Solve_OLS()
		{
			// Boston dataset downloaded from http://lib.stat.cmu.edu/datasets/boston

			// validating results against values from https://towardsdatascience.com/simple-and-multiple-linear-regression-in-python-c928425168f9


		}

		[Test]
		public void Regression_Predict()
		{
			decimal[] dataset = new decimal[]
			{
				1,
				2,
				4,
				3,
				5
			};

			var predictions = dataset.Select(d => Regression.Predict(
				d,
				0.8m,
				0.4m)).ToArray();

			Assert.AreEqual(
				new decimal[]
				{
					1.2m,
					2m,
					3.6m,
					2.8m,
					4.4m
				},
				predictions);
		}
	}
}
