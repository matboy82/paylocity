using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos.Dependent;
using Data.Dtos.Employee;
using Data.Dtos.Paycheck;

namespace Service.Services
{
	public class PayService : IPayService
	{
		public Task<PaycheckCalcDto> GetPaycheck(GetEmployeeDto employee)
		{
			PaycheckCalcDto payCalculator = CalcAnnualPayInfo(employee);
			PaycheckCalcDto singlePaycheck = CalcSinglePaycheckInfo(employee, payCalculator);

			return Task.FromResult(singlePaycheck);
		}

		public PaycheckCalcDto CalcSinglePaycheckInfo(GetEmployeeDto employee, PaycheckCalcDto payCalculator)
		{
			// Calc for a single paycheck (26/year)
			var singlePaycheck = new PaycheckCalcDto();
			singlePaycheck.GrossIncome = employee.Salary;
			singlePaycheck.BaseCost = payCalculator.BaseCost / 26;
			singlePaycheck.DependentCost = payCalculator.DependentCost / 26;
			singlePaycheck.SalaryPenalty = (payCalculator.SalaryPenalty == Convert.ToDecimal(0.00) ? Convert.ToDecimal(0.00) : payCalculator.SalaryPenalty / 26);
			singlePaycheck.NetIncome = payCalculator.NetIncome / 26;
			return singlePaycheck;
		}

		public PaycheckCalcDto CalcAnnualPayInfo(GetEmployeeDto employee)
		{
			//Calc for Annual information
			var payCalculator = new PaycheckCalcDto();
			payCalculator.GrossIncome = employee.Salary;
			payCalculator.BaseCost = Convert.ToDecimal(1000.00 * 12);
			payCalculator.DependentCost = (600 * employee.Dependents.Count()) * 12;
			payCalculator.SalaryPenalty = (employee.Salary > Convert.ToDecimal(80000.00) ? Convert.ToDecimal(0.02) : Convert.ToDecimal(0.00)) * employee.Salary;

			foreach (var dependent in employee.Dependents)
			{
				if (DependentOver50(dependent))
				{
					payCalculator.DependentCost += Convert.ToDecimal(200.00 * 12);
				}
			}
			payCalculator.NetIncome = payCalculator.GrossIncome - payCalculator.BaseCost - payCalculator.DependentCost - payCalculator.SalaryPenalty;
			return payCalculator;
		}

		/// <summary>
		/// Dealing with money, so round it using the bankers rounding
		/// </summary>
		/// <param name="paycheck"></param>
		/// <returns></returns>
		public Task<PaycheckCalcDto> RoundPayCheckInfo(PaycheckCalcDto paycheck)
		{
			paycheck.BaseCost = Math.Round(paycheck.BaseCost, 2, MidpointRounding.ToEven);
			paycheck.DependentCost = Math.Round(paycheck.DependentCost, 2, MidpointRounding.ToEven);
			paycheck.SalaryPenalty = Math.Round(paycheck.SalaryPenalty, 2, MidpointRounding.ToEven);
			paycheck.NetIncome = Math.Round(paycheck.NetIncome, 2, MidpointRounding.ToEven);
			return Task.FromResult(paycheck);
		}

		public bool DependentOver50(GetDependentDto dependent)
		{
			TimeSpan ageDifference = DateTime.Today - dependent.DateOfBirth;
			int age = (int)(ageDifference.TotalDays / 365.25); // Approximate calculation for age in years

			return age > 50;
		}
	}
}
