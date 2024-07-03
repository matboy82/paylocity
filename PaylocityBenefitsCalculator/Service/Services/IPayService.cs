using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Dtos.Employee;
using Data.Dtos.Paycheck;
namespace Service.Services
{
	public interface IPayService
	{
		//TODO finish filling out the pay service, this should contain the logic for pay calculations
		// given employee, do the calculations, return paycheck data
		Task<PaycheckCalcDto> GetPaycheck(GetEmployeeDto employee);
		Task<PaycheckCalcDto> RoundPayCheckInfo(PaycheckCalcDto paycheck);
		PaycheckCalcDto CalcAnnualPayInfo(GetEmployeeDto employee);
		PaycheckCalcDto CalcSinglePaycheckInfo(GetEmployeeDto employee, PaycheckCalcDto payCalculator);
	}
}
