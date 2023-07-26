using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliticalParties.BusinessLayer.Interfaces;
using PoliticalParties.BusinessLayer.ViewModels;
using PoliticalParties.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoliticalParties.Controllers
{
    [ApiController]
    public class PoliticalPartyController : ControllerBase
    {
        private readonly IPoliticalPartyServices _politicalPartyServices;

        public PoliticalPartyController(IPoliticalPartyServices politicalPartyServices)
        {
            _politicalPartyServices = politicalPartyServices;
        }

        #region PoliticalPartyRegion
        /// <summary>
        /// Create a new Political Party
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("parties")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterPoliticalPartyViewModel model)
        {
            var politicalPartyExists = await _politicalPartyServices.GetById(model.PoliticalPartyId);
            if (politicalPartyExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Political Party already exists!" });
            PoliticalParty politicalParty = new PoliticalParty()
            {

                Name = model.Name,
                Founder = model.Founder,
                IsDeleted = false
            };
            var result = await _politicalPartyServices.Create(politicalParty);
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Political Party creation failed! Please check details and try again." });

            return Ok(new Response { Status = "Success", Message = "Political Party created successfully!" });

        }

        /// <summary>
        /// Update a existing Political Party
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("parties/{id}")]
        public async Task<IActionResult> UpdatePoliticalParty(long id,[FromBody] RegisterPoliticalPartyViewModel model)
        {
            var politicalParty = await _politicalPartyServices.GetById(id);
            if (politicalParty == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"political Party With Id = {model.PoliticalPartyId} cannot be found" });
            }
            else
            {
                model.PoliticalPartyId = id;
                var result = await _politicalPartyServices.Update(model);
                return Ok(new Response { Status = "Success", Message = "Political Party Edited successfully!" });
            }
        }


        /// <summary>
        /// Delete a existing Political Party
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("parties/{id}")]
        public async Task<IActionResult> DeletePoliticalParty(long id)
        {
            var politicalParty = await _politicalPartyServices.GetById(id);
            if (politicalParty == null || politicalParty.IsDeleted == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Political Party With Id = {id} cannot be found" });
            }
            else
            {
                RegisterPoliticalPartyViewModel register = new RegisterPoliticalPartyViewModel();
                register.PoliticalPartyId = politicalParty.PoliticalPartyId;
                register.Name = politicalParty.Name;
                register.Founder = politicalParty.Founder;
                register.IsDeleted = true;
                var result = await _politicalPartyServices.Delete(register);
                return Ok(new Response { Status = "Success", Message = "Political Party deleted successfully!" });
            }
        }

        /// <summary>
        /// Get Political Party by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("parties/{id}")]
        public async Task<IActionResult> GetPoliticalPartyById(long id)
        {
            var politicalParty = await _politicalPartyServices.GetById(id);
            if (politicalParty == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Political Party With Id = {id} cannot be found" });
            }
            else
            {
                return Ok(politicalParty);
            }
        }

        /// <summary>
        /// Get Political Party by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("parties/searchParty")]
        public async Task<IActionResult> GetPoliticalPartyByName([FromQuery] string name)
        {
            var politicalParty = await _politicalPartyServices.GetByPartyName(name);
            if (politicalParty == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Political Party With Name = {name} cannot be found" });
            }
            else
            {
                return Ok(politicalParty);
            }
        }

        /// <summary>
        /// Get Political Party by Founder Name
        /// </summary>
        /// <param name="founderName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("parties/search")]
        public async Task<IActionResult> GetPoliticalPartyByFounderName([FromQuery] string founderName)
        {
            var politicalParty = await _politicalPartyServices.GetByFounderName(founderName);
            if (politicalParty == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                { Status = "Error", Message = $"Political Party With Founder Name = {founderName} cannot be found" });
            }
            else
            {
                return Ok(politicalParty);
            }
        }

        /// <summary>
        /// List All political Parties
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("parties")]
        public async Task<IEnumerable<PoliticalParty>> ListAllPoliticalParties()
        {
            return await _politicalPartyServices.GetAll();
        }
        #endregion
    }
}
