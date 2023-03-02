using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PRSBackEndCaptsone.Models;

namespace PRSBackEndCaptsone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestLinesController : ControllerBase
    {
        private readonly Context _context;

        public RequestLinesController(Context context)
        {
            _context = context;
        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLines()
        {
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                recalcRequestTotal(requestLine.RequestId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
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

        // POST: api/RequestLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine)
        {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();
            recalcRequestTotal(requestLine.RequestId);

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestLine(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            int theRequesttId = requestLine.RequestId;

            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();

            recalcRequestTotal(requestLine.RequestId);

            return NoContent();
        }

        private bool RequestLineExists(int id)
        {
            return _context.RequestLines.Any(e => e.Id == id);
        }

        //Get list of RequestLines by RequestId
        [HttpGet]
        [Route("RequestLines/{requestId}")]
        public async Task<List<RequestLine>> GetRequestLinesByRequestId(int requestId) {

                List<RequestLine> requestLines = await _context.RequestLines
                    .Include(r => r.Request).ThenInclude(Request => Request.User)
                    .Include(r => r.Product).ThenInclude(Product => Product.Vendor)
                    .Where(r => r.RequestId == requestId)
                    .ToListAsync();

                return requestLines;

    }
        private async void recalcRequestTotal(int requestId)
        {

            //get the total
            var total = await _context.RequestLines
                .Where(rl => rl.RequestId == requestId)
                .Include(rl => rl.Product)
                .Select(rl => new { linetotal = rl.Quantity * rl.Product.Price })
                .SumAsync(s => s.linetotal)
                ;
            //find the request
            var theRequest = await _context.Requests.FindAsync(requestId);

            //update the request
            theRequest.Total = total;

  
            //save changes()!

            try
            {
                await _context.SaveChangesAsync();


            }
            catch
            {

            }

            throw new NotImplementedException();

        }


    }
}
