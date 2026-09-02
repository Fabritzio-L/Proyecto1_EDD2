using System;
using System.IO; 

public class LectorArchivos
{
    //Metodo estatico que carga los jugadores desde un archivo CSV y los devuelve en ArregloJugadores
    public static ArregloJugadores CargarDesdeCSV(string rutaArchivo)
    {
        // Instancia estructura
        ArregloJugadores jugadoresCargados = new ArregloJugadores();

        try
        {
            // Validam que el archivo exista
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"No se encontró el archivo: {rutaArchivo}");
                return jugadoresCargados;
            }

            //Lee el archivo línea por línea
            using (StreamReader lector = new StreamReader(rutaArchivo))
            {
                // Lee el encabezado y lo descarta
                string linea = lector.ReadLine();

                // Lee hasta que no haya más líneas
                while ((linea = lector.ReadLine()) != null)
                {
                    // Evita líneas en blanco si llegan a existir
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] datos = linea.Split(',');

                    //Asegura de que haya la minima cantidad de datos de un jugador
                    if (datos.Length >= 8)
                    {
                        // Instancia al jugador 
                        Jugador nuevoJugador = new Jugador(
                            datos[0].Trim(),// Nombre
                            datos[1].Trim(),// Selección
                            datos[2].Trim(),// Posición
                            int.Parse(datos[3]),// Minutos Jugados
                            int.Parse(datos[4]),// Goles
                            int.Parse(datos[5]),// Asistencias
                            int.Parse(datos[6]),// Tarjetas
                            int.Parse(datos[7])// Partidos
                        );

                        // Agrega el jugador al arreglo
                        jugadoresCargados.Agregar(nuevoJugador);
                    }
                }
            }
            
            Console.WriteLine($"Se cargaron {jugadoresCargados.Cantidad} jugadores.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }

        return jugadoresCargados;
    }
}