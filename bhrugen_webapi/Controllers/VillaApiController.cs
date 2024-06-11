using System.Security.Cryptography;
using AutoMapper;
using bhrugen_webapi.Data;
using bhrugen_webapi.Models;
using bhrugen_webapi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace bhrugen_webapi.Controllers;
[Route("api/VillaApi")]
[ApiController]
public class VillaApiController:ControllerBase
{
    private readonly ApplicationDBContext _DB;
    //private readonly IMapper _mapper;
    public VillaApiController(ApplicationDBContext db /*,IMapper mapper*/)
    {
        _DB = db;
        /*_mapper = mapper;*/
    }

    [HttpGet]
    /*[Authorize]*/
    public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
    {
        return  Ok(await _DB.Villas.ToListAsync());
    }

    //the id and route name(implicity for CreatedAtRoute in createvilla function)
    [HttpGet("{id:int}",Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VillaDTO>> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest("There no record with id =0");
        }
        var villa =await _DB.Villas.FirstOrDefaultAsync(x => x.Id == id);
        if (villa == null)
        {
            return NotFound($"There no record with id = {id}");
        }
        return Ok(villa);
    }
    
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villadto)
    {
        #region Modelvalidation

        /*if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }*/
        

        #endregion
        
        if (villadto == null)
        {
            return BadRequest(villadto);
        }
       
        Villa model = new()
        {
            //Id = _DB.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1,
            Name = villadto.Name,
            Details = villadto.Details,
            Amenity = villadto.Amenity,
            Occupancy = villadto.Occupancy,
            Rate = villadto.Rate,
            Sqft = villadto.Sqft,
            ImageUrl = villadto.ImageUrl,
            CreatedDate = DateTime.Now
        };
        await _DB.Villas.AddAsync(model);
        await _DB.SaveChangesAsync();
        return CreatedAtRoute("GetVilla",new{id=model.Id},model);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest("the is no record with id = 0");
        }

        var villa =await _DB.Villas.FirstOrDefaultAsync(v => v.Id == id);
        if (villa == null)
        {
            return NotFound($"There is no villa with id ={id}");
        }
        _DB.Villas.Remove(villa);
        await _DB.SaveChangesAsync();
        return Ok("Villa Deleted successfully");
    }
    
    
    
    [HttpPut("{id:int}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateVilla(int id,[FromBody]VillaUpdateDTO villadto)
    {
        if (villadto==null || id != villadto.Id)
        {
            return BadRequest("Check the Model");
        }

        var villa =await _DB.Villas.FirstOrDefaultAsync(v => v.Id == id);
        if (villa == null)
        {
            return NotFound($"There is no villa Number with Number ={id}");
        }
        villa.Name = villadto.Name;
        villa.Details = villadto.Details;
        villa.Amenity = villadto.Amenity;
        villa.Occupancy = villadto.Occupancy;
        villa.Rate = villadto.Rate;
        villa.Sqft = villadto.Sqft;
        villa.ImageUrl = villadto.ImageUrl;
        villa.UpdatedDate = DateTime.Now;
        await _DB.SaveChangesAsync();
        return Ok($"Villa Updated Successfully");
    }

    [HttpPut("UpdateVillaName/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateByVillaName(int id,[FromBody]VillaUpdateNameDTO villadto)
    {
        if (villadto==null || id != villadto.Id)
        {
            return BadRequest("Check the Model");
        }

        var villa =await _DB.Villas.FirstOrDefaultAsync(v => v.Id == id);
        if (villa == null)
        { return NotFound($"There is no villa Number with Number ={id}"); }
        villa.Name = villadto.Name;
        await _DB.SaveChangesAsync();
        return Ok($"Villa Name Updated Successfully");
    }
    
    
    
    #region Patch

    // [HttpPatch("{id:int}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)] 
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public ActionResult<VillaDTO> UpdatePartialvilla(int id,[FromBody]JsonPatchDocument<VillaUpdateDTO> patchdto)
    // {
    //     if (patchdto==null || id == 0)
    //     {
    //         return BadRequest();
    //     }
    //     var villa = _DB.Villas.FirstOrDefault(x => x.Id == id);
    //     if (villa == null)
    //     {
    //         return NotFound($"There no record with id = {id}");
    //     }
    //     // patchdto.ApplyTo(villa, ModelState);
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     _DB.SaveChanges();
    //     return Ok($"Villa Updated Successfully");
    // }

    #endregion
    

}
