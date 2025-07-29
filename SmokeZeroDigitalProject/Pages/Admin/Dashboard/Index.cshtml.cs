using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokeZeroDigitalSolution.Domain.Entites;

namespace SmokeZeroDigitalProject.Pages.Admin.Dashboard
{
	public class MonthlySales
	{
		public string Month { get; set; }
		public decimal Revenue { get; set; }
	}

	public class IndexModel : PageModel
	{
		public List<MonthlySales> MonthlyRevenues { get; set; }

		public void OnGet()
		{
			MonthlyRevenues = new List<MonthlySales>
		{
			new() { Month = "Jan", Revenue = 1200000 },
			new() { Month = "Feb", Revenue = 1500000 },
			new() { Month = "Mar", Revenue = 1800000 },
			new() { Month = "Apr", Revenue = 2200000 },
			new() { Month = "May", Revenue = 2000000 },
			new() { Month = "Jun", Revenue = 2500000 },
			new() { Month = "Jul", Revenue = 2400000 },
			new() { Month = "Aug", Revenue = 2300000 },
			new() { Month = "Sep", Revenue = 2100000 },
			new() { Month = "Oct", Revenue = 2600000 },
			new() { Month = "Nov", Revenue = 3000000 },
			new() { Month = "Dec", Revenue = 3500000 }
		};
		}
	}

}
