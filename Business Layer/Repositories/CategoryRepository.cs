using AutoMapper;
using Business_Layer.DataAccess;
using Data_Layer.ResourceModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FastFoodDeliveryDBContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(FastFoodDeliveryDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryVM>> GetAllCategory()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryVM>>(categories);
        }
    }
}
