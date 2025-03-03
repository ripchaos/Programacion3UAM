using System;
using System.Collections.Generic;

public class TareaConSubtareas : Tarea
{
    private List<Tarea> subtareas;

    // ✅ Constructor vacío para deserialización JSON
    public TareaConSubtareas() : base()
    {
        subtareas = new List<Tarea>();
    }

    public TareaConSubtareas(string? titulo, string? descripcion, string? prioridad, DateTime fechaVencimiento)
        : base(titulo, descripcion, prioridad, fechaVencimiento)
    {
        subtareas = new List<Tarea>();
    }

    // ✅ Permite agregar subtareas correctamente
    public override void AgregarSubtarea(Tarea subtarea)
    {
        subtareas.Add(subtarea);
    }

    // ✅ Devuelve la lista de subtareas
    public override List<Tarea> ObtenerSubtareas()
    {
        return subtareas;
    }

    public override void MostrarTarea()
    {
        base.MostrarTarea();
        if (subtareas.Count > 0)
        {
            Console.WriteLine("📂 **Subtareas:**");
            foreach (var subtarea in subtareas)
            {
                Console.WriteLine("   ├──  " + subtarea.Titulo);
                Console.WriteLine("   │    " + subtarea.Descripcion);
                Console.WriteLine("   │    " + subtarea.Prioridad);
                Console.WriteLine("   │    " + subtarea.FechaVencimiento.ToShortDateString());
                Console.WriteLine("   └──  Estado: " + (subtarea.Completada ? "✔ Completada" : "❌ Pendiente"));
            }
            Console.WriteLine("─────────────────────────────\n");
        }
        else
        {
            Console.WriteLine("📂 No hay subtareas para esta tarea.\n");
        }
    }

    public void AgregarSubtareaInteractiva()
    {
        Console.Clear();
        Console.WriteLine($"📌 Agregando subtarea a: {Titulo}");

        string? tituloSubtarea = Utilidades.PedirDato("Ingrese el título de la subtarea:");
        if (tituloSubtarea == null) return;

        string? descripcionSubtarea = Utilidades.PedirDato("Ingrese la descripción de la subtarea:");
        if (descripcionSubtarea == null) return;

        string? prioridadSubtarea = Utilidades.PedirDato("Ingrese la prioridad de la subtarea (Baja, Media, Alta):");
        if (prioridadSubtarea == null) return;

        DateTime fechaVencimientoSubtarea;
        while (true)
        {
            Console.Write("Ingrese la fecha de vencimiento de la subtarea (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out fechaVencimientoSubtarea))
                break;
            Console.WriteLine("⚠ Fecha inválida. Intente de nuevo.");
        }

        Tarea subtarea = new Tarea(tituloSubtarea, descripcionSubtarea, prioridadSubtarea, fechaVencimientoSubtarea);
        AgregarSubtarea(subtarea);
    }


}




