
namespace BusinessServices
{
    public class CodigoError
    {
        public string Error(string msj)
        {
            if (msj.Contains("SQL"))
            {
                return "0100";
            }
            else if (msj.Contains("Timeout"))
            {
                return "0200";
            }
            else if (msj.Contains("Object reference not set to an instance") || msj.Contains("System.Linq.Enumerable.First") || msj.Contains("with the REFERENCE constraint"))
            {
                return "0400";
            }
            //Norma no encontrada
            else if (msj.Contains("null"))
            {
                return "0300 Objeto no encontrado";
            }
            return "0900";
        }
    }

}
