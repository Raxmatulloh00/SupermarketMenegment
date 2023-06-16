using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePulignInterfaces;
using UseCases.UseCaseInterfaces;

namespace UseCases.CategoriesUseCases
{
    public class ViewCategoriesUseCase : IViewCategoriesUseCase
    {
        private readonly ICategoryRepository categoryRepository;

        public ViewCategoriesUseCase(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IEnumerable<Category> Execute()
        {
            return categoryRepository.GetCategories();
        }

        public bool Something(int id)
        {
            if (id == 1)
                return true;
            else
                return false;
        }

        public int Qoshish(int sum1, int sum2)
        {
            return sum1 + sum2;
        }
    }
}
