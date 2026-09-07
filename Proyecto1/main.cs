using System;
using System.IO;

namespace Proyecto1_EDD2
{
    class Program
    {
        
        static ArbolBPlus arbol = new ArbolBPlus(5);
        static void Main(string[] args)
        {

            //Carga datos desde el CSV al iniciar el programa
            Console.WriteLine("Cargando datos de jugadores desde el archivo CSV...");
            ArregloJugadores jugadoresCargados = LectorArchivos.CargarDesdeCSV("jugadores.csv"); 

            //  Lo carga en el árbol B+
            if (jugadoresCargados != null && jugadoresCargados.Cantidad > 0)
            {
                for (int i = 0; i < jugadoresCargados.Cantidad; i++)
                {
                    arbol.Insertar(jugadoresCargados.datos[i]);
                }
                Console.WriteLine($"{jugadoresCargados.Cantidad} jugadores cargados al Árbol B+.");
            }
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine(" -----------SISTEMA DE GESTIÓN DE JUGADORES DEL MUNDIAL-----------");
                Console.WriteLine("1. Registrar jugadores");
                Console.WriteLine("2. Buscar estadisticas de un jugador");
                Console.WriteLine("3. Actualizar estadísticas de un jugador");
                Console.WriteLine("4. Eliminar a un jugador");
                Console.WriteLine("5. Generar Top 5 por categoría");
                Console.WriteLine("6. Mostrar listado general");
                Console.WriteLine("7. Salir");
                Console.Write("\nSeleccione una opción: ");
                
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarJugador();
                        break;
                    case "2":
                        BuscarJugador();
                        break;
                    case "3":
                        ActualizarJugador();
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida intente de nuevo.");
                        break;
                }
            }
        }

        static void RegistrarJugador()
        {
            Console.WriteLine("\n--- REGISTRAR NUEVO JUGADOR ---");
            try
            {
                Jugador nuevoJugador = new Jugador();

                Console.Write("Nombre: ");
                nuevoJugador.Nombre = Console.ReadLine().Trim();

                Console.Write("Selección: ");
                nuevoJugador.Seleccion = Console.ReadLine().Trim();

                Console.Write("Posición: ");
                nuevoJugador.Posicion = Console.ReadLine().Trim();

                Console.Write("Goles: ");
                nuevoJugador.Goles = int.Parse(Console.ReadLine());

                Console.Write("Asistencias: ");
                nuevoJugador.Asistencias = int.Parse(Console.ReadLine());

                Console.Write("Minutos Jugados: ");
                nuevoJugador.MinutosJugados = int.Parse(Console.ReadLine());

                Console.Write("Partidos Disputados: ");
                nuevoJugador.PartidosDisputados = int.Parse(Console.ReadLine());

                Console.Write("Tarjetas: ");
                nuevoJugador.Tarjetas = int.Parse(Console.ReadLine());

                // Lo guarda en el arbol
                arbol.Insertar(nuevoJugador);
                Console.WriteLine($"\n¡Éxito! El jugador {nuevoJugador.Nombre} fue registrado.");

                //Lo guarda en el CSV
                GuardarCambiosCSV();
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Debe de ingresar números válidos para las estadísticas.");
            }
        }


        static void BuscarJugador()
        {
            Console.Write("\nIngrese el nombre del jugador a buscar: ");
            string nombre = Console.ReadLine().Trim();

            // Usa el metodo de búsqueda del arbol
            Jugador encontrado = arbol.Buscar(nombre);

            if (encontrado != null)
            {
                Console.WriteLine("\n--- ESTADÍSTICAS DEL JUGADOR ---");
                // Llama al método imprimir del jugador encontrado
                encontrado.Imprimir(); 
            }
            else
            {
                Console.WriteLine("\nError: Jugador no encontrado.");
            }
        }

        static void ActualizarJugador()
        {
            Console.Write("\nIngrese el nombre del jugador a actualizar: ");
            string nombre = Console.ReadLine().Trim();

            // Buscamos al jugador en el Árbol B+
            Jugador encontrado = arbol.Buscar(nombre);

            if (encontrado != null)
            {
                Console.WriteLine($"\n--- ACTUALIZANDO ESTADÍSTICAS DE {encontrado.Nombre.ToUpper()} ---");
                Console.WriteLine("Nota: Presione Enter sin escribir nada para mantener el valor actual.");

                try
                {
                    Console.Write($"Goles actuales ({encontrado.Goles}): ");
                    string goles = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(goles)) 
                        encontrado.Goles = int.Parse(goles);

                    Console.Write($"Asistencias actuales ({encontrado.Asistencias}): ");
                    string asistencias = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(asistencias)) 
                        encontrado.Asistencias = int.Parse(asistencias);

                    Console.Write($"Minutos actuales ({encontrado.MinutosJugados}): ");
                    string minutos = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(minutos)) 
                        encontrado.MinutosJugados = int.Parse(minutos);

                    Console.Write($"Partidos disputados actuales ({encontrado.PartidosDisputados}): ");
                    string partidos = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(partidos)) 
                        encontrado.PartidosDisputados = int.Parse(partidos);

                    Console.Write($"Tarjetas actuales ({encontrado.Tarjetas}): ");
                    string tarjetas = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(tarjetas)) 
                        encontrado.Tarjetas = int.Parse(tarjetas);

                    GuardarCambiosCSV(); // Guarda los cambios en el archivo CSV

                    Console.WriteLine("\nEstadísticas actualizadas correctamente");
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nError: Debe ingresar un número entero válido. Actualización cancelada.");
                }
            }
            else
            {
                Console.WriteLine("\nError: Jugador no encontrado.");
            }
        }


        static void GuardarCambiosCSV()
        {
            // Sobrescribe el archivo con los datos actualizados
            using (StreamWriter writer = new StreamWriter("jugadores.csv"))
            {
                // Escribe el encabezado
                writer.WriteLine("Nombre,Seleccion,Posicion,Goles,Asistencias,MinutosJugados,PartidosDisputados,Tarjetas");

                // Extrae la primera hoja del Árbol B+
                NodoBPlus actual = arbol.ObtenerPrimeraHoja();

                // Recorre la lista enlazada de hojas
                while (actual != null)
                {
                    for (int i = 0; i < actual.CantidadClaves; i++)
                    {
                        Jugador j = actual.Jugadores[i];
                        writer.WriteLine($"{j.Nombre},{j.Seleccion},{j.Posicion},{j.Goles},{j.Asistencias},{j.MinutosJugados},{j.PartidosDisputados},{j.Tarjetas}");
                    }
                    actual = actual.Siguiente; // Salta a la siguiente hoja
                }
            }
        }
    }
}