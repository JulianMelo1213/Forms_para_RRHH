//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Formulario_MinisterioAgri
{
    using System;
    using System.Collections.Generic;
    
    public partial class Solicitud_nombramiento
    {
        public int Id_solicitud { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Id_Departamento { get; set; }
        public Nullable<int> Id_Cargo { get; set; }
        public string Grupo_ocupacional { get; set; }
        public string Sustitucion { get; set; }
        public decimal Salario { get; set; }
        public string Direccion_de_residencia { get; set; }
        public string Autorizado { get; set; }
        public string Observaciones { get; set; }
        public string Enviado_en_oficialNo { get; set; }
        public string Pregunta { get; set; }
        public string NoOficioyFecha { get; set; }
        public byte[] Archivo_Cedula_identidad { get; set; }
        public string Extension_Cedula_identidad { get; set; }
        public byte[] Archivo_HojaVida_identidad { get; set; }
        public string Extension_HojaVida_identidad { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
