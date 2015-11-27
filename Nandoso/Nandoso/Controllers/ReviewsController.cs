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
using Nandoso.Models;

namespace Nandoso.Controllers
{
    public class ReviewsController : ApiController
    {
        private NandosoContext db = new NandosoContext();

        // GET: api/Reviews
        public IQueryable<Review> GetReviews()
        {
            return db.Reviews;
        }

        // GET: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult GetReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReview(int id, Review review, string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.ID)
            {
                return BadRequest();
            }

            foreach (Admin a in db.Admins.ToList())
            {
                if (a.username.Equals(username))
                {
                    if (a.password.Equals(password))
                    {
                        db.Entry(review).State = EntityState.Modified;

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ReviewExists(id))
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

                    // If the password is incorrect for this username,
                    // it will not be correct for other usernames either.
                    break;
                }
            }
            return StatusCode(HttpStatusCode.Unauthorized);
        }

        // POST: api/Reviews
        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reviews.Add(review);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = review.ID }, review);
        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public IHttpActionResult DeleteReview(int id, string username, string password)
        {foreach (Admin a in db.Admins.ToList())
            {
                if (a.username.Equals(username))
                {
                    if (a.password.Equals(password))
                    {
                        Review review = db.Reviews.Find(id);
                        if (review == null)
                        {
                            return NotFound();
                        }

                        db.Reviews.Remove(review);
                        db.SaveChanges();

                        return Ok(review);
                    }

                    // If the password is incorrect for this username,
                    // it will not be correct for other usernames either.
                    break;
                }
            }
        return StatusCode(HttpStatusCode.Unauthorized);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.ID == id) > 0;
        }
    }
}