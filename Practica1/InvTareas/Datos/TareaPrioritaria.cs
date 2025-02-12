using System;

namespace InvTareas.Datos
{
    // Enum para definir los niveles de prioridad
    public enum Prioridad
    {
        Alta,
        Media,
        Baja
    }

    public class TareaPrioritaria : Tarea
    {
        public Prioridad NivelPrioridad { get; set; }

        public TareaPrioritaria(string descripcion, Prioridad prioridad)
            : base(descripcion)
        {
            NivelPrioridad = prioridad;
        }

        public override void MostrarTarea()
        {
            base.MostrarTarea();
            Console.WriteLine($"Prioridad: {NivelPrioridad}");
        }
    }
}
