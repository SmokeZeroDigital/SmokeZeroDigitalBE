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
			new() { Month = "Jan", Revenue = 1200 },
			new() { Month = "Feb", Revenue = 1500 },
			new() { Month = "Mar", Revenue = 1800 },
			new() { Month = "Apr", Revenue = 2200 },
			new() { Month = "May", Revenue = 2000 },
			new() { Month = "Jun", Revenue = 2500 },
			new() { Month = "Jul", Revenue = 2400 },
			new() { Month = "Aug", Revenue = 2300 },
			new() { Month = "Sep", Revenue = 2100 },
			new() { Month = "Oct", Revenue = 2600 },
			new() { Month = "Nov", Revenue = 3000 },
			new() { Month = "Dec", Revenue = 3500 }
		};
		}
	}

}
