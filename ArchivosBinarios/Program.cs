using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivoBinarioEmpleados
    {
        //declaracion de flujos
        BinaryWriter bw = null; //flujo salida- escritura de datos
        BinaryReader br = null; //flujo entrada- lectura de datos

        //campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SaliarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //variable local
            char resp;

            try
            {
                //creacion del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    NumEmp = int.Parse(Console.ReadLine());
                    Console.Write("Nombre del empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion del empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono del empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados del empleado: ");
                    DiasTrabajados = int.Parse(Console.ReadLine());
                    Console.Write("Salario diario del empleado: ");
                    SaliarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos al archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SaliarioDiario);
                    Console.Write("\n\nDeseas almacenar otro registro (s/n)? ");

                    resp = char.Parse(Console.ReadLine());
                } while ((resp == 'S') || (resp == 's'));
            }
            catch(IOException e)
            {
                Console.WriteLine("\nError: " + e.Message);
                Console.WriteLine("\nRuta: " + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close();
                Console.Write("\nPresione enter para terminar la Escritura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //verifica si existe el archivo
                if(File.Exists(Archivo))
                {
                    //creacion de flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                    //despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //lectura de registros mientras no llegue a EndofFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SaliarioDiario = br.ReadSingle();

                        //muestra los datos
                        Console.WriteLine("Numero del Empleado: " + NumEmp);
                        Console.WriteLine("Nombre del empleado: " + Nombre);
                        Console.WriteLine("Direccion del empleado: " + Direccion);
                        Console.WriteLine("Telefono del empleado: " + Telefono);
                        Console.WriteLine("Dias Trabajados del empleado: " + DiasTrabajados);
                        Console.WriteLine("Salario diario del empleado: " + SaliarioDiario);
                        Console.WriteLine("SUELDO TOTAL del Empleado: {0:C}", (DiasTrabajados * SaliarioDiario));
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl archivo " + Archivo + " No Existe en el Disco!!");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();
                }
            }
            catch(EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado Empleados");
                Console.Write("\nPresione <enter> para Continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close();
                Console.Write("\nPresione enter para terminar la Lectura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }
        static void Main(string[] args)
        {
            //declaracion de variables auxiliares
            string Arch = null;
            int option;

            //creacion del objeto
            ArchivoBinarioEmpleados Al = new ArchivoBinarioEmpleados();

            //menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n---ARCHIVO BINARIO EMPLEADOS---");
                Console.WriteLine("1.- Creacion de un Archivo");
                Console.WriteLine("2.- Lectura de un Archivo");
                Console.WriteLine("3.- Salida del Programa");
                Console.Write("\nQue opcion deseas? ");
                option = Int16.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();
                            //verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo (s/n)? ");
                                resp = char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                            {
                                Al.CrearArchivo(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //Bloque de lectura
                        try
                        {
                            //Captura nombre del archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                            Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("Presione <enter> para salir del Programa");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("Esa Opcion no Existe!!, Presione <enter> para continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (option != 3);
        }
    }
}
