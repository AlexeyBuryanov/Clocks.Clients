using System.ComponentModel.DataAnnotations;

namespace Clocks.Clients.Core.Validations
{
    public class ValidUrlRule : IValidationRule<string>
    {
        public ValidUrlRule()
        {
            ValidationMessage = "Должен быть URL-адрес";
        }
        
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return new UrlAttribute().IsValid(value);
        }
    }
}
