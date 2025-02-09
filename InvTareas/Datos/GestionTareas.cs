namespace InvTareas.Datos
{
    public class GestionTareas
    {
        private List<ITarea> tareas; // Lista de tareas que implementan la interfaz ITarea

        public GestionTareas()
        {
            tareas = new List<ITarea>();
        }

        // Método para agregar una tarea
        public void AgregarTarea(ITarea tarea)
        {
            tareas.Add(tarea);
            Console.WriteLine("Tarea agregada exitosamente.");
        }

        // Método para mostrar todas las tareas
        public void MostrarTareas()
        {
            if (tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas.");
                return;
            }

            for (int i = 0; i < tareas.Count; i++)
            {
                Console.WriteLine($"Tarea #{i + 1}:");
                tareas[i].MostrarTarea();
                Console.WriteLine("---------------------------");
            }
        }

        // Método para marcar una tarea como completada
        public void MarcarTareaComoRealizada(int indice)
        {
            if (indice >= 0 && indice < tareas.Count)
            {
                tareas[indice].CompletarTarea();
                Console.WriteLine("Tarea marcada como completada.");
            }
            else
            {
                Console.WriteLine("Índice inválido.");
            }
        }

        // Método para eliminar una tarea
        public void EliminarTarea(int indice)
        {
            if (indice >= 0 && indice < tareas.Count)
            {
                tareas.RemoveAt(indice);
                Console.WriteLine("Tarea eliminada exitosamente.");
            }
            else
            {
                Console.WriteLine("Índice inválido.");
            }
        }

        // Método para mostrar tareas por prioridad
        public void MostrarTareasPorPrioridad(Prioridad prioridad)
        {
            bool hayTareas = false;

            foreach (var tarea in tareas)
            {
                if (tarea is TareaPrioritaria tareaPrioritaria && tareaPrioritaria.NivelPrioridad == prioridad)
                {
                    tareaPrioritaria.MostrarTarea();
                    Console.WriteLine("---------------------------");
                    hayTareas = true;
                }
            }

            if (!hayTareas)
            {
                Console.WriteLine($"No hay tareas con prioridad {prioridad}.");
            }
        }
    }
}
