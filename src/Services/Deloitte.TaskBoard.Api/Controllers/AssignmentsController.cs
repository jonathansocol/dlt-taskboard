using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Deloitte.TaskBoard.Api.Models.Dtos;
using Deloitte.TaskBoard.Domain.Models;
using Deloitte.TaskBoard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Deloitte.TaskBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentRepository _repository;
        private readonly IMapper _mapper;

        public AssignmentsController(IAssignmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAll();
            var response = _mapper.Map<List<AssignmentDto>>(result);
            
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById([FromRoute] Guid id)
        {
            var result = await _repository.FindById(id);

            if (result == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<AssignmentDto>(result);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssignmentDto assignment)
        {
            var newAssignment = _mapper.Map<Assignment>(assignment);
            var result = await _repository.Create(newAssignment);

            return CreatedAtAction(nameof(FindById), new { id = result.Id }, _mapper.Map<AssignmentDto>(result));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAssignmentDto assignment)
        {
            var existingAssignment = await _repository.FindById(id);

            if (existingAssignment == null)
            {
                return NotFound();
            }

            MapUpdatedAssignment(assignment, existingAssignment);

            var result = await _repository.Update(existingAssignment);
            var response = _mapper.Map<AssignmentDto>(result);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _repository.Delete(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        private void MapUpdatedAssignment(UpdateAssignmentDto updatedAssignment, Assignment existingAssignment)
        {
            existingAssignment.Title = updatedAssignment.Title ?? existingAssignment.Title;
            existingAssignment.Description = updatedAssignment.Description ?? existingAssignment.Description;
            existingAssignment.Status = updatedAssignment.Status ?? existingAssignment.Status;
            existingAssignment.Priority = updatedAssignment.Priority ?? existingAssignment.Priority;
            existingAssignment.Requester = updatedAssignment.Requester ?? existingAssignment.Requester;
            existingAssignment.Order = updatedAssignment.Order ?? existingAssignment.Order;
        }
    }
}