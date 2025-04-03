using FluentValidation;

namespace CatalogAPI.Products.GetProductByCategoryName
{
    public class GetProductsByCategoryNameQueryValidator : AbstractValidator<GetProductsByCategoryNameQuery>
    {
        public GetProductsByCategoryNameQueryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotNull()
                .NotEmpty();
        }
    }
}
