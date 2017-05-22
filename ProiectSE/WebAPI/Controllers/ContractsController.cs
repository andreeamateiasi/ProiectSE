﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ContractsController : ApiController
    {
        private OwnersAssociationContext db = new OwnersAssociationContext();

        // GET: api/Contracts
        public IQueryable<Contract> GetContracts()
        {
            return db.Contracts;
        }

        // GET: api/Contracts/5
        [ResponseType(typeof(Contract))]
        public IHttpActionResult GetContract(int id)
        {
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        // PUT: api/Contracts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContract(int id, [FromBody]Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contract.ContractId)
            {
                return BadRequest();
            }

            //db.Entry(contract).State = EntityState.Modified;
            Contract c = db.Contracts.Find(contract.ContractId);
            c.ContractPeriod = contract.ContractPeriod;
            c.Cost = contract.Cost;
            c.ServicesFacilitiesOffered = contract.ServicesFacilitiesOffered;
            c.Supplier = contract.Supplier;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        [ResponseType(typeof(Contract))]
        public IHttpActionResult PostContract(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contracts.Add(contract);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contract.ContractId }, contract);
        }

        // DELETE: api/Contracts/5
        [ResponseType(typeof(Contract))]
        public IHttpActionResult DeleteContract(int id)
        {
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return NotFound();
            }

            db.Contracts.Remove(contract);
            db.SaveChanges();

            return Ok(contract);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContractExists(int id)
        {
            return db.Contracts.Count(e => e.ContractId == id) > 0;
        }
    }
}