using CoffeeShopSystem.Models;
using CoffeeShopSystem.Services;
using CoffeeShopSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IIngredientSvc _ingredientSvc;
        public IngredientController(IIngredientSvc ingredientSvc)
        {
            _ingredientSvc = ingredientSvc;
        }

        [HttpPost("add-stock-entry")]
        public async Task<IActionResult>AddStockEntry(CreateStockEntryDto stockEntryDto)
        {
            try
            {
                var stockEntry = await _ingredientSvc.AddStockEntry(stockEntryDto);
                return Ok(stockEntry);
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

        [HttpPost("add-ingredient")]
        public async Task<IActionResult> AddIngredient(CreateIngredientDto ingredientDto)
        {
            try
            {
                var ingredient = await _ingredientSvc.AddIngredient(ingredientDto);
                return Ok(ingredient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-ingredient/{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            try
            {
                var ingredient = await _ingredientSvc.DeleteIngredient(ingredientId);
                return Ok(ingredient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-stock-entry/{stockEntryId}")]
        public async Task<IActionResult> DeleteStockEntry(int stockEntryId)
        {
            try
            {
                var stockEntry = await _ingredientSvc.DeleteStockEntry(stockEntryId);
                return Ok(stockEntry);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-ingredient/{ingredientId}")]
        public async Task<IActionResult> GetIngredientById(int ingredientId)
        {
            try
            {
                var ingredient = await _ingredientSvc.GetIngredientById(ingredientId);
                return Ok(ingredient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        
    }
}
