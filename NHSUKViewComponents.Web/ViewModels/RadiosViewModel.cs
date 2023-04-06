namespace NHSUKViewComponents.Web.ViewModels
{
    using System.Collections.Generic;

    public class RadiosViewModel
    {
        public RadiosViewModel(
            string aspFor,
            string label,
            string? hintText,
            IEnumerable<RadiosItemViewModel> radios,
            string? errorMessage = null,
            bool hasError = false,
            bool required = false
        )
        {
            AspFor = aspFor;
            Label = (!required && !label.EndsWith("(optional)") ? label + " (optional)" : label);
            HintText = hintText;
            Radios = radios;
            ErrorMessage = errorMessage;
            HasError = hasError;
            Required = required;
        }

        public string AspFor { get; set; }

        public string Label { get; set; }

        public string? HintText { get; set; }

        public IEnumerable<RadiosItemViewModel> Radios { get; set; }
        public bool Required { get; set; }
        public string? ErrorMessage { get; set; }
        public bool HasError { get; set; }
    }
}
