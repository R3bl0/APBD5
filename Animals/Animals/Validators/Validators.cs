using FluentValidation;

namespace Animals.Validators
{
    public static class Validators
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AnimalCreateRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<AnimalReplaceRequestValidator>();
        }
    }
}