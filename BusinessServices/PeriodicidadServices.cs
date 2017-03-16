using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class PeriodicidadServices : IPeriodicidadServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PeriodicidadServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all the Periodicidades
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PeriodicidadEntity> GetAllPeriodicidades()
        {
            var periodicidades = _unitOfWork.PeriodicidadRepository.GetAll().ToList();
            if (periodicidades.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PERIODICIDAD, PeriodicidadEntity>();
                });
                var periodicidadesModel = Mapper.Map<List<PERIODICIDAD>, List<PeriodicidadEntity>>(periodicidades);
                return periodicidadesModel;
            }
            return null;
        }
    }
}
