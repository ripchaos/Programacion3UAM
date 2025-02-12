using System;

namespace InvTareas.Datos
{
    public class TareaConFecha : Tarea
    {
        public DateTime FechaVencimiento { get; set; }

        public TareaConFecha(string descripcion, DateTime fechaVencimiento)
            : base(descripcion)
        {
            FechaVencimiento = fechaVencimiento;
        }

        public override void MostrarTarea()
        {
            base.MostrarTarea();
            Console.WriteLine($"Fecha de Vencimiento: {FechaVencimiento.ToShortDateString()}");
        }
    }
}
