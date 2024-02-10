using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Constants
{
    public static class Messages
    {
        //Error const
        public const string ENTITY_UNFOUND = "¡No se encontro registro en la base de Datos!";
        public const string INVOICE_UNFOUND = "¡No se encontro Factura en la base de Datos!";
        public const string CLIENT_UNFOUND = "¡No se encontro Cliente en la base de Datos!";
        public const string PROD_UNFOUND = "¡No se encontro Producto en la base de Datos!";
        public const string ENTITY_ERROR_VALIDATE = "¡Se detectaron datos invalidos!";

        //Success const
        public const string CREATE_SUCCESS = "¡Registro creado con exito!";
                    

        //Common const
        public const string CUIT_EXISTS = "El Cuit ingresado ya existe en el sistema";
        public const string ENTITY_INSERT = "Insertando Nuevo Registro";
        public const string ENTITY_INSERTED = "¡Registro Guardado con Exito!";
        public const string ENTITY_UPDATE= "Actualizando Registro";
        public const string ENTITY_UPDATED= "¡Registro Actualizado con Exito!";
        public const string ENTITY_DELETE= "Eliminando Registro";
        public const string ENTITY_DELETE_PERMANETLY= "Eliminando Registro Permanentemente";
        public const string ENTITY_DELETED= "¡Registro Eliminado con Exito!";
        public const string ENTITY_DELETED_PERMANETLY= "¡Registro Eliminado con Exito! (Borrado Permanente)";

    }
}
