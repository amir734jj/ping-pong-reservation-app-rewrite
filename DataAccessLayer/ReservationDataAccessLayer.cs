using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DataAccessLayer.Interfaces;
using Models.Models;
using Models.RawModels;

namespace DataAccessLayer
{
    public class ReservationDataAccessLayer:  IReservationDataAccessLayer
    {
        private readonly EntityDbContext _database;
        
        private readonly IMapper _mapper;

        public ReservationDataAccessLayer(IMapper mapper, EntityDbContext database)
        {
            _mapper = mapper;
            _database = database;
        }

        public Reservation Get(int id)
        {
            return _mapper.Map<Reservation>(_database.RawPdfInfos.Find(id));
        }

        public bool Save(Reservation reservation)
        {
            _database.RawPdfInfos.Add(_mapper.Map<RawReservation>(reservation));

            _database.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var rawPdfInfo = _database.RawPdfInfos.FirstOrDefault(x => x.Id == id);

            if (rawPdfInfo == null) return false;

            _database.Remove(rawPdfInfo);

            _database.SaveChanges();

            return true;
        }

        public bool Update(int id, Reservation reservation)
        {
            var rawPdfInfo = _database.RawPdfInfos.FirstOrDefault(x => x.Id == id);

            if (rawPdfInfo == null) return false;
            
            _database.RawPdfInfos.Update(_mapper.Map<RawReservation>(reservation));

            _database.SaveChanges();

            return true;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _mapper.Map<IEnumerable<Reservation>>(_database.RawPdfInfos.ToList());
        }
    }
}