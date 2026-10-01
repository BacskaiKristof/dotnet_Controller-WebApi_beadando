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
    public async Task<ActionResult<IEnumerable<GroceryListItem>>> GetGroceryListItem()
    {
        return await _context.GroceryList.ToListAsync();
    }

    // GET: api/GroceryListItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryListItem>> GetGroceryListItem(long id)
    {
        var grocerylistitem = await _context.GroceryList.FindAsync(id);

        if (grocerylistitem == null)
        {
            return NotFound();
        }

        return grocerylistitem;
    }

    // PUT: api/GroceryListItem/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGroceryListItem(long? id, GroceryListItem grocerylistitem)
    {
        if (id != grocerylistitem.Id)
        {
            return BadRequest();
        }

        _context.Entry(grocerylistitem).State = EntityState.Modified;

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
    public async Task<ActionResult<GroceryListItem>> PostGroceryListItem(GroceryListItem grocerylistitem)
    {
        _context.GroceryList.Add(grocerylistitem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGroceryListItem", new { id = grocerylistitem.Id }, grocerylistitem);
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
}
