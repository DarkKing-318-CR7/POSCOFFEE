using CoffeeShopSystem.Services;
using CoffeeShopSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly IDrinkSvc _drinkSvc;

        public DrinkController(IDrinkSvc drinkSvc)
        {
            _drinkSvc = drinkSvc;
        }

        [HttpPost("recipe")]
        public async Task<IActionResult> AddOrUpdateRecipe([FromBody] AddDrinkIngredientDto dto)
        {
            try
            {
                var result = await _drinkSvc.AddOrUpdateRecipeIngredientAsync(dto);
                return Ok(new { message = "Cập nhật công thức thành công!", data = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        [HttpDelete("{drinkId}/recipe/{ingredientId}")]
        public async Task<IActionResult> RemoveRecipe(int drinkId, int ingredientId)
        {
            try
            {
                var result = await _drinkSvc.RemoveRecipeIngredientAsync(drinkId, ingredientId);
                return Ok(new { message = "Xóa nguyên liệu khỏi công thức thành công!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        [HttpGet("{drinkId}/cost")]
        public async Task<IActionResult> GetDrinkCost(int drinkId)
        {
            try
            {
                var result = await _drinkSvc.CalculateDrinkCostAsync(drinkId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }

        [HttpGet("cost-report")]
        public async Task<IActionResult> GetAllDrinksCostReport()
        {
            try
            {
                var result = await _drinkSvc.GetAllDrinksCostReportAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }
}
