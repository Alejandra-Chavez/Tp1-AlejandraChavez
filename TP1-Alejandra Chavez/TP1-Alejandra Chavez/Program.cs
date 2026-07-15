using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TP1_Alejandra_Chavez;

namespace TP1_Alejandra_Chavez
{
    /*Realizar un programa que represente una simulación de copos de nieve cayendo en la consola, utilizando el símbolo "*" para cada copo.
El programa debe cumplir con las siguientes condiciones:
Definir una clase Configuracion que almacene parámetros de la simulación, como la cantidad de filas, columnas y la velocidad de caída de los copos.
Definir una clase Copo que modele el comportamiento de un copo de nieve. Cada copo debe tener una posición en la consola y un método para mostrarse 
    y desplazarse hacia abajo.
Usar una lista para administrar todos los copos activos durante la simulación.
Implementar una lógica que controle la caída de los copos de nieve, evitando que se superpongan en la misma posición.
Al completarse una fila con copos en todas las columnas, esta debe eliminarse para permitir que continúe la simulación.
El programa debe ejecutarse en un ciclo continuo, simulando de manera animada la caída de los copos.*/
    class Configuracion
    {
        public int Filas { get; set; }
        public int Columnas { get; set; }
        public int Velocidad { get; set; }

        public Configuracion()
        {
            Filas = 20;
            Columnas = 40;
            Velocidad = 100;
        }
    }
    class Copo
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Copo(int x)
        {
            X = x;
            Y = 0;
        }

        public void Mostrar()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("*");
        }

        public void Borrar()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(" ");
        }

        public void Bajar()
        {
            Y++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Configuracion config = new Configuracion();

            List<Copo> copos = new List<Copo>();

            Random random = new Random();

            int[] columnas = { 5, 10, 15, 20, 25, 30 };

            bool[,] ocupadas = new bool[config.Filas, config.Columnas];

            DateTime ultimoMovimiento = DateTime.Now;

            while (true)
            {
                if ((DateTime.Now - ultimoMovimiento).TotalMilliseconds >= config.Velocidad)
                {
                    ultimoMovimiento = DateTime.Now;

                    int columna = columnas[random.Next(columnas.Length)];

                    if (!ocupadas[0, columna])
                    {
                        copos.Add(new Copo(columna));
                    }

                    for (int i = copos.Count - 1; i >= 0; i--)
                    {
                        Copo c = copos[i];

                        c.Borrar();

                        if (c.Y + 1 < config.Filas &&
                            !ocupadas[c.Y + 1, c.X])
                        {
                            c.Bajar();
                        }
                    }

                    ocupadas = new bool[config.Filas, config.Columnas];

                    foreach (Copo c in copos)
                    {
                        ocupadas[c.Y, c.X] = true;
                    }

                    for (int fila = config.Filas - 1; fila >= 0; fila--)
                    {
                        bool completa = true;

                        for (int j = 0; j < columnas.Length; j++)
                        {
                            if (!ocupadas[fila, columnas[j]])
                            {
                                completa = false;
                                }
                        }

                        if (completa)
                        {
                            for (int i = copos.Count - 1; i >= 0; i--)
                            {
                                if (copos[i].Y == fila)
                                {
                                    copos.RemoveAt(i);
                                }
                            }

                            foreach (Copo c in copos)
                            {
                                if (c.Y < fila)
                                {
                                    c.Y++;
                                }
                            }

                            ocupadas = new bool[config.Filas, config.Columnas];

                            foreach (Copo c in copos)
                            {
                                ocupadas[c.Y, c.X] = true;
                            }
                        }
                    }

                    Console.Clear();

                    foreach (Copo c in copos)
                    {
                        c.Mostrar();
                    }
                }
            }
        }
    }
}