using Business.Abstract;
using DataAccess.Abstract;
using Entites.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int CategoryId)
        {
            return _categoryDal.Get(c => c.CategoryId == CategoryId);
        }
    }
}
