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
    public class AddProductsUseCase : IAddProductsUseCase
	{
		private readonly IProductRepository productRepository;

		public AddProductsUseCase(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}
		public void Execute(Product product)
		{
			productRepository.AddProduct(product);
		}
	}
}
