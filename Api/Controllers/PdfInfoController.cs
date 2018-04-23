using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PdfInfoController : Controller
    {
        private readonly IReservationDataAccessLayer _reservationDataAccessLayer;

        public PdfInfoController(IReservationDataAccessLayer reservationDataAccessLayer)
        {
            _reservationDataAccessLayer = reservationDataAccessLayer;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return _reservationDataAccessLayer.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return _reservationDataAccessLayer.Get(id);
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody] Reservation reservation)
        {
            return _reservationDataAccessLayer.Save(reservation);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] Reservation reservation)
        {
            return _reservationDataAccessLayer.Update(id, reservation);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _reservationDataAccessLayer.Delete(id);
        }
    }
}