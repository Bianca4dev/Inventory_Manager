using Inv_back.Data;
using Inv_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inv_back.Controllers
{
    //?
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController:ControllerBase{
//hoekom n _
//Can replace_ with this.context at line 15(second _context)      
        private readonly InventoryContext _context;

        public ItemsController(InventoryContext context){
            _context =context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems(){
            return await _context.items.ToListAsync();
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Item>>GetItem(int Id){
            var item = await _context.items.FindAsync(Id);
            //if nothing is found
            if(item==null){
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItem(Item item){

            _context.items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem),new {id=item.Id},item);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateItem(int Id,Item item){
            if(Id ==item.Id){
                //Updare the item
                _context.Entry(item).State =EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();

            }else{
                return BadRequest();
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult>Delete(int Id){

            var item = await _context.items.FindAsync(Id);

            if(item ==null){
                return NotFound();
            }

            _context.items.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}