using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;
using UseCases.ProductsUseCaseInterfaces;

namespace UseCases.ProductsUseCase
{
    public class EditProductsUseCase : IEditProductsUseCase
	{
		private readonly IProductRepository productRepository;

		public EditProductsUseCase(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}
		public void Execute(Product product)
		{
			productRepository.UpdateProduct(product);
		}
	}
}
