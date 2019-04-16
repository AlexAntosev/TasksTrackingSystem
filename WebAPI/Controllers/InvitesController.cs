using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [Authorize]
    public class InvitesController : ApiController
    {
        private readonly IInviteService _service;
        private readonly IMapper _mapper;

        public InvitesController(IInviteService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> CreateInviteAsync(InviteDTO invite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The invite is not created. The invite creating model is incorrectly filled.");
            }
            Invite createdInvite = await _service.CreateInviteAsync(invite);
            InviteDTO createdInviteDTO = _mapper.Map<Invite, InviteDTO>(createdInvite);

            return Created(Url.Request.RequestUri, createdInviteDTO);
        }
        

        [HttpGet]
        [Route("api/Projects/{projectId}/Invites")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllInvitesByProjectIdAsync(int projectId)
        {
            List<Invite> invites = await _service.GetAllInvitesByProjectIdAsync(projectId);
            if (invites == null)
            {
                return NotFound();
            }

            var invitesDTO = _mapper.Map<List<Invite>, List<InviteDTO>>(invites);
            return Ok(invitesDTO);
        }

        [HttpGet]
        [Route("api/Users/{userId}/Invites")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetAllInvitesByReceiverIdAsync(int userId)
        {
            List<Invite> invites = await _service.GetAllInvitesByReceiverIdAsync(userId);
            if (invites == null)
            {
                return NotFound();
            }

            var invitesDTO = _mapper.Map<List<Invite>, List<InviteDTO>>(invites);
            return Ok(invitesDTO);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetInviteByIdAsync(int id)
        {
            Invite invite = await _service.GetInviteByIdAsync(id);
            if (invite == null)
            {
                return NotFound();
            }

            InviteDTO inviteDTO = _mapper.Map<Invite, InviteDTO>(invite);
            return Ok(inviteDTO);
        }
        
        [HttpDelete]
        public async System.Threading.Tasks.Task<IHttpActionResult> DeleteInviteAsync(int id)
        {
            Invite currentInvite = await _service.GetInviteByIdAsync(id);
            if (currentInvite == null)
            {
                return NotFound();
            }

            await _service.DeleteInviteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
