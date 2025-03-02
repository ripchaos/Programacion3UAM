public class TareaConSubtareas : Tarea
{
    private List<ITarea> subtareas;

    public TareaConSubtareas(string? titulo, string? descripcion, string? prioridad, DateTime fechaVencimiento)
        : base(titulo, descripcion, prioridad, fechaVencimiento)
    {
        subtareas = new List<ITarea>();
    }

    public override void AgregarSubtarea(ITarea subtarea)
    {
        subtareas.Add(subtarea);
    }

    public override List<ITarea> ObtenerSubtareas()
    {
        return subtareas;
    }

    public override void MostrarTarea()
    {
        base.MostrarTarea();
        if (subtareas.Count > 0)
        {
            Console.WriteLine("Subtareas:");
            foreach (var subtarea in subtareas)
            {
                Console.Write("- ");
                subtarea.MostrarTarea();
            }
        }
    }
}

