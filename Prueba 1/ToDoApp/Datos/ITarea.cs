using System;
using System.Collections.Generic;

// Definición de la interfaz ITarea
public interface ITarea
{
    void MostrarTarea();
    void MarcarCompletada();
    void Editar(string nuevoTitulo, string nuevaDescripcion, string nuevaPrioridad, DateTime nuevaFecha);

    // Métodos para subtareas
    void AgregarSubtarea(ITarea subtarea);
    List<ITarea> ObtenerSubtareas();
}
