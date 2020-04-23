using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Scratch.Regression;

namespace Tests
{
	public class FormulasTests
	{
		[Test]
		public void Formulas_Mean_Simple()
		{
			decimal mean = Formulas.Mean(
				2.1m,
				2.2m,
				2.3m);

			Assert.AreEqual(2.2, mean);
		}

		[Test]
		public void Formulas_Variance_Simple()
		{
			decimal variance = Formulas.Variance(
				1m,
				2m,
				3m,
				4m,
				5m);

			Assert.AreEqual(10, variance);
		}

		[Test]
		public void Formulas_Covariance_Simple()
		{
			decimal covariance = Formulas.Covariance(
				(1m, 1m),
				(2m, 3m), 
				(4m, 3m), 
				(3m, 2m), 
				(5m, 5m));

			Assert.AreEqual(8, covariance);
		}
	}
}
