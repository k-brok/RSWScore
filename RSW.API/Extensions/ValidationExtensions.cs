using System.ComponentModel.DataAnnotations;

public static class ValidationExtensions
{
    public static IResult Validate<T>(this T model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        if (!Validator.TryValidateObject(model, context, results, true))
        {
            return Results.ValidationProblem(results.ToDictionary(
                v => v.MemberNames.FirstOrDefault() ?? "",
                v => new[] { v.ErrorMessage ?? "Ongeldige waarde" }
            ));
        }

        return null;
    }
}
