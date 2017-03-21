using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class FormatoServicioServices : IFormatoServicioServices
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor to initialize UnitOfWork instance
        /// with Unity Constructor Inject Dependency
        /// </summary>
        public FormatoServicioServices(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool ExistSectorFormato(int sectorId)
        {
            bool exist = false;
            exist = _unitOfWork.FormatoServicioRepositoryCustom.ExistServicio(sectorId);

            if (exist)
                return true;
            else
                return false;
        }
    }
}
