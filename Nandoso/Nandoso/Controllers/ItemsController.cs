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
    public class ItemsController : ApiController
    {
        private NandosoContext db = new NandosoContext();

        // GET: api/Items
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        // Since this method can edit the database, it requires authorisation.
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item, string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ID)
            {
                return BadRequest();
            }

            foreach (Admin a in db.Admins.ToList())
            {
                if (a.username.Equals(username))
                {
                    if (a.password.Equals(password))
                    {
                        db.Entry(item).State = EntityState.Modified;

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!ItemExists(id))
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

        // POST: api/Items
        // Since this method can edit the database, it requires authorisation.
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item, string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (Admin a in db.Admins.ToList())
            {
                if (a.username.Equals(username))
                {
                    if (a.password.Equals(password))
                    {
                        db.Items.Add(item);
                        db.SaveChanges();

                        return CreatedAtRoute("DefaultApi", new { id = item.ID }, item);
                    }

                    // If the password is incorrect for this username,
                    // it will not be correct for other usernames either.
                    break;
                }
            }
            return StatusCode(HttpStatusCode.Unauthorized);
        }

        // DELETE: api/Items/5
        // Since this method can edit the database, it requires authorisation.
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id, string username, string password)
        {
            foreach (Admin a in db.Admins.ToList())
            {
                if (a.username.Equals(username))
                {
                    if (a.password.Equals(password))
                    {
                        Item item = db.Items.Find(id);
                        if (item == null)
                        {
                            return NotFound();
                        }

                        db.Items.Remove(item);
                        db.SaveChanges();

                        return Ok(item);
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

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ID == id) > 0;
        }
    }
}