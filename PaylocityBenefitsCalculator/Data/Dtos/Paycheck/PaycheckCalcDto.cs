using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.Paycheck
{
	/// <summary>
	/// Model for annual pay calculation
	/// </summary>
	public class PaycheckCalcDto
	{
		public decimal GrossIncome { get; set; }
		public decimal BaseCost { get; set; }
		public decimal SalaryPenalty { get; set; }
		public decimal DependentCost { get; set; }
		public decimal NetIncome { get; set; }
	}
}
