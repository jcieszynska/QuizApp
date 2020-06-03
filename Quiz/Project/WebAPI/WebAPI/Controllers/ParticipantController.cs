using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ParticipantController : ApiController
    {
        //method updating user
        [HttpPost]
        [Route("api/InsertParticipant")]

        public Participant Insert(Participant model){
            using (DBModel db = new DBModel())
            {
                db.Participant.Add(model);
                db.SaveChanges();
                return model;
            }
        }

        //score&time
        [HttpPost]
        [Route("api/UpdateOutput")]
        public void UpdateOutput(Participant model) {
            using (DBModel db = new DBModel())
            {
                db.Entry(model).State = System.Data.EntityState.Modified;
                db.SaveChanges();
              
            }

        }

    }
}
