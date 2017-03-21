#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using DataModel.GenericRepository;

#endregion

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private member variables...

        private WebApiDbEntities _context = null;
        private GenericRepository<TABLA> _tablaRepository;
        private GenericRepository<TABLA_CAMPO> _tablaCampoRepository;
        private GenericRepository<TABLA_VALOR> _tablaValorRepository;
        private GenericRepository<ENTIDAD> _entidadRepository;
        private GenericRepository<FORMATO> _formatoRepository;
        private GenericRepository<SECTOR_SERVICIO> _sectorServicioRepository;
        private GenericRepository<NORMA> _normaRepository;
        private GenericRepository<NORMA_SECTOR> _normaSectorRepository;
        private GenericRepository<PERIODICIDAD> _periodicidadRepository;
        private GenericRepository<PLAZO> _plazoRepository;
        private NormaRepositoryCustom _normaRepositoryCustom;
        private NormaSectorRepositoryCustom _normaSectorRepositoryCustom;
        private TablaRepositoryCustom _tablaRepositoryCustom;
        private FormatoRepositoryCustom _formatoRepositoryCustom;
        private FormatoServicioRepositoryCustom _formatoServicioRepositoryCustom;
        #endregion

        public UnitOfWork()
        {
            _context = new WebApiDbEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for tabla repository.
        /// </summary>
        public GenericRepository<TABLA> TablaRepository
        {
            get
            {
                if (this._tablaRepository == null)
                    this._tablaRepository = new GenericRepository<TABLA>(_context);
                return _tablaRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for tabla repository  custom.
        /// </summary>
        public TablaRepositoryCustom TablaRepositoryCustom
        {
            get
            {
                if (this._tablaRepositoryCustom == null)
                    this._tablaRepositoryCustom = new TablaRepositoryCustom(_context);
                return _tablaRepositoryCustom;
            }
        }

        /// <summary>
        /// Get/Set Property for tabla_campo repository.
        /// </summary>
        public GenericRepository<TABLA_CAMPO> TablaCampoRepository
        {
            get
            {
                if (this._tablaCampoRepository == null)
                    this._tablaCampoRepository = new GenericRepository<TABLA_CAMPO>(_context);
                return _tablaCampoRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for tabla_valor repository.
        /// </summary>
        public GenericRepository<TABLA_VALOR> TablaValorRepository
        {
            get
            {
                if (this._tablaValorRepository == null)
                    this._tablaValorRepository = new GenericRepository<TABLA_VALOR>(_context);
                return _tablaValorRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Entidad repository.
        /// </summary>
        public GenericRepository<ENTIDAD> EntidadRepository
        {
            get
            {
                if (this._entidadRepository == null)
                    this._entidadRepository = new GenericRepository<ENTIDAD>(_context);
                return _entidadRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for Formato repository.
        /// </summary>
        public GenericRepository<FORMATO> FormatoRepository
        {
            get
            {
                if (this._formatoRepository == null)
                    this._formatoRepository = new GenericRepository<FORMATO>(_context);
                return _formatoRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for sectorServicio repository.
        /// </summary>
        public GenericRepository<SECTOR_SERVICIO> SectorServicioRepository
        {
            get
            {
                if (this._sectorServicioRepository == null)
                    this._sectorServicioRepository = new GenericRepository<SECTOR_SERVICIO>(_context);
                return _sectorServicioRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for norma repository.
        /// </summary>
        public GenericRepository<NORMA> NormaRepository
        {
            get
            {
                if (this._normaRepository == null)
                    this._normaRepository = new GenericRepository<NORMA>(_context);
                return _normaRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for norma repository  custom.
        /// </summary>
        public NormaRepositoryCustom NormaRepositoryCustom
        {
            get
            {
                if (this._normaRepositoryCustom == null)
                    this._normaRepositoryCustom = new NormaRepositoryCustom(_context);
                return _normaRepositoryCustom;
            }
        }

        /// <summary>
        /// Get/Set Property for normaSector repository custom.
        /// </summary>
        public NormaSectorRepositoryCustom NormaSectorRepositoryCustom
        {
            get
            {
                if (this._normaSectorRepositoryCustom == null)
                    this._normaSectorRepositoryCustom = new NormaSectorRepositoryCustom(_context);
                return _normaSectorRepositoryCustom;
            }
        }

        /// <summary>
        /// Get/Set Property for normaSector repository.
        /// </summary>
        public GenericRepository<NORMA_SECTOR> NormaSectorRepository
        {
            get
            {
                if (this._normaSectorRepository == null)
                    this._normaSectorRepository = new GenericRepository<NORMA_SECTOR>(_context);
                return _normaSectorRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for periodicidad repository.
        /// </summary>
        public GenericRepository<PERIODICIDAD> PeriodicidadRepository
        {
            get
            {
                if (this._periodicidadRepository == null)
                    this._periodicidadRepository = new GenericRepository<PERIODICIDAD>(_context);
                return _periodicidadRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for plazo repository.
        /// </summary>
        public GenericRepository<PLAZO> PlazoRepository
        {
            get
            {
                if (this._plazoRepository == null)
                    this._plazoRepository = new GenericRepository<PLAZO>(_context);
                return _plazoRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for formato repository custom.
        /// </summary>
        public FormatoRepositoryCustom FormatoRepositoryCustom
        {
            get
            {
                if (this._formatoRepositoryCustom == null)
                    this._formatoRepositoryCustom = new FormatoRepositoryCustom(_context);
                return _formatoRepositoryCustom;
            }
        }

        /// <summary>
        /// Get/Set Property for formatoServicio repository custom.
        /// </summary>
        public FormatoServicioRepositoryCustom FormatoServicioRepositoryCustom
        {
            get
            {
                if (this._formatoServicioRepositoryCustom == null)
                    this._formatoServicioRepositoryCustom = new FormatoServicioRepositoryCustom(_context);
                return _formatoServicioRepositoryCustom;
            }
        }

        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}