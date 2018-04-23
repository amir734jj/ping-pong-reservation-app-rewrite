using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models;
using Models.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IReservationDataAccessLayer
    {
        Reservation Get(int id);

        bool Save(Reservation pdfInfo);
        
        bool Delete(int id);

        bool Update(int id, Reservation pdfInfo);

        IEnumerable<Reservation> GetAll();
    }
}