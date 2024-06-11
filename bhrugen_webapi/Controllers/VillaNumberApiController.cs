using bhrugen_webapi.Data;
using bhrugen_webapi.Models;
using bhrugen_webapi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bhrugen_webapi.Controllers;

[Route("api/VillaNumberApi")]
[ApiController]
public class VillaNumberApiController:ControllerBase
{
    private readonly ApplicationDBContext _DB;
    
    
    public VillaNumberApiController(ApplicationDBContext db)
    {
        _DB = db;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VillaNumberDTO>>> GetvillaNumbers()
    {
        return Ok(await _DB.VillaNumbers.ToListAsync());
    }
    
    [HttpGet("{id:int}",Name = "GetVillaNumber")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VillaNumberDTO>> GetvillaNumber(int id)
    {
        if (id == 0)
        {
            return BadRequest("Check the Id Value");
        }

        var villaNumber =await _DB.VillaNumbers.FirstOrDefaultAsync(v => v.VillaNo == id);
        if (villaNumber == null)
        {
            return NotFound($"There is no villa with id ={id}");
        }
        return Ok(villaNumber);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<VillaNumberDTO>> CreateVillaNumber([FromBody]VillaNumberCreateDTO ModelDTO)
    {
        if (ModelState.IsValid != true)
        {
            return BadRequest("Check the Model Values");
        }
       
        //to insure not repetition villa number
        if (await _DB.VillaNumbers.FirstOrDefaultAsync(v => v.VillaNo == ModelDTO.VillaNo)!=null)
        {
            ModelState.AddModelError("customerror", "villa Number already exists");
            return BadRequest(ModelState);
        }
            

        #region this region is for check if the villaid is existing or not (the ForeignKey)

        var villa =await _DB.Villas.FirstOrDefaultAsync(v => v.Id == ModelDTO.VillaId);
        if (villa == null)
        {
            return BadRequest($"The is no Villa with id {ModelDTO.VillaId}");
        }

        #endregion
        
        
        var model = new VillaNumber
        {
           VillaNo = ModelDTO.VillaNo,
           SpecialDetails = ModelDTO.SpecialDetails,
           VillaId = ModelDTO.VillaId,
           CreatedDate = DateTime.Now
        };
        await  _DB.VillaNumbers.AddAsync(model);
        await _DB.SaveChangesAsync();
        return CreatedAtRoute("GetVilla",new{id=model.VillaNo},model);
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteVillaNumber(int id)
    {
        if ( id == 0)
        {
            return BadRequest("Check the Id Value");
        }

        var villaNumber =await _DB.VillaNumbers.FirstOrDefaultAsync(v => v.VillaNo == id);
        if (villaNumber == null)
        {
            return NotFound($"There is no villa with id ={id}");
        }

        _DB.VillaNumbers.Remove(villaNumber);
        await _DB.SaveChangesAsync();
        return Ok("Villa Number Deleted successfully");
    }
    
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateVillaNo(int villano,[FromBody]VillaNumberUpdateDTO ModelDTO)
    {
        if ( villano != ModelDTO.VillaNo)
        {
            return BadRequest("Check the Model");
        }
        #region this region is for check if the villaid is exit or not (the ForeignKey)

        var villa =await _DB.Villas.FirstOrDefaultAsync(v => v.Id == ModelDTO.VillaId);
        if (villa == null)
        {
            return BadRequest($"The is no Villa with id {ModelDTO.VillaId}");
        }

        #endregion
        var model =await _DB.VillaNumbers.FirstOrDefaultAsync(v => v.VillaNo == villano);
        if (model == null)
        {
            return NotFound($"There is no villa Number with Number ={villano}");
        }
        model.VillaNo = ModelDTO.VillaNo;
        model.SpecialDetails = ModelDTO.SpecialDetails;
        model.UpdatedDate = DateTime.Now;
        model.VillaId = ModelDTO.VillaId;
        await _DB.SaveChangesAsync();
        return Ok("Model Updated successfully");
    }
}