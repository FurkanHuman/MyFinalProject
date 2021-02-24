﻿using Entites.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int CategoryId);
    }
}
