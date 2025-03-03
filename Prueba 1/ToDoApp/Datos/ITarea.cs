using System;
using System.Collections.Generic;

// ✅ Ajustado para evitar problemas de serialización y compilación
public interface ITarea
{
    void MostrarTarea();
    void MarcarCompletada();
    void Editar(string nuevoTitulo, string nuevaDescripcion, string nuevaPrioridad, DateTime nuevaFecha);

    // ✅ Ahora usa `Tarea` en lugar de `ITarea` para evitar problemas de implementación
    void AgregarSubtarea(Tarea subtarea);
    List<Tarea> ObtenerSubtareas();
}
