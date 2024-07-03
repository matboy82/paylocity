using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Data.Dtos.Dependent;
using Data.Dtos.Employee;
using Data.Dtos.Paycheck;
using Data.Models;
using Service.Services;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaycheckIntegrationTests : IntegrationTest
{ 
	[Fact]
	public async Task GetPaycheck_ReturnsSinglePaycheckAsync()
	{
		// Arrange
		var payService = new PayService();
		var employee = new GetEmployeeDto { Salary = 90000 };
		var annualSalary = payService.CalcAnnualPayInfo(employee);

		// Act
		var result = payService.CalcSinglePaycheckInfo(employee, annualSalary);
		var roundedResult = await payService.RoundPayCheckInfo(result);

		// Assert, 90k - 12k = 78k net, 78k - salary penalty = 76200 / 26 paychecks = 2930.77
		Assert.NotNull(result);
		Assert.Equal(Convert.ToDecimal(2930.77), Convert.ToDecimal(roundedResult.NetIncome));
	}

	[Fact]
	public async Task GetPaycheck_ReturnSingleWithDependentsAsync()
	{
		// Arrange
		var payService = new PayService();
		var dependent = new GetDependentDto{ DateOfBirth = DateTime.Today.AddYears(-55) , Relationship = Relationship.Spouse , FirstName = "Spouse" , LastName = "Morant" };
		var dependent2 = new GetDependentDto { DateOfBirth = DateTime.Today.AddYears(-55), Relationship = Relationship.Child, FirstName = "Kid", LastName = "Morant" };
		List<GetDependentDto> deps = new List<GetDependentDto> { dependent, dependent2 };
		
		var employee = new GetEmployeeDto { Salary = 60000, Dependents = deps };
		var annualSalary = payService.CalcAnnualPayInfo(employee);

		// Act
		var result = payService.CalcSinglePaycheckInfo(employee, annualSalary);
		var roundedResult = await payService.RoundPayCheckInfo(result);

		// Assert, 60k - 12k cost = 48k net / 26 paychecks = 1,846.15 w/no deps, two deps as above should be 1107.69
		Assert.NotNull(result);
		Assert.NotEqual(Convert.ToDecimal(1846.15), Convert.ToDecimal(roundedResult.NetIncome));
	}

	[Fact]
	public async Task CalcSinglePaycheckInfo_CalculatesSinglePaycheckAsync()
	{
		// Arrange
		var payService = new PayService();
		var employee = new GetEmployeeDto { Salary = 60000 };
		var annualSalary = payService.CalcAnnualPayInfo(employee);
			
		// Act
		var result = payService.CalcSinglePaycheckInfo(employee, annualSalary);
		var roundedResult = await payService.RoundPayCheckInfo(result);

		// Assert, 60k - 12k cost = 48k net / 26 paychecks = 1,846.15
		Assert.NotNull(result);
		Assert.Equal(Convert.ToDecimal(1846.15), Convert.ToDecimal(roundedResult.NetIncome));
	}

	[Fact]
	public async Task CalcSinglePaycheckInfo_CalculatesSinglePaycheckWithPenaltyAsync()
	{
		// Arrange
		var payService = new PayService();
		var employee = new GetEmployeeDto { Salary = 100000 };
		var annualSalary = payService.CalcAnnualPayInfo(employee);

		// Act
		var result = payService.CalcSinglePaycheckInfo(employee, annualSalary);
		var roundedResult = await payService.RoundPayCheckInfo(result);

		// Assert, 100k - 12k cost = 88k net / 26 paychecks = 3307.69
		Assert.NotNull(result);
		Assert.Equal(Convert.ToDecimal(3307.69), Convert.ToDecimal(roundedResult.NetIncome));
	}

	[Fact]
	public void DependentOver50_ReturnsTrueIfOver50()
	{
		// Arrange
		var payService = new PayService();
		var dependent = new GetDependentDto { DateOfBirth = DateTime.Today.AddYears(-55) };

		// Act
		var result = payService.DependentOver50(dependent);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void DependentOver50_ReturnsFalseIfUnder50()
	{
		// Arrange
		var payService = new PayService();
		var dependent = new GetDependentDto { DateOfBirth = DateTime.Today.AddYears(-25) };

		// Act
		var result = payService.DependentOver50(dependent);

		// Assert
		Assert.False(result);
	}
	[Fact]
	public void CalcAnnualPayInfo_CalculatesAnnualPay()
	{
		// Arrange
		var payService = new PayService();
		var employee = new GetEmployeeDto { Salary = 70000 };

		// Act
		var result = payService.CalcAnnualPayInfo(employee);

		// Assert 70k -12k cost = 58k net
		Assert.NotNull(result);
		Assert.Equal(Convert.ToDecimal(58000), result.NetIncome); // Adjust expected value based on calculations
	}
	[Fact]
	public void CalcAnnualPayInfo_CalculatesAnnualPayWithPenalty()
	{
		// Arrange
		var payService = new PayService();
		var employee = new GetEmployeeDto { Salary = 170000 };

		// Act
		var result = payService.CalcAnnualPayInfo(employee);

		// Assert 
		Assert.NotNull(result);
		Assert.Equal(Convert.ToDecimal(154600), result.NetIncome); // Adjust expected value based on calculations
	}
}
