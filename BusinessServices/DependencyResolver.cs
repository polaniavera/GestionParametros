using System.ComponentModel.Composition;
using Resolver;

namespace BusinessServices
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IFormatoServices, FormatoServices>();
            registerComponent.RegisterType<IFormatoServicioServices, FormatoServicioServices>();
            registerComponent.RegisterType<ISectorServicioServices, SectorServicioServices>();
            registerComponent.RegisterType<INormaServices, NormaServices>();
            registerComponent.RegisterType<INormaSectorServices, NormaSectorServices>();
            registerComponent.RegisterType<IEntidadServices, EntidadServices>();
            registerComponent.RegisterType<ITablaServices, TablaServices>();
            registerComponent.RegisterType<ITablaValorServices, TablaValorServices>();
            registerComponent.RegisterType<IPeriodicidadServices, PeriodicidadServices>();
            registerComponent.RegisterType<IPlazoServices, PlazoServices>();
        }
    }
}
