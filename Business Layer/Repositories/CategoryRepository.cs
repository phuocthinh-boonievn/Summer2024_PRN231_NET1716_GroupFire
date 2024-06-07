using AutoMapper;
using Business_Layer.Commons;
using Business_Layer.DataAccess;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly FastFoodDeliveryDBContext _dbContext;
        public CategoryRepository(FastFoodDeliveryDBContext context, FastFoodDeliveryDBContext dbContext) : base(context)
        {
            _dbContext = dbContext;
        }
    }
}
