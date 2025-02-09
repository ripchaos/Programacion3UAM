public class Tarea : ITarea
{
    public string Descripcion { get; set; }
    private bool completada;

    public Tarea(string descripcion)
    {
        Descripcion = descripcion;
        completada = false;
    }

    public virtual void MostrarTarea()
    {
        Console.WriteLine($"Tarea: {Descripcion} - Estado: {(completada ? "Completada" : "Pendiente")}");
    }

    public void CompletarTarea()
    {
        completada = true;
    }

    public bool EstaCompletada()
    {
        return completada;
    }
}

