using System;

namespace Proyecto1_EDD2
{
    public class ArbolBPlus
    {
        private NodoBPlus Raiz;
        private int Grado;

        public ArbolBPlus(int grado = 5)
        {
            Grado = grado;
            // El árbol siempre nace con una raíz que es hoja
            Raiz = new NodoBPlus(Grado, true); 
        }

        //Metodo de busqueda
        public Jugador Buscar(string nombreBuscado)
        {
            if (Raiz == null || Raiz.CantidadClaves == 0) return null;

            NodoBPlus actual = Raiz;

            // Navegar por los nodos internos hasta llegar a la hoja correcta
            while (!actual.EsHoja)
            {
                int i = 0;
                // Compara alfabéticamente. 
                // Avanza mientras el nombre buscado sea "mayor" que la clave actual
                while (i < actual.CantidadClaves && 
                       string.Compare(nombreBuscado, actual.Claves[i], StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    i++;
                }
                // Baja al hijo correspondiente
                actual = actual.Hijos[i];
            }

            //Buscar dentro de la hoja encontrada
            for (int i = 0; i < actual.CantidadClaves; i++)
            {
                if (actual.Claves[i].Equals(nombreBuscado, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Jugadores[i]; // Retorna el jugador encontrado
                }
            }

            return null; // El jugador no existe
        }

        public void Insertar(Jugador nuevoJugador)
        {
            string clavePromovida = ""; // Atrapa el nombre que quede arriba
            NodoBPlus nuevoHijo = InsertarRecursivo(Raiz, nuevoJugador.Nombre, nuevoJugador, ref clavePromovida);

            // Si la raíz original se llenó y se partió el árbol crece un nivel hacia arriba
            if (nuevoHijo != null)
            {
                NodoBPlus nuevaRaiz = new NodoBPlus(Grado, false);
                nuevaRaiz.Claves[0] = clavePromovida;
                nuevaRaiz.Hijos[0] = Raiz;
                nuevaRaiz.Hijos[1] = nuevoHijo;
                nuevaRaiz.CantidadClaves = 1;
                
                Raiz = nuevaRaiz;
            }
        }

        private NodoBPlus InsertarRecursivo(NodoBPlus nodo, string clave, Jugador jugador, ref string clavePromovida)
        {
            if (nodo.EsHoja)
            {
                // Si hay espacio en la hoja
                if (nodo.CantidadClaves < nodo.Grado - 1)
                {
                    InsertarEnHoja(nodo, clave, jugador);
                    return null; 
                }
                // Si la hoja está llena hay que partirla
                else
                {
                    return DividirHoja(nodo, clave, jugador, ref clavePromovida);
                }
            }
            else
            {
                // Buscar por que rama bajar
                int i = 0;
                while (i < nodo.CantidadClaves && string.Compare(clave, nodo.Claves[i], StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    i++;
                }

                // Llamada recursiva
                string clavePromovidaHijo = "";
                NodoBPlus nuevoHijo = InsertarRecursivo(nodo.Hijos[i], clave, jugador, ref clavePromovidaHijo);

                // Si el hijo se dividio mandó un nuevo nodo que se debe de acomodar aquí
                if (nuevoHijo != null)
                {
                    if (nodo.CantidadClaves < nodo.Grado - 1)
                    {
                        InsertarEnInterno(nodo, clavePromovidaHijo, nuevoHijo);
                        return null;
                    }
                    else
                    {
                        return DividirInterno(nodo, clavePromovidaHijo, nuevoHijo, ref clavePromovida);
                    }
                }
                return null;
            }
        }

        //Insertar en hoja y en nodo interno
        private void InsertarEnHoja(NodoBPlus hoja, string clave, Jugador jugador)
        {
            int i = hoja.CantidadClaves - 1;
            // Desplazar elementos a la derecha para hacer espacio
            while (i >= 0 && string.Compare(clave, hoja.Claves[i], StringComparison.OrdinalIgnoreCase) < 0)
            {
                hoja.Claves[i + 1] = hoja.Claves[i];
                hoja.Jugadores[i + 1] = hoja.Jugadores[i];
                i--;
            }
            hoja.Claves[i + 1] = clave;
            hoja.Jugadores[i + 1] = jugador;
            hoja.CantidadClaves++;
        }

        private void InsertarEnInterno(NodoBPlus interno, string clave, NodoBPlus hijoDerecho)
        {
            int i = interno.CantidadClaves - 1;
            while (i >= 0 && string.Compare(clave, interno.Claves[i], StringComparison.OrdinalIgnoreCase) < 0)
            {
                interno.Claves[i + 1] = interno.Claves[i];
                interno.Hijos[i + 2] = interno.Hijos[i + 1];
                i--;
            }
            interno.Claves[i + 1] = clave;
            interno.Hijos[i + 2] = hijoDerecho;
            interno.CantidadClaves++;
        }

        // Dividir nodos hoja e internos
        private NodoBPlus DividirHoja(NodoBPlus hoja, string clave, Jugador jugador, ref string clavePromovida)
        {
            int total = hoja.Grado;
            string[] tempClaves = new string[total];
            Jugador[] tempJugadores = new Jugador[total];

            // Copiar a arreglos temporales ordenadamente
            int i = 0, j = 0;
            while (i < hoja.CantidadClaves && string.Compare(clave, hoja.Claves[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                tempClaves[j] = hoja.Claves[i]; tempJugadores[j] = hoja.Jugadores[i];
                i++; j++;
            }
            tempClaves[j] = clave; tempJugadores[j] = jugador; j++;
            while (i < hoja.CantidadClaves)
            {
                tempClaves[j] = hoja.Claves[i]; tempJugadores[j] = hoja.Jugadores[i];
                i++; j++;
            }

            // Partir a la mitad
            int mitad = total / 2;
            hoja.CantidadClaves = mitad;
            
            NodoBPlus nuevaHoja = new NodoBPlus(hoja.Grado, true);
            nuevaHoja.CantidadClaves = total - mitad;

            for (int k = 0; k < hoja.CantidadClaves; k++)
            {
                hoja.Claves[k] = tempClaves[k];
                hoja.Jugadores[k] = tempJugadores[k];
            }
            for (int k = 0; k < nuevaHoja.CantidadClaves; k++)
            {
                nuevaHoja.Claves[k] = tempClaves[mitad + k];
                nuevaHoja.Jugadores[k] = tempJugadores[mitad + k];
            }

            // Actualizar punteros de la lista doblemente enlazada
            nuevaHoja.Siguiente = hoja.Siguiente;
            nuevaHoja.Anterior = hoja;
            if (hoja.Siguiente != null)
                hoja.Siguiente.Anterior = nuevaHoja;
            hoja.Siguiente = nuevaHoja;

            // Copiar clave hacia arriba
            clavePromovida = nuevaHoja.Claves[0];
            return nuevaHoja;
        }

        // Dividir nodos internos
        private NodoBPlus DividirInterno(NodoBPlus interno, string clave, NodoBPlus hijo, ref string clavePromovida)
        {
            int totalClaves = interno.Grado;
            string[] tempClaves = new string[totalClaves];
            NodoBPlus[] tempHijos = new NodoBPlus[totalClaves + 1];

            // Acomodo similar con arreglos temporales
            int i = 0, j = 0;
            while (i < interno.CantidadClaves && string.Compare(clave, interno.Claves[i], StringComparison.OrdinalIgnoreCase) >= 0)
            {
                tempClaves[j] = interno.Claves[i]; tempHijos[j] = interno.Hijos[i];
                i++; j++;
            }
            tempClaves[j] = clave; tempHijos[j] = interno.Hijos[i]; tempHijos[j + 1] = hijo; j++;
            while (i < interno.CantidadClaves)
            {
                tempClaves[j] = interno.Claves[i]; tempHijos[j + 1] = interno.Hijos[i + 1];
                i++; j++;
            }

            int mitad = totalClaves / 2;
            interno.CantidadClaves = mitad;
            NodoBPlus nuevoInterno = new NodoBPlus(interno.Grado, false);
            nuevoInterno.CantidadClaves = totalClaves - mitad - 1;

            for (int k = 0; k < interno.CantidadClaves; k++)
            {
                interno.Claves[k] = tempClaves[k];
                interno.Hijos[k] = tempHijos[k];
            }
            interno.Hijos[interno.CantidadClaves] = tempHijos[interno.CantidadClaves];

            // La clave de en medio sube y abandona el nodo
            clavePromovida = tempClaves[mitad]; 

            for (int k = 0; k < nuevoInterno.CantidadClaves; k++)
            {
                nuevoInterno.Claves[k] = tempClaves[mitad + 1 + k];
                nuevoInterno.Hijos[k] = tempHijos[mitad + 1 + k];
            }
            nuevoInterno.Hijos[nuevoInterno.CantidadClaves] = tempHijos[totalClaves];

            return nuevoInterno;
        }

        // Devuelve la primera hoja para recorrer todos los datos rápido
        public NodoBPlus ObtenerPrimeraHoja()
        {
            if (Raiz == null) return null;

            NodoBPlus actual = Raiz;
            while (!actual.EsHoja)
            {
                actual = actual.Hijos[0]; // Baja siempre por la izquierda
            }
            return actual;
        }
    }

}
