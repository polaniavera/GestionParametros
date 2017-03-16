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
    public class PlazoServices : IPlazoServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PlazoServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all the Periodicidades
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlazoEntity> GetAllPlazos()
        {
            var plazos = _unitOfWork.PlazoRepository.GetAll().ToList();
            if (plazos.Any())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PLAZO, PlazoEntity>();
                });
                var plazosModel = Mapper.Map<List<PLAZO>, List<PlazoEntity>>(plazos);
                return plazosModel;
            }
            return null;
        }
    }
}
