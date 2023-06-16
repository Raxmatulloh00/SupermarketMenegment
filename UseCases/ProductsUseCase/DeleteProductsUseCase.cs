using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;
using UseCases.ProductsUseCaseInterfaces;

namespace UseCases.ProductsUseCase
{
    public class DeleteProductsUseCase : IDeleteProductsUseCase
	{
		private readonly IProductRepository productRepository;
		
		public DeleteProductsUseCase(IProductRepository productRepository)
		{
			this.productRepository = productRepository;
		}

		public void Execute(int productId)
		{
			productRepository.DeleteProduct(productId);
			
		}
	}
}
