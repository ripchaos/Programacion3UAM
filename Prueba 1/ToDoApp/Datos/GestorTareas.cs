using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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

    public void AgregarSubtarea(int tareaId)
    {
        if (tareaId >= 0 && tareaId < tareas.Count)
        {
            if (!(tareas[tareaId] is TareaConSubtareas))
            {
                // ✅ Convertir la tarea en una TareaConSubtareas si no lo es
                Tarea tareaOriginal = tareas[tareaId];
                TareaConSubtareas nuevaTarea = new TareaConSubtareas(
                    tareaOriginal.Titulo,
                    tareaOriginal.Descripcion,
                    tareaOriginal.Prioridad,
                    tareaOriginal.FechaVencimiento
                );

                tareas[tareaId] = nuevaTarea; // 🔄 Reemplaza la tarea original
            }

            // ✅ Ahora que estamos seguros de que es TareaConSubtareas, llamamos al método
            ((TareaConSubtareas)tareas[tareaId]).AgregarSubtareaInteractiva();
            GuardarTareas();
        }
        else
        {
            Console.WriteLine("⚠ ID de tarea inválido.");
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
            Console.WriteLine("⚠ ID de tarea no válido.");
        }
    }

    public void EliminarTarea(int indice)
    {
        if (indice >= 0 && indice < tareas.Count)
        {
            tareas.RemoveAt(indice);
            GuardarTareas();
        }
        else
        {
            Console.WriteLine("⚠ Número inválido.");
        }
    }

    public void MostrarTareas()
    {
        if (tareas.Count == 0)
        {
            Console.WriteLine("📂 No hay tareas registradas.");
            return;
        }

        Console.WriteLine("📌 **Lista de Tareas:**");
        for (int i = 0; i < tareas.Count; i++)
        {
            Console.WriteLine($"[{i + 1}]");
            tareas[i].MostrarTarea();
        }
    }

    public void LimpiarTareas()
    {
        tareas.Clear();
        File.WriteAllText(archivoJson, "[]");
        Console.WriteLine("✅ **Todas las tareas han sido eliminadas.**");
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
        }
    }

    public void MarcarTareaComoCompletada(int indice)
    {
        if (indice >= 0 && indice < tareas.Count)
        {
            tareas[indice].MarcarCompletada();
            GuardarTareas();
            Console.WriteLine("✅ **Tarea marcada como completada!**");
        }
        else
        {
            Console.WriteLine("⚠ Número de tarea inválido.");
        }
    }
    public void FiltrarPorEstado(bool mostrarCompletadas)
    {
        var tareasFiltradas = tareas.Where(t => t.Completada == mostrarCompletadas).ToList();

        if (tareasFiltradas.Count == 0)
        {
            Console.WriteLine("⚠ No hay tareas con este estado.");
            return;
        }

        Console.WriteLine($"📌 Tareas {(mostrarCompletadas ? "Completadas" : "Pendientes")}:");
        foreach (var tarea in tareasFiltradas)
        {
            tarea.MostrarTarea();
        }
    }
    public void FiltrarPorPrioridad(string prioridad)
    {
        var tareasFiltradas = tareas.Where(t => t.Prioridad.Equals(prioridad, StringComparison.OrdinalIgnoreCase)).ToList();

        if (tareasFiltradas.Count == 0)
        {
            Console.WriteLine("⚠ No hay tareas con esta prioridad.");
            return;
        }

        Console.WriteLine($"⚡ Tareas con prioridad {prioridad}:");
        foreach (var tarea in tareasFiltradas)
        {
            tarea.MostrarTarea();
        }
    }
    public void FiltrarPorFecha()
    {
        var tareasOrdenadas = tareas.OrderBy(t => t.FechaVencimiento).ToList();

        if (tareasOrdenadas.Count == 0)
        {
            Console.WriteLine("⚠ No hay tareas registradas.");
            return;
        }

        Console.WriteLine("📅 Tareas ordenadas por fecha de vencimiento:");
        foreach (var tarea in tareasOrdenadas)
        {
            tarea.MostrarTarea();
        }
    }
    public void BuscarTarea(string palabraClave)
    {
        var tareasEncontradas = tareas.Where(t =>
            t.Titulo.Contains(palabraClave, StringComparison.OrdinalIgnoreCase) ||
            t.Descripcion.Contains(palabraClave, StringComparison.OrdinalIgnoreCase)).ToList();

        if (tareasEncontradas.Count == 0)
        {
            Console.WriteLine("⚠ No se encontraron tareas con esa palabra clave.");
            return;
        }

        Console.WriteLine($"🔎 Tareas que contienen '{palabraClave}':");
        foreach (var tarea in tareasEncontradas)
        {
            tarea.MostrarTarea();
        }
    }
    public void EstablecerDependencia(int tareaPrincipal, int tareaDependiente)
    {
        if (tareaPrincipal >= 0 && tareaPrincipal < tareas.Count &&
            tareaDependiente >= 0 && tareaDependiente < tareas.Count)
        {
            tareas[tareaDependiente].Dependencias.Add(tareas[tareaPrincipal]);
            Console.WriteLine($" Se ha establecido una dependencia: '{tareas[tareaDependiente].Titulo}' depende de '{tareas[tareaPrincipal].Titulo}'.");
        }
        else
        {
            Console.WriteLine(" Índices de tarea inválidos.");
        }
    }


}

