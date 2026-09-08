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
                        EliminarJugador();
                        break;
                    case "5":
                        GenerarTop5();
                        break;
                    case "6":
                        MostrarListadoGeneral();
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
                Console.WriteLine($"\nEl jugador {nuevoJugador.Nombre} fue registrado.");

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

        static void EliminarJugador()
        {
            Console.WriteLine("\n--- ELIMINAR JUGADOR ---");
            Console.Write("Ingrese el nombre del jugador que desea eliminar: ");
            string nombre = Console.ReadLine().Trim();

            Console.Write($"¿Está totalmente seguro de eliminar a {nombre.ToUpper()}? (S/N): ");
            string confirmacion = Console.ReadLine().Trim().ToUpper();

            if (confirmacion == "S")
            {
                // Usa el metodo de eliminar del arbol
                bool exito = arbol.Eliminar(nombre);

                if (exito)
                {
                    // Sobrescribe el archivo para que el borrado sea permanente
                    GuardarCambiosCSV();
                    Console.WriteLine($"\nEl jugador {nombre} ha sido eliminado.");
                }
                else
                {
                    Console.WriteLine($"\nError: No se encontró al jugador {nombre}");
                }
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }
        }


        static void GenerarTop5()
        {
            Console.WriteLine("\n---TOP 5 DE JUGADORES---");
            Console.WriteLine("Seleccione la estadística a evaluar:");
            Console.WriteLine("1. Goles");
            Console.WriteLine("2. Asistencias");
            Console.WriteLine("3. Minutos Jugados");
            Console.WriteLine("4. Partidos Disputados");
            Console.WriteLine("5. Tarjetas");
            Console.Write("\nOpción: ");
            string opcionCat = Console.ReadLine();

            Categoria categoriaSeleccionada;
            switch (opcionCat)
            {
                case "1": categoriaSeleccionada = Categoria.Goles; break;
                case "2": categoriaSeleccionada = Categoria.Asistencias; break;
                case "3": categoriaSeleccionada = Categoria.MinutosJugados; break;
                case "4": categoriaSeleccionada = Categoria.PartidosDisputados; break;
                case "5": categoriaSeleccionada = Categoria.Tarjetas; break;
                default:
                    Console.WriteLine("Opción no válida. Se usará Goles por defecto.");
                    categoriaSeleccionada = Categoria.Goles;
                    break;
            }

            Console.WriteLine("\n¿Desea obtener los 5 más altos o los 5 más bajos?");
            Console.WriteLine("1. Los 5 más altos");
            Console.WriteLine("2. Los 5 más bajos");
            Console.Write("Opción: ");
            string tipoHeap = Console.ReadLine();

            // Prepara los heaps
            MaxHeap maxHeap = null;
            MinHeap minHeap = null;

            if (tipoHeap == "2")
                minHeap = new MinHeap(categoriaSeleccionada, 100);
            else
                maxHeap = new MaxHeap(categoriaSeleccionada, 100);

            // Extrae a todos los jugadores del Árbol B+
            NodoBPlus actual = arbol.ObtenerPrimeraHoja();
            if (actual == null)
            {
                Console.WriteLine("\nNo hay jugadores registrados.");
                return;
            }

            while (actual != null)
            {
                for (int i = 0; i < actual.CantidadClaves; i++)
                {
                    // Inserta en el Heap correspondiente
                    if (tipoHeap == "2")
                        minHeap.Insertar(actual.Jugadores[i]);
                    else
                        maxHeap.Insertar(actual.Jugadores[i]);
                }
                actual = actual.Siguiente;
            }

            // 3. Imprime el resultado
            Console.WriteLine($"\n--- TOP 5 POR {categoriaSeleccionada.ToString().ToUpper()} ---");
            Console.WriteLine(new string('-', 98));
            Console.WriteLine($"     {"NOMBRE",-18} | {"SELECCIÓN",-12} | {"POSICIÓN",-13} | GLS | ASI | MIN  | TAR");
            Console.WriteLine(new string('-', 98));

            // Extrae 5 veces 
            for (int i = 0; i < 5; i++)
            {
                Jugador top = null;
                if (tipoHeap == "2" && minHeap.Cantidad > 0)
                {
                    top = minHeap.ExtraerMinimo();
                }
                else if (tipoHeap != "2" && maxHeap.Cantidad > 0)
                {
                    top = maxHeap.ExtraerMaximo();
                }

                // Imprime el jugador encontrado con su posición del 1 al 5
                if (top != null)
                {
                    Console.Write($"{i + 1}.- ");
                    top.Imprimir();
                }
            }
            Console.WriteLine(new string('-', 98));
        }
        static void MostrarListadoGeneral()
        {
            Console.WriteLine("\n--- LISTADO GENERAL DE JUGADORES---");
            Console.WriteLine("Seleccione la estadística para ordenar el listado:");
            Console.WriteLine("1. Goles");
            Console.WriteLine("2. Asistencias");
            Console.WriteLine("3. Minutos Jugados");
            Console.WriteLine("4. Partidos Disputados");
            Console.WriteLine("5. Tarjetas");
            Console.Write("\nOpción: ");
            string opcionCat = Console.ReadLine();

            Categoria categoriaSeleccionada;

            // Asigna la categoria seleccionada
            switch (opcionCat)
            {
                case "1": categoriaSeleccionada = Categoria.Goles; break;
                case "2": categoriaSeleccionada = Categoria.Asistencias; break;
                case "3": categoriaSeleccionada = Categoria.MinutosJugados; break;
                case "4": categoriaSeleccionada = Categoria.PartidosDisputados; break;
                case "5": categoriaSeleccionada = Categoria.Tarjetas; break;
                default:
                    Console.WriteLine("Opción no válida. Se ordenará por Goles por defecto.");
                    categoriaSeleccionada = Categoria.Goles;
                    break;
            }


            //Instancia el arbol AVL pasándole la categoría
            ArbolAVL arbolAVL = new ArbolAVL(categoriaSeleccionada);

            // Extrae a todos los jugadores del Árbol B+ usando la lista doblemente enlazada
            NodoBPlus actual = arbol.ObtenerPrimeraHoja();
            
            if (actual == null)
            {
                Console.WriteLine("\nNo hay jugadores registrados.");
                return;
            }

            while (actual != null)
            {
                for (int i = 0; i < actual.CantidadClaves; i++)
                {
                    // Pasa cada jugador al AVL para que lo acomode
                    arbolAVL.Insertar(actual.Jugadores[i]);
                }
                actual = actual.Siguiente; // Salta rápido a la siguiente hoja
            }

            // 3. Imprime el resultado usando recorrido inorden del AVL
            Console.WriteLine($"\n--- JUGADORES ORDENADOS POR {categoriaSeleccionada.ToString().ToUpper()} ---");            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($" {"NOMBRE",-18} | {"SELECCIÓN",-12} | {"POSICIÓN",-13} | {"GLS",-2} | {"ASI",-2} | {"MIN",-4} | {"TAR",-2}");
            Console.WriteLine("--------------------------------------");
            arbolAVL.MostrarOrdenado();
            
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