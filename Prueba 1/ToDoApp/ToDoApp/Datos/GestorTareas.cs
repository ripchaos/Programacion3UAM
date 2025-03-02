using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Clase que gestiona las tareas y subtareas
public class GestorTareas
{
    private List<Tarea> tareas;
    private readonly string archivoJson = "tareas.json";

    public GestorTareas()
    {
        tareas = new List<Tarea>();
        CargarTareas();
    }

    public void AgregarTarea(Tarea tarea)
    {
        tareas.Add(tarea);
        GuardarTareas();
    }

    public void AgregarSubtarea(int tareaId, Tarea subtarea)
    {
        if (tareaId >= 0 && tareaId < tareas.Count && tareas[tareaId] is TareaConSubtareas tareaConSubtareas)
        {
            tareaConSubtareas.AgregarSubtarea(subtarea);
            GuardarTareas();
        }
        else
        {
            Console.WriteLine("ID de tarea inválido o la tarea no admite subtareas.");
        }
    }

    public void EditarTarea(int indice, string nuevoTitulo, string nuevaDescripcion, string nuevaPrioridad, DateTime nuevaFecha)
    {
        if (indice >= 0 && indice < tareas.Count)
        {
            tareas[indice].Editar(nuevoTitulo, nuevaDescripcion, nuevaPrioridad, nuevaFecha);
            GuardarTareas();
        }
        else
        {
            Console.WriteLine("ID de tarea no válido.");
        }
    }

    public void EliminarTarea(int indice)
    {
        if (indice >= 0 && indice < tareas.Count)
        {
            tareas.RemoveAt(indice);
            GuardarTareas();
        }
    }

    public void MostrarTareas()
    {
        if (tareas.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas.");
            return;
        }

        for (int i = 0; i < tareas.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]");
            tareas[i].MostrarTarea();
        }
    }

    public void LimpiarTareas()
    {
        tareas.Clear();
        File.WriteAllText(archivoJson, "[]"); // Guarda un JSON vacío
        Console.WriteLine("Todas las tareas han sido eliminadas.");
    }

    private void GuardarTareas()
    {
        string json = JsonSerializer.Serialize(tareas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(archivoJson, json);
    }

    private void CargarTareas()
    {
        if (File.Exists(archivoJson))
        {
            string json = File.ReadAllText(archivoJson);
            tareas = JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
        } // ← **Esta llave estaba faltando**
    }

    public void MarcarTareaComoCompletada(int indice)
    {
        if (indice >= 0 && indice < tareas.Count)
        {
            tareas[indice].MarcarCompletada();
            GuardarTareas();
        }
        else
        {
            Console.WriteLine("Número de tarea inválido.");
        }
    }
}

