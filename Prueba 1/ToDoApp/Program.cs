using System;
using System.Collections.Generic;
using System.Threading;

Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.Clear();
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Title = "⌨️ ✅ ToDoApp - Gestor de Tareas";

int consoleWidth = Console.WindowWidth;
int consoleHeight = Console.WindowHeight;

// ✅ Función para centrar texto en la consola
void PrintCentered(string text)
{
    int padding = (consoleWidth - text.Length) / 2;
    Console.WriteLine(new string(' ', Math.Max(0, padding)) + text);
}

void MostrarFirma()
{
    int posX = consoleWidth - 20; 
    int posY = Console.WindowHeight - 1; 

    Console.SetCursorPosition(posX, posY);
    Console.Write("Dev by: @Ripchaos");
}


// ✅ Función para mostrar el logo ASCII
void MostrarLogo()
{
    Console.Clear();
    string[] logo = {
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
    foreach (string line in logo)
    {
        PrintCentered(line);
    }
    PrintCentered("~ Bienvenido a ToDoApp ~\n");
}

// ✅ Función para mostrar animación de carga
void MostrarCarga(string mensaje)
{
    Console.Clear();
    PrintCentered(mensaje);
    Console.Write("\n\n");

    int barraLargo = 20;
    int padding = (consoleWidth - (barraLargo + 2)) / 2;
    Console.Write(new string(' ', Math.Max(0, padding)) + "[");

    for (int i = 0; i < barraLargo; i++)
    {
        Thread.Sleep(100);
        Console.Write("█");
    }

    Console.WriteLine("]");
    Thread.Sleep(300);
}

// ✅ Función para pausar antes de regresar al menú
void Pausar()
{
    PrintCentered("Presione Enter para continuar...");
    Console.ReadLine();
    Console.Clear();
}

// ✅ Función para pedir datos al usuario con opción de cancelar
string? PedirDato(string mensaje)
{
    while (true)
    {
        Console.Clear();
        PrintCentered(mensaje);
        PrintCentered("(O ingrese 'X' para cancelar)");
        Console.Write("\n\n");

        int padding = (consoleWidth - mensaje.Length) / 2;
        Console.Write(new string(' ', Math.Max(0, padding)) + "> ");

        string? input = Console.ReadLine()?.Trim();

        if (input?.ToUpper() == "X")
        {
            PrintCentered("Operación cancelada.");
            Thread.Sleep(1000);
            Console.Clear();
            return null;
        }

        if (!string.IsNullOrWhiteSpace(input))
            return input;
    }
}

GestorTareas gestor = new GestorTareas();

// ✅ **Diccionario con nombres de opciones**
Dictionary<int, string> nombresOpciones = new Dictionary<int, string>
{
    { 1, "Crear Tarea" },
    { 2, "Ver Tareas" },
    { 3, "Marcar Tarea como Completada" },
    { 4, "Editar Tarea" },
    { 5, "Eliminar Tarea" },
    { 6, "Agregar Subtarea" },
    { 7, "Limpiar Todas las Tareas" },
    { 8, "Filtrar y Buscar Tareas" },
    { 9, "Salir" }
};

// ✅ **Diccionario de Funciones para el Menú**
Dictionary<int, Action> menuOpciones = new Dictionary<int, Action>
{
    { 1, () => {
        MostrarCarga("Creando tarea");
        string? titulo = PedirDato("Ingrese el título:");
        if (titulo == null) return;

        string? descripcion = PedirDato("Ingrese la descripción:");
        if (descripcion == null) return;

        string? prioridad = PedirDato("Ingrese la prioridad (Baja, Media, Alta):");
        if (prioridad == null) return;

        DateTime fechaVencimiento;
        while (true)
        {
            Console.Write("Ingrese la fecha de vencimiento (YYYY-MM-DD): ");
            if (DateTime.TryParse(Console.ReadLine(), out fechaVencimiento))
                break;
            Console.WriteLine(" Fecha inválida. Intente de nuevo.");
        }

        gestor.AgregarTarea(new Tarea(titulo, descripcion, prioridad, fechaVencimiento));
        Pausar();
    }},

    { 2, () => { MostrarCarga("Cargando tareas"); gestor.MostrarTareas(); Pausar(); } },

    { 3, () => { MostrarCarga("Actualizando estado de la tarea"); gestor.MostrarTareas();
        Console.Write("Ingrese el número de la tarea a marcar como completada: ");
        if (int.TryParse(Console.ReadLine(), out int indiceTarea))
            gestor.MarcarTareaComoCompletada(indiceTarea - 1);
        else
            Console.WriteLine(" Número inválido.");
        Pausar();
    }},

    { 4, () => { // ✅ Editar Tarea con índice correcto
    MostrarCarga("Editando tarea");
    gestor.MostrarTareas();
    Console.Write("Ingrese el número de la tarea a editar: ");
    if (int.TryParse(Console.ReadLine(), out int indiceEdit))
    {
        string? nuevoTitulo = PedirDato("Nuevo título:");
        string? nuevaDescripcion = PedirDato("Nueva descripción:");
        string? nuevaPrioridad = PedirDato("Nueva prioridad (Baja, Media, Alta):");
        DateTime.TryParse(PedirDato("Nueva fecha de vencimiento (YYYY-MM-DD):"), out DateTime nuevaFecha);

        gestor.EditarTarea(indiceEdit - 1, nuevoTitulo ?? "", nuevaDescripcion ?? "", nuevaPrioridad ?? "", nuevaFecha);
    }
    else
    {
        Console.WriteLine(" Número inválido.");
    }
    Pausar();
}},

{ 5, () => { // ✅ Eliminar Tarea con índice correcto
    MostrarCarga("Eliminando tarea");
    gestor.MostrarTareas();
    Console.Write("Ingrese el número de la tarea a eliminar: ");
    if (int.TryParse(Console.ReadLine(), out int indiceEliminar))
    {
        gestor.EliminarTarea(indiceEliminar - 1);
    }
    else
    {
        Console.WriteLine(" Número inválido.");
    }
    Pausar();
}},

{ 6, () => { // ✅ Agregar Subtarea con índice correcto
    MostrarCarga("Agregando subtarea");
    gestor.MostrarTareas();
    Console.Write("Ingrese el número de la tarea a la que desea agregar una subtarea: ");
    if (int.TryParse(Console.ReadLine(), out int numeroTarea))
    {
        gestor.AgregarSubtarea(numeroTarea - 1);
    }
    else
    {
        Console.WriteLine("⚠ Número inválido.");
    }
    Pausar();
}},

{ 7, () => {
    MostrarCarga("Limpiando todas las tareas");
    gestor.LimpiarTareas();
    Pausar();
}},


    { 8, () => { // ✅ Filtrar y Buscar tareas con opciones reales
    MostrarCarga("Filtrando tareas");
    PrintCentered("Seleccione un filtro:");
    PrintCentered("1. Ver solo tareas pendientes");
    PrintCentered("2. Ver solo tareas completadas");
    PrintCentered("3. Filtrar por prioridad");
    PrintCentered("4. Ordenar por fecha de vencimiento");
    PrintCentered("5. Buscar tarea por palabra clave");
    PrintCentered("6. Establecer dependencia entre tareas");
    PrintCentered("7. Volver al menú principal");

    Console.Write("\n> ");
    if (int.TryParse(Console.ReadLine(), out int opcionFiltro))
    {
        Console.Clear();
        switch (opcionFiltro)
        {
            case 1:
                MostrarCarga("Mostrando tareas pendientes...");
                gestor.FiltrarPorEstado(false);
                break;
            case 2:
                MostrarCarga("Mostrando tareas completadas...");
                gestor.FiltrarPorEstado(true);
                break;
            case 3:
                string? prioridad = PedirDato("Ingrese la prioridad (Baja, Media, Alta):");
                if (prioridad != null)
                {
                    MostrarCarga($"Filtrando por prioridad {prioridad}...");
                    gestor.FiltrarPorPrioridad(prioridad);
                }
                break;
            case 4:
                MostrarCarga("Ordenando por fecha de vencimiento...");
                gestor.FiltrarPorFecha();
                break;
            case 5:
                string? palabraClave = PedirDato("Ingrese una palabra clave para buscar:");
                if (palabraClave != null)
                {
                    MostrarCarga($"Buscando tareas que contengan '{palabraClave}'...");
                    gestor.BuscarTarea(palabraClave);
                }
                break;
            case 6:
                Console.Write("Ingrese el número de la tarea principal: ");
                if (int.TryParse(Console.ReadLine(), out int tareaPrincipal))
                {
                    Console.Write("Ingrese el número de la tarea dependiente: ");
                    if (int.TryParse(Console.ReadLine(), out int tareaDependiente))
                    {
                        gestor.EstablecerDependencia(tareaPrincipal - 1, tareaDependiente - 1);
                    }
                }
                break;
            case 7: return; // Volver al menú principal
            default: Console.WriteLine("⚠ Opción inválida."); break;
        }
    }
    Pausar();
}},


    { 9, () => { // ✅ Opción "Salir" corregida
        MostrarCarga("Saliendo del programa");
        PrintCentered("Hasta pronto!");
        Thread.Sleep(1000);
        Console.Clear();
        Environment.Exit(0);
    }},
};

// ✅ **Menú Principal con Diccionario y Logo**
int opcion;
do
{
    MostrarLogo();
    PrintCentered("--- Menú Principal ---");

    // 🔥 Asegurar que todas las opciones aparecen en orden numérico
    for (int i = 1; i <= nombresOpciones.Count; i++)
    {
        if (nombresOpciones.ContainsKey(i))
            PrintCentered($"{i}. {nombresOpciones[i]}");
    }

    // ✅ Mostrar la firma ANTES del prompt para que no lo mueva
    MostrarFirma();

    // ✅ Imprimir el prompt en el centro
    int promptPos = (consoleWidth / 2) - 2;
    int promptLine = Console.CursorTop - 3; 
    if (promptLine < 0) promptLine = 0; 

    Console.SetCursorPosition(promptPos, promptLine);
    Console.Write("> ");

    if (int.TryParse(Console.ReadLine(), out opcion) && menuOpciones.ContainsKey(opcion))
        menuOpciones[opcion]();
    else
    {
        Console.WriteLine("⚠ Opción inválida.");
        Pausar();
    }

} while (opcion != 9);




