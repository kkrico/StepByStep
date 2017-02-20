using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StepByStep.Web.Models
{
    public class CPF : ValidationAttribute
    {
        public CPF()
        {
            ErrorMessage = "CPF Inválido";
        }

        public bool MostrarFormatado { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {


            var requerido = BuscarRequerido(context);
            if (!requerido)
            {
                if (string.IsNullOrEmpty(value as string))
                {
                    return ValidationResult.Success;
                }
            }

            if (!(value is string)) return new ValidationResult(ErrorMessage);

            var cpfInserido = (string)value;

            var mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpfInserido = cpfInserido.Trim();
            cpfInserido = cpfInserido.Replace(".", "").Replace("-", "");

            if (cpfInserido.Length != 11)
                return new ValidationResult(ErrorMessage);

            var tempCpf = cpfInserido.Substring(0, 9);
            var soma = 0;
            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt1[i];

            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpfInserido.EndsWith(digito) ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }

        private bool BuscarRequerido(ValidationContext context)
        {
            var model = context.ObjectInstance;

            var propriedade = context.DisplayName;
            var propertyName = model.GetType()
                .GetProperties().First(p => p.Name == propriedade);

            var required = propertyName.GetCustomAttributes(typeof(RequiredAttribute), false);

            return required.Length > 0;
        }
    }
}