using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SolarisMci.Pages.PartialViews
{
    public class PaginationModel : PageModel
    {
        [BindProperty]
        public int TotalPages { get; set; } = 0;

        [BindProperty]
        public string Parameters { get; set; } = "";
        public void OnPost() { }

        public void OnGet(int totalPages = 1) { 
            TotalPages = totalPages;
        }
    }
}
