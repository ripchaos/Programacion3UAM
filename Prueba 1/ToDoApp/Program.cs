using System;
using System.Threading;

Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.Clear();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Title = "⌨️ ✅ ToDoApp - Gestor de Tareas";

// Obtener ancho de la consola
int consoleWidth = Console.WindowWidth;

// Función para centrar texto
void PrintCentered(string text)
{
    int padding = (consoleWidth - text.Length) / 2;
    Console.WriteLine(new string(' ', Math.Max(0, padding)) + text);
}

// Función de animación de carga centrada
void MostrarCarga(string mensaje)
{
    Console.Clear();
    PrintCentered(mensaje);
    Console.Write("\n\n");
    PrintCentered("[");
    for (int i = 0; i < 3; i++)  // Animación de 3 puntos
    {
        Thread.Sleep(500);
        Console.Write("█");
    }
    Console.WriteLine("]\n");
}

// Función para pausar antes de regresar al menú
void Pausar()
{
    PrintCentered("Presione Enter para continuar...");
    Console.ReadLine();
    Console.Clear();
}

// Función para pedir datos al usuario con opción de cancelar
string PedirDato(string mensaje)
{
    while (true)
    {
        Console.Clear();
        PrintCentered(mensaje);
        PrintCentered("(O ingrese 'X' para cancelar)");
        Console.Write("\n\n");

        int padding = (consoleWidth - mensaje.Length) / 2;
        Console.Write(new string(' ', Math.Max(0, padding)) + "> ");

        string input = Console.ReadLine()?.Trim();

        if (input?.ToUpper() == "X")
        {
            PrintCentered("Operación cancelada.");
            Thread.Sleep(1000);
            Console.Clear();
            return null;  // Retorna nulo para indicar cancelación
        }

        if (!string.IsNullOrWhiteSpace(input))
            return input;
    }
}

// Logo del teclado ASCII
string[] logo = new string[]
{
    ",---,---,---,---,---,---,---,---,---,---,---,---,---,-------,",
    "|1/2| 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 0 | + | ' | <-    |",
    "|---'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-----|",
    "| ->| | Q | W | E | R | T | Y | U | I | O | P | ] | ^ |     |",
    "|-----',--',--',--',--',--',--',--',--',--',--',--',--'|    |",
    "| Caps | A | S | D | F | G | H | J | K | L | \\ | [ | * |    |",
    "|----,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'-,-'---'----|",
    "|    | < | Z | X | C | V | B | N | M | , | . | - |          |",
    "|----'-,-',--'--,'---'---'---'---'---'---'-,-'---',--,------|",
    "| ctrl |  | alt |                          |altgr |  | ctrl |",
    "'------'  '-----'--------------------------'------'  '------'"
};

// Imprimir el logo centrado
foreach (string line in logo)
{
    PrintCentered(line);
}

PrintCentered("~ Gestor de Tareas ~\n");

GestorTareas gestor = new GestorTareas();

int opcion;
do
{
    Console.Clear();
    foreach (string line in logo)
    {
        PrintCentered(line);
    }
    PrintCentered("~ Gestor de Tareas ~\n");

    PrintCentered("--- Menú Principal ---");
    PrintCentered("1. Crear Tarea");
    PrintCentered("2. Ver Tareas");
    PrintCentered("3. Marcar Tarea como Completada");
    PrintCentered("4. Editar Tarea");
    PrintCentered("5. Eliminar Tarea");
    PrintCentered("6. Agregar Subtarea");
    PrintCentered("7. Limpiar Todas las Tareas");
    PrintCentered("8. Salir");
    PrintCentered("Seleccione una opción: ");

    int padding = (consoleWidth - 10) / 2;
    Console.Write(new string(' ', Math.Max(0, padding)) + "> ");

    if (int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.Clear();

        switch (opcion)
        {
            case 1:
                MostrarCarga("Creando tarea");
                string titulo = PedirDato("Ingrese el título:");
                if (titulo == null) break;  // Si canceló, salir de la opción
                string descripcion = PedirDato("Ingrese la descripción:");
                if (descripcion == null) break;
                PrintCentered("Tarea creada con éxito!");
                Pausar();
                break;

            case 2:
                MostrarCarga("Cargando tareas");
                gestor.MostrarTareas();
                Pausar();
                break;

            case 3:
                MostrarCarga("Actualizando estado de la tarea");
                MarcarTareaComoCompletada(gestor);
                Pausar();
                break;

            case 4:
                MostrarCarga("Editando tarea");
                EditarTarea(gestor);
                Pausar();
                break;

            case 5:
                MostrarCarga("Eliminando tarea");
                EliminarTarea(gestor);
                Pausar();
                break;

            case 6:
                MostrarCarga("Agregando subtarea");
                AgregarSubtarea(gestor);
                Pausar();
                break;

            case 7:
                MostrarCarga("Limpiando todas las tareas");
                gestor.LimpiarTareas();
                Pausar();
                break;

            case 8:
                MostrarCarga("Saliendo del programa");
                PrintCentered("Hasta pronto!");
                Thread.Sleep(1000);
                Console.Clear();
                break;

            default:
                PrintCentered("⚠ Opción no válida, intente de nuevo.");
                Thread.Sleep(1000);
                break;
        }
    }
    else
    {
        PrintCentered("⚠ Opción no válida, intente de nuevo.");
        Thread.Sleep(1000);
    }

} while (opcion != 8);



static void MarcarTareaComoCompletada(GestorTareas gestor)
    {
        gestor.MostrarTareas();
        Console.Write("Ingrese el número de la tarea a marcar como completada: ");
        if (int.TryParse(Console.ReadLine(), out int indice))
        {
            gestor.MarcarTareaComoCompletada(indice - 1);
            Console.WriteLine("Tarea marcada como completada!\n");
        }
        else
        {
            Console.WriteLine("Número inválido.\n");
        }
    }

    static void EliminarTarea(GestorTareas gestor)
    {
        gestor.MostrarTareas();
        Console.Write("Ingrese el número de la tarea a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int indice))
        {
            gestor.EliminarTarea(indice - 1);
            Console.WriteLine("Tarea eliminada con éxito!\n");
        }
        else
        {
            Console.WriteLine("Número inválido.\n");
        }
    }

    static void CrearTarea(GestorTareas gestor)
    {
        Console.Write("Ingrese el título: ");
        string? titulo = Console.ReadLine();
        titulo = string.IsNullOrWhiteSpace(titulo) ? "Sin título" : titulo;

        Console.Write("Ingrese la descripción: ");
        string? descripcion = Console.ReadLine();
        descripcion = string.IsNullOrWhiteSpace(descripcion) ? "Sin descripción" : descripcion;

        Console.Write("Ingrese la prioridad (Baja, Media, Alta): ");
        string? prioridad = Console.ReadLine();
        prioridad = string.IsNullOrWhiteSpace(prioridad) ? "Media" : prioridad;

        DateTime fechaVencimiento;
        while (true)
        {
            Console.Write("Ingrese la fecha de vencimiento (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out fechaVencimiento))
                break;
            Console.WriteLine("Fecha inválida. Intente de nuevo.");
        }

        gestor.AgregarTarea(new Tarea(titulo, descripcion, prioridad, fechaVencimiento));
        Console.WriteLine("Tarea creada con éxito!\n");
    }

    static void EditarTarea(GestorTareas gestor)
    {
        gestor.MostrarTareas();
        Console.Write("Ingrese el número de la tarea a editar: ");
        if (int.TryParse(Console.ReadLine(), out int indice))
        {
            Console.Write("Nuevo título: ");
            string nuevoTitulo = Console.ReadLine() ?? "";
            Console.Write("Nueva descripción: ");
            string nuevaDescripcion = Console.ReadLine() ?? "";
            Console.Write("Nueva prioridad (Baja, Media, Alta): ");
            string nuevaPrioridad = Console.ReadLine() ?? "";
            Console.Write("Nueva fecha de vencimiento (YYYY-MM-DD): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime nuevaFecha);

            gestor.EditarTarea(indice - 1, nuevoTitulo, nuevaDescripcion, nuevaPrioridad, nuevaFecha);
            Console.WriteLine("Tarea editada con éxito!\n");
        }
        else
        {
            Console.WriteLine("Número inválido.\n");
        }
    }

    static void AgregarSubtarea(GestorTareas gestor)
    {
        gestor.MostrarTareas();
        Console.Write("Ingrese el número de la tarea principal: ");
        if (int.TryParse(Console.ReadLine(), out int tareaId))
        {
            Console.Write("Ingrese el título de la subtarea: ");
            string titulo = Console.ReadLine() ?? "Sin título";
            Console.Write("Ingrese la descripción de la subtarea: ");
            string descripcion = Console.ReadLine() ?? "Sin descripción";
            Console.Write("Ingrese la prioridad de la subtarea (Baja, Media, Alta): ");
            string prioridad = Console.ReadLine() ?? "Media";
            Console.Write("Ingrese la fecha de vencimiento de la subtarea (YYYY-MM-DD): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime fechaVencimiento);

            gestor.AgregarSubtarea(tareaId - 1, new TareaConSubtareas(titulo, descripcion, prioridad, fechaVencimiento));
            Console.WriteLine("Subtarea agregada con éxito!\n");
        }
        else
        {
            Console.WriteLine("Número inválido.\n");
        }
    }


