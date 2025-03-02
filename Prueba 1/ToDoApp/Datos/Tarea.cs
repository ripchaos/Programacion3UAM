public class Tarea : ITarea
{
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public string Prioridad { get; set; }
    public bool Completada { get; set; }
    public DateTime FechaVencimiento { get; set; }

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
        Completada = true;
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
        Console.WriteLine($"Titulo: {Titulo}\nDescripcion: {Descripcion}\nPrioridad: {Prioridad}\nEstado: {(Completada ? "Completada" : "Pendiente")}\nFecha de Vencimiento: {FechaVencimiento.ToShortDateString()}\n");
    }

    public virtual void AgregarSubtarea(ITarea subtarea)
    {
        throw new NotImplementedException("Esta clase base no admite subtareas.");
    }

    public virtual List<ITarea> ObtenerSubtareas()
    {
        return new List<ITarea>(); // Devuelve una lista vacía porque no soporta subtareas
    }
}

