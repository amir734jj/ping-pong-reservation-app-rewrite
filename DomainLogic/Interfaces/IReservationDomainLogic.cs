using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models;
using Models.Models;

namespace Domainlogic.Interfaces
{
    public interface IReservationDomainLogic
    {
        bool Update(int id, Reservation reservation);

        Reservation Get(int id);

        bool Save(Reservation reservation);
        
        bool Delete(int id);

        IEnumerable<Reservation> GetAll();
    }
}