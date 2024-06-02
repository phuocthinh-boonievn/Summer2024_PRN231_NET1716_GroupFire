

namespace Data_Layer.ResourceModel.ViewModel.MenuFoodItemVMs
{
    public class MenuFoodItemUpdateVM
    {
        public Guid? CategoryId { get; set; }
        public string FoodName { get; set; }
        public string? FoodDescription { get; set; }
        public string? Image { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
