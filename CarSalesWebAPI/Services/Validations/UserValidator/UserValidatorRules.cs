namespace CarSalesWebAPI.Services.Validations.UserValidator
{
    public static class UserValidatorRules
    {
        public static bool BeOver18(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;

            // Verificar se a pessoa já fez aniversário este ano
            if (birthday.Date > today.AddYears(-age))
            {
                age--;
            };

            return age >= 18;
        }
    }
}
