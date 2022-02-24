using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogue.Business.Implementaion;
using ProductCatalogue.Common;
using ProductCatalogue.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace ProductCatalogue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        // GET: api/<CategoriesController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.Get();
            return Ok(list.Data);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id is null)
                return BadRequest(new Result<IActionResult>(false, ResultConstant.IdNotNull));
            var data = await _service.GetById((int)id);
            if (data != null)
                return Ok(data.Data);
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordNotFound));
        }

        // GET api/<PlayerController>?<id>=2
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var data = await _service.GetById((int)id);
            if (data != null)
                return Ok(data.Data);
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordNotFound));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Post([FromBody] CategoryModel model)
        {
            var data = await _service.Add(model);
            if (data.IsSuccess)
                return StatusCode(201);
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordCreateNotSuccessfully));
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryModel model)
        {

            model.Id = id;
            var data = await _service.Update(model);
            if (data.IsSuccess)
                return Ok(new Result<IActionResult>(true, ResultConstant.RecordUpdateSuccessfully));
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordUpdateNotSuccessfully));


        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest(new Result<IActionResult>(false, ResultConstant.IdNotNull));
            var data = await _service.Delete((int)id);
            if (data.IsSuccess)
                return Ok(new Result<IActionResult>(true, ResultConstant.RecordRemoveSuccessfully));
            else
                return BadRequest(new Result<IActionResult>(false, ResultConstant.RecordRemoveNotSuccessfully));
        }
    }
}
