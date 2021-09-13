using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Personaldetails.Models;

namespace Personaldetails.Controllers
{
    public class PersonaldetailsController : ApiController
    {
        private dbEntities db = new dbEntities();

        // GET: api/Personaldetails
        public IQueryable<details> Getdetails()
        {
            return db.details;
        }

        // GET: api/Personaldetails/5
        [ResponseType(typeof(details))]
        public IHttpActionResult Getdetails(long id)
        {
            details details = db.details.Find(id);
            if (details == null)
            {
                return NotFound();
            }

            return Ok(details);
        }

        // PUT: api/Personaldetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdetails(long id, details details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != details.Id)
            {
                return BadRequest();
            }

            db.Entry(details).State = EntityState.Modified;
            //details.IsActive = true;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detailsExists(id))
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

        // POST: api/Personaldetails
        [ResponseType(typeof(details))]
        public IHttpActionResult Postdetails(details details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.details.Add(details);
            //details.IsActive = true;
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = details.Id }, details);
        }

        // DELETE: api/Personaldetails/5
        [ResponseType(typeof(details))]
        public IHttpActionResult Deletedetails(long id)
        {
            details details = db.details.Find(id);
            if (details == null)
            {
                return NotFound();
            }

            db.details.Remove(details);
            db.SaveChanges();

            return Ok(details);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detailsExists(long id)
        {
            return db.details.Count(e => e.Id == id) > 0;
        }
    }
}