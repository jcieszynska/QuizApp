using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class QuizController : ApiController
    {
        [HttpGet]
        [Route("api/Questions")]

        public HttpResponseMessage GetQuestions(){
            using (DBModel db = new DBModel())
            {
                //get 10 random qs
                var Qns = db.Questions
                    .Select(x => new { QnID = x.QnID, Qn = x.Qn, ImageName = x.ImageName, x.Option1, x.Option2, x.Option3, x.Option4 })
                    .OrderBy(y => Guid.NewGuid())
                    .Take(10)
                    .ToArray();
                var updated = Qns.AsEnumerable()
                    .Select(x => new
                    {
                        QnID = x.QnID,
                        Qn = x.Qn,
                        ImageName = x.ImageName,
                        Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                    }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updated);

            }
        }

        [HttpPost]
        [Route("api/Answers")]
        //qIDs-10qs from previous method
        //int array of answers
        public HttpResponseMessage GetAnswer(int[] qIDs) {
            using (DBModel db = new DBModel())
            {
                var result = db.Questions
                    .AsEnumerable()
                    .Where(y => qIDs.Contains(y.QnID))
                    .OrderBy(x => { return Array.IndexOf(qIDs, x.QnID); }) //to maintain question order
                    .Select(z => z.Answer)
                    .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result);

            }


        }

    }
}
