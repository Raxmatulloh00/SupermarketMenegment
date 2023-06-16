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
    public class GetProductsByIdUseCase : IGetProductsByIdUseCase
	{
		private readonly IProductRepository productRepository;

		public GetProductsByIdUseCase(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public Product Execute(int productId)
		{
			return productRepository.GetProductById(productId);
		}
	}
}
