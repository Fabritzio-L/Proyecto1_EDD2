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
                        break;
                    case "3":
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
                Console.WriteLine($"\n¡Éxito! El jugador {nuevoJugador.Nombre} fue registrado en la base de datos.");

                //Lo guarda en el CSV
                string lineaCSV = $"{nuevoJugador.Nombre},{nuevoJugador.Seleccion},{nuevoJugador.Posicion},{nuevoJugador.Goles},{nuevoJugador.Asistencias},{nuevoJugador.MinutosJugados},{nuevoJugador.PartidosDisputados},{nuevoJugador.Tarjetas}";
                File.AppendAllText("jugadores.csv", lineaCSV + Environment.NewLine);
            }
            catch (FormatException)
            {
                Console.WriteLine("\nError: Debe de ingresar números válidos para las estadísticas.");
            }
        }
    }
}