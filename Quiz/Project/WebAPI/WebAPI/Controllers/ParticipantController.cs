using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ParticipantController : ApiController
    {
        private DBModel db = new DBModel();

        [HttpPost]
        [Route("api/InsertParticipant")]
        public Participant Insert(Participant model){
            
                try
                {
                    db.Participant.Add(model);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return model;
            
        }

        [HttpGet]
        [Route("api/participant/{id}")]
        public IHttpActionResult GetParticipant (int id)
        {

            Participant participant = db.Participant.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        [HttpPut]
        [Route("api/participant/{id}")]
        public IHttpActionResult UpdateParticipant(int id, Participant participant)
        {       
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != participant.ParticipantID)
            {
                return BadRequest();
            }

            db.Entry(participant).State = System.Data.EntityState.Modified;
            try
            {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool ParticipantExists(int id)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [ResponseType(typeof(Participant))] 
        [Route("api/removeParticipant/{id}")]
        public IHttpActionResult DeleteParticipant(int id)
        {
            Participant participant = db.Participant.Find(id);
            if (participant == null)
            {
                return NotFound();
            }
            db.Participant.Remove(participant);
            db.SaveChanges();

            return Ok(participant);

        }

        [HttpPost]
        [Route("api/UpdateOutput")]
        public void UpdateOutput(Participant model) {
            using (DBModel db = new DBModel())
            {
                try
                {
                    db.Entry(model).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
              
            }

        }

    }
}
