using System.Resources;

namespace ProyectoPruebasEntityFrameworkCore.Helpers
{
    public static class MessageManager
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("ProyectoPruebasEntityFrameworkCore.Messages", typeof(MessageManager).Assembly);
        public static string GetPersonasObtenidasCorrectamente()
        {
             return _resourceManager.GetString("PersonasObtenidasCorrectamente");
        }

        public static string GetErrorObtenerPersonas()
        {
            return _resourceManager.GetString("ErrorAlObtenerPersonas");
        }


        public static string GetPersonasEliminadasCorrectamente()
        {
            return _resourceManager.GetString("PersonasEliminadasCorrectamente");
        }

        public static string GetPersonaObtenidaCorrectamente()
        {
            return _resourceManager.GetString("PersonaObtenidaCorrectamente");
        }

        public static string GetPersonaCreadaCorrectamente()
        {
            return _resourceManager.GetString("PersonaCreadaCorrectamente");
        }

        public static string GetErrorAlBuscarPersonas()
        {
            return _resourceManager.GetString("ErrorAlBuscarPersonas");
        }

        public static string GetOcurrioUnError()
        {
            return _resourceManager.GetString("OcurrioUnError");
        }

        public static string GetNoExistePersona()
        {
            return _resourceManager.GetString("NoExistePersona");
        }

        public static string GetDatosRecibidosIncorrectos()
        {
            return _resourceManager.GetString("NoExistePersona");
        }

        public static string GetErrorAlCrearLaPersona()
        {
            return _resourceManager.GetString("ErrorAlCrearLaPersona");
        }

        public static string GetPersonaActualizadaCorrectamente()
        {
            return _resourceManager.GetString("PersonaActualizadaCorrectamente");
        }

        public static string GetErrorAlActualizarLaPersona()
        {
            return _resourceManager.GetString("ErrorAlActualizarLaPersona");
        }




    }
}
