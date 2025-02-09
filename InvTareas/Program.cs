using System;
using InvTareas.Datos;

namespace InvTareas
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionTareas gestionTareas = new GestionTareas();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- Gestión de Tareas ---");
                Console.WriteLine("1. Agregar Tarea");
                Console.WriteLine("2. Mostrar Tareas");
                Console.WriteLine("3. Marcar Tarea como Realizada");
                Console.WriteLine("4. Eliminar Tarea");
                Console.WriteLine("5. Mostrar Tareas por Prioridad");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                try
                {
                    string? opcionInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(opcionInput) || !int.TryParse(opcionInput, out int opcion))
                    {
                        Console.WriteLine("Entrada inválida. Ingrese un número válido.");
                        continue;
                    }

                    switch (opcion)
                    {
                        case 1:
                            AgregarTarea(gestionTareas);
                            break;

                        case 2:
                            gestionTareas.MostrarTareas();
                            break;

                        case 3:
                            Console.Write("Ingrese el índice de la tarea a marcar como realizada: ");
                            if (int.TryParse(Console.ReadLine(), out int indiceCompletar))
                            {
                                gestionTareas.MarcarTareaComoRealizada(indiceCompletar - 1);
                            }
                            else
                            {
                                Console.WriteLine("Índice inválido.");
                            }
                            break;

                        case 4:
                            Console.Write("Ingrese el índice de la tarea a eliminar: ");
                            if (int.TryParse(Console.ReadLine(), out int indiceEliminar))
                            {
                                gestionTareas.EliminarTarea(indiceEliminar - 1);
                            }
                            else
                            {
                                Console.WriteLine("Índice inválido.");
                            }
                            break;

                        case 5:
                            Console.WriteLine("Seleccione la prioridad (0 - Alta, 1 - Media, 2 - Baja): ");
                            if (int.TryParse(Console.ReadLine(), out int prioridadInput) && Enum.IsDefined(typeof(Prioridad), prioridadInput))
                            {
                                Prioridad prioridad = (Prioridad)prioridadInput;
                                gestionTareas.MostrarTareasPorPrioridad(prioridad);
                            }
                            else
                            {
                                Console.WriteLine("Prioridad inválida.");
                            }
                            break;

                        case 6:
                            salir = true;
                            Console.WriteLine("¡Gracias por usar el gestor de tareas!");
                            break;

                        default:
                            Console.WriteLine("Opción inválida. Intente de nuevo.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Ingrese un número válido.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
            }
        }

        // Método para agregar una nueva tarea
        static void AgregarTarea(GestionTareas gestionTareas)
        {
            string descripcion;
            do
            {
                Console.Write("Descripción de la tarea: ");
                descripcion = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    Console.WriteLine("La descripción no puede estar vacía. Intente de nuevo.");
                }
            } while (string.IsNullOrWhiteSpace(descripcion));

            Console.WriteLine("¿Qué tipo de tarea desea agregar?");
            Console.WriteLine("1. Tarea Simple");
            Console.WriteLine("2. Tarea con Fecha");
            Console.WriteLine("3. Tarea Prioritaria");
            Console.Write("Seleccione una opción: ");

            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int tipoTarea))
            {
                Console.WriteLine("Entrada inválida. Ingrese un número válido.");
                return;
            }

            switch (tipoTarea)
            {
                case 1:
                    gestionTareas.AgregarTarea(new Tarea(descripcion));
                    break;

                case 2:
                    Console.Write("Ingrese la fecha de vencimiento (yyyy-MM-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime fechaVencimiento))
                    {
                        gestionTareas.AgregarTarea(new TareaConFecha(descripcion, fechaVencimiento));
                    }
                    else
                    {
                        Console.WriteLine("Fecha inválida.");
                    }
                    break;

                case 3:
                    Console.WriteLine("Seleccione la prioridad (0 - Alta, 1 - Media, 2 - Baja): ");
                    if (int.TryParse(Console.ReadLine(), out int prioridadInput) && Enum.IsDefined(typeof(Prioridad), prioridadInput))
                    {
                        Prioridad prioridad = (Prioridad)prioridadInput;
                        gestionTareas.AgregarTarea(new TareaPrioritaria(descripcion, prioridad));
                    }
                    else
                    {
                        Console.WriteLine("Prioridad inválida.");
                    }
                    break;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}


