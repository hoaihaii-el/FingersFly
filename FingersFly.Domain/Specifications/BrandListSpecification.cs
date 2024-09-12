using FingersFly.Domain.Entities;

namespace FingersFly.Domain.Specifications
{
    public class BrandListSpecification : BaseSpecification<Product, string>
    {
        public BrandListSpecification() 
        {
            AddSelect(p => p.Brand);
            AddDistinct();
        }
    }
}
