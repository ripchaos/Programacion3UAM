using System;
using System.Collections.Generic;

public class Tarea : ITarea

{
    public List<Tarea> Dependencias { get; set; } = new List<Tarea>();
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Prioridad { get; set; }
    public bool Completada { get; set; }
    public DateTime FechaVencimiento { get; set; }

    // ✅ Constructor vacío para permitir la deserialización JSON
    public Tarea()
    {
        Titulo = "Sin título";
        Descripcion = "Sin descripción";
        Prioridad = "Media";
        Completada = false;
        FechaVencimiento = DateTime.Now;
    }

    public Tarea(string? titulo, string? descripcion, string? prioridad, DateTime fechaVencimiento)
    {
        Titulo = string.IsNullOrWhiteSpace(titulo) ? "Sin título" : titulo;
        Descripcion = string.IsNullOrWhiteSpace(descripcion) ? "Sin descripción" : descripcion;
        Prioridad = string.IsNullOrWhiteSpace(prioridad) ? "Media" : prioridad;
        Completada = false;
        FechaVencimiento = fechaVencimiento;
    }

    public void MarcarCompletada()
    {
        foreach (var tarea in Dependencias)
        {
            if (!tarea.Completada)
            {
                Console.WriteLine($" No puede completar '{Titulo}' hasta que '{tarea.Titulo}' esté terminada.");
                return;
            }
        }

        Completada = true;
        Console.WriteLine($" La tarea '{Titulo}' ha sido completada.");
    }


    public virtual void Editar(string nuevoTitulo, string nuevaDescripcion, string nuevaPrioridad, DateTime nuevaFecha)
    {
        Titulo = string.IsNullOrWhiteSpace(nuevoTitulo) ? Titulo : nuevoTitulo;
        Descripcion = string.IsNullOrWhiteSpace(nuevaDescripcion) ? Descripcion : nuevaDescripcion;
        Prioridad = string.IsNullOrWhiteSpace(nuevaPrioridad) ? Prioridad : nuevaPrioridad;
        FechaVencimiento = nuevaFecha;
    }

    public virtual void MostrarTarea()
    {
        Console.WriteLine("─────────────────────────────");
        Console.WriteLine($" **Título:** {Titulo}");
        Console.WriteLine($" **Descripción:** {Descripcion}");
        Console.WriteLine($" **Prioridad:** {Prioridad}");
        Console.WriteLine($" **Estado:** {(Completada ? "✔ Completada" : "❌ Pendiente")}");
        Console.WriteLine($" **Fecha de Vencimiento:** {FechaVencimiento:yyyy-MM-dd}");
        Console.WriteLine("─────────────────────────────\n");
    }

    // ✅ Solución: En lugar de lanzar una excepción, mostramos un mensaje de advertencia.
    public virtual void AgregarSubtarea(Tarea subtarea)
    {
        Console.WriteLine("⚠ Esta tarea no admite subtareas. Se recomienda crear una tarea con subtareas.");
    }

    // ✅ Ya no lanza excepción, simplemente devuelve una lista vacía.
    public virtual List<Tarea> ObtenerSubtareas()
    {
        return new List<Tarea>();
    }
}

