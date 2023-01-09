using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using SolarisMci.Settings;
using System.Text.RegularExpressions;

namespace SolarisMci.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INumberSettings _numberSettings;


        private readonly int pageSize = 40;
        public IndexModel(ILogger<IndexModel> logger, INumberSettings numberSettings)
        {
            _logger = logger;
            _numberSettings = numberSettings;
        }

        [BindProperty]
        public string? StartRange { get; set; }
        [BindProperty]
        public string? EndRange { get; set; }

        [BindProperty]
        public string? Parameters { get; set; } = "";

        [BindProperty]
        public int? TotalPages { get; set; }
        [BindProperty]
        public List<string> IdLists { get; set; }

        [BindProperty]
        public string Error { get; set; }


        public IActionResult OnPost() {
            string url = "/?pageNum=1&start=" + StartRange + "&end=" + EndRange;

            return Redirect(url);
        }
        public void OnGet(string? pageNum,string? start, string? end)
        {
            int page = int.Parse(pageNum ?? "1");
            StartRange = start ?? "".PadLeft(_numberSettings.NumOfLetters, _numberSettings.MinLetter) + "".PadRight(_numberSettings.NumOfDigits, (Char)(_numberSettings.MinDigit + 48));
            EndRange = end ?? "".PadLeft(_numberSettings.NumOfLetters, _numberSettings.MaxLetter) + "".PadRight(_numberSettings.NumOfDigits, (Char)(_numberSettings.MaxDigit + 48));
            Error = EndRange.CompareTo(StartRange) < 0 ? "Invalid range: \"End is smaller than start\"" : "";

            if (String.IsNullOrEmpty(Error))
            {
                var tempList = new List<string>() { FormatNumber(StartRange) };

                string iteration = StartRange;
                while (!iteration.Equals(EndRange))
                {

                    iteration = IncrementString(iteration);
                    tempList.Add(FormatNumber(iteration));
                }
                TotalPages = (int)Math.Ceiling((decimal)tempList.Count / pageSize);
                int startIndex = (page-1) * pageSize < tempList.Count ? (page - 1) * pageSize : tempList.Count-1;
                IdLists = tempList.Skip(startIndex).Take(pageSize).ToList();
            }


        }



        private string IncrementString(string Id)
        {
            var temp = IncrementYieldString(Id);
            string s = "";
            while (temp.MoveNext())
            {
                s += temp.Current;
            }
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private IEnumerator<char> IncrementYieldString(string Id)
        {
            bool inc = true;
            for (int i = Id.Length - 1; i >= 0; i--)
            {
                char c = Id.ElementAt(i);
                if (Char.IsDigit(c) && (c == '9' || (i == Id.Length - 1 && c == (char)(_numberSettings.MaxDigit + 48))) && inc)
                {
                    yield return (Char)(_numberSettings.MinDigit+48);
                }
                else if (Char.IsLetter(c) && c == _numberSettings.MaxLetter && inc)
                {
                    yield return _numberSettings.MinLetter;

                }
                else if (inc)
                {
                    yield return ++c;
                    inc = false;
                }
                else
                    yield return c;
            }
        }

        private string FormatNumber(string Id)
        {

            string letters = Regex.Replace(Id, @"\d", "");
            string digits = Regex.Replace(Id, @"\D", "");

            string format = "D" + _numberSettings.ToString();
            string result = letters + digits.ToString().PadLeft(_numberSettings.NumOfDigits + 1, '0');

            return result;
        }
    }
}