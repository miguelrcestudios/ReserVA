using ReserVA;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ReserVA.Controllers
{
    public static class EspacioController
    {
        public static List<Espacio> ObtenerTodos()
        {
            List<Espacio> listaEspacios;
            using (var contexto = new ReserVAEntities())
            {
                listaEspacios = contexto.Espacio.Include(i => i.Recinto).ToList();
                
            }
            return listaEspacios;
        }
    }
}
