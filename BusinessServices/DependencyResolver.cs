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
            registerComponent.RegisterType<ISectorServicioServices, SectorServicioServices>();
            registerComponent.RegisterType<INormaServices, NormaServices>();
            registerComponent.RegisterType<INormaSectorServices, NormaSectorServices>();
            registerComponent.RegisterType<IEntidadServices, EntidadServices>();
            registerComponent.RegisterType<ITablaServices, TablaServices>();
        }
    }
}
