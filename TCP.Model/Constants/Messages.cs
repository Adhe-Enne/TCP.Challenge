using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Constants
{
    public static class Messages
    {
        //Error
        public static string ENTITY_UNFOUND = "¡No se encontro registro en la base de Datos!";
        public static string ENTITY_ERROR_VALIDATE = "¡Se detectaron valores invalidos!";

        //Success
        public static string CREATE_SUCCESS = "¡Registro creado con exito!";


        //Common
        public static string CUIT_EXISTS = "El Cuit ingresado ya existe en el sistema";
        public static string ENTITY_INSERT = "Insertando Nuevo Registro";
        public static string ENTITY_UPDATE= "Actualizando Registro";
        public static string ENTITY_UPDATED= "¡Registro Actualizado con Exito!";
        public static string ENTITY_DELETE= "Eliminando Registro";
        public static string ENTITY_DELETE_PERMANETLY= "Eliminando Registro Permanentemente";
        public static string ENTITY_DELETED= "¡Registro Eliminado con Exito!";
        public static string ENTITY_DELETED_PERMANETLY= "¡Registro Eliminado con Exito! (Borrado Permanente)";

    }
}
