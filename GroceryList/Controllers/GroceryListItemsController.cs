using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryList.Models;

[Route("api/[controller]")]
[ApiController]
public class GroceryListItemsController : ControllerBase
{
    private readonly GroceryListContext _context;
    public GroceryListItemsController(GroceryListContext context)
    {
        _context = context;
    }

    // GET: api/GroceryListItem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GroceryListItemDTO>>> GetGroceryListItem()
    {
        
        return await _context.GroceryList
            .Select(x => GroceryListItemToDTO(x))
            .ToListAsync();
    }

    // GET: api/GroceryListItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryListItemDTO>> GetGroceryListItem(long id)
    {
        var grocerylistitem = await _context.GroceryList.FindAsync(id);

        if (grocerylistitem == null)
        {
            return NotFound();
        }

        return GroceryListItemToDTO(grocerylistitem);
    }

    // PUT: api/GroceryListItem/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGroceryListItem(long id, GroceryListItemDTO grocerylistitemDTO)
    {
        if (id != grocerylistitemDTO.Id)
        {
            return BadRequest();
        }

        var groceryListItem = await _context.GroceryList.FindAsync(id);

        if(groceryListItem == null)
        {
            return NotFound();
        }

        groceryListItem.Name = grocerylistitemDTO.Name;
        groceryListItem.IsComplete = grocerylistitemDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GroceryListItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/GroceryListItem
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GroceryListItemDTO>> PostGroceryListItem(GroceryListItemDTO grocerylistitemDTO)
    {
        var groceryListItem = new GroceryListItem
        {
            IsComplete = grocerylistitemDTO.IsComplete,
            Name = grocerylistitemDTO.Name,
        };

        _context.GroceryList.Add(groceryListItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGroceryListItem",
            new { id = groceryListItem.Id }, 
            GroceryListItemToDTO(groceryListItem));
    }

    // DELETE: api/GroceryListItem/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroceryListItem(long? id)
    {
        var grocerylistitem = await _context.GroceryList.FindAsync(id);
        if (grocerylistitem == null)
        {
            return NotFound();
        }

        _context.GroceryList.Remove(grocerylistitem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GroceryListItemExists(long? id)
    {
        return _context.GroceryList.Any(e => e.Id == id);
    }



    private static GroceryListItemDTO GroceryListItemToDTO(GroceryListItem groceryListItem) =>
     new GroceryListItemDTO
     {
         Id = groceryListItem.Id,
         Name = groceryListItem.Name,
         IsComplete = groceryListItem.IsComplete
     };
}
