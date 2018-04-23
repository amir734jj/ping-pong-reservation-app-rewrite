using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using Domainlogic.Interfaces;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Models;
using Models.Models;

namespace DomainLogic
{
    public class ReservationDomainLogic: IReservationDomainLogic
    {
        private readonly IReservationDataAccessLayer _reservationDataAccessLayer;
        
        public ReservationDomainLogic(IReservationDataAccessLayer reservationDataAccessLayer)
        {
            _reservationDataAccessLayer = reservationDataAccessLayer;
        }

        public bool Update(int id, Reservation reservation)
        {
            return _reservationDataAccessLayer.Update(id, reservation);
        }

        public Reservation Get(int id)
        {
            return _reservationDataAccessLayer.Get(id);
        }

        public bool Save(Reservation pdfInfo)
        {
            return _reservationDataAccessLayer.Save(pdfInfo);
        }

        public bool Delete(int id)
        {
            return _reservationDataAccessLayer.Delete(id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _reservationDataAccessLayer.GetAll();
        }
    }
}