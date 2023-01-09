using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolarisMci.Models;
using SolarisMci.Settings;

namespace SolarisMci.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly ILogger<SettingsModel> _logger;
        private readonly INumberSettings _numberSettings;

        public SettingsModel(ILogger<SettingsModel> logger, INumberSettings numberSettings)
        {
            _logger = logger;
            _numberSettings = numberSettings;

        }

        [BindProperty]
        public int NumOfDigits { get; set; }

        [BindProperty]
        public int MaxDigit { get; set; }

        [BindProperty]
        public int MinDigit { get; set; }

        [BindProperty]

        public int NumOfLetters { get; set; }
        [BindProperty]

        public char MaxLetter { get; set; }
        [BindProperty]

        public char MinLetter { get; set; }
        public void OnGet()
        {

            NumOfDigits = _numberSettings.NumOfDigits;
            NumOfLetters = _numberSettings.NumOfLetters;
            MaxDigit = _numberSettings.MaxDigit;
            MinDigit = _numberSettings.MinDigit;
            MinLetter = _numberSettings.MinLetter;
            MaxLetter = _numberSettings.MaxLetter;

        }

        public void OnPost()
        {
            _numberSettings.NumOfDigits = NumOfDigits;
            _numberSettings.MinDigit = MinDigit;
            _numberSettings.MaxDigit = MaxDigit;
            _numberSettings.NumOfLetters = NumOfLetters;
            _numberSettings.MinLetter = MinLetter;
            _numberSettings.MaxLetter = MaxLetter;

        }
    }
}