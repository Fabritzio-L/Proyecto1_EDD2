using System;
using System.IO;

namespace Proyecto1_EDD2
{
    class Program
    {

        static void Main(string[] args)
        {
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
    }
}