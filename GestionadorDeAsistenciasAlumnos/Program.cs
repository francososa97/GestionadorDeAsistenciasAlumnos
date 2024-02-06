using GestionadorDeAsistenciasAlumnos.Models;
using System.Text.RegularExpressions;

namespace GestionadorDeAsistenciasAlumnos;

internal class Program
{
    static void Main(string[] args)
    {
        ReedListaAlumnosPresentes();
    }

    private static void ReedListaAlumnosPresentes()
    {
        string rutaRelativa = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\input\alumnos.txt");
        try
        {
            string contenido = File.ReadAllText(rutaRelativa);
            List<Alumno> CursoCoder = BuildListadoAlumnos();
            List<Alumno> alumnosPresentes = BuildStringToListAlumnos(contenido);
            List<Alumno> alumnosPresentesPorComision =  ValidateAlumnosPresnetes(alumnosPresentes, CursoCoder);
            LogAlumnos(alumnosPresentesPorComision);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer el archivo:");
            Console.WriteLine(ex.Message);
        }
    }

    private static void LogAlumnos(List<Alumno> alumnosPresentesPorComision)
    {
        List<string> listadoAlumnos = new List<string>();
        var alumnosPresnetes = alumnosPresentesPorComision.Where(x => x.EstaPresente).ToList();
        var alumnosAusentes = alumnosPresentesPorComision.Where(x => !x.EstaPresente).ToList();

        listadoAlumnos.Add($"Alumnos Presentes:{alumnosPresnetes.Count()}");
        listadoAlumnos.Add("---------------------------------");
        alumnosPresnetes.ForEach(x => listadoAlumnos.Add($"{x.Nombre} Presente"));
        listadoAlumnos.Add("---------------------------------");
        listadoAlumnos.Add($"Alumnos Ausentes:{alumnosAusentes.Count()}");
        listadoAlumnos.Add("---------------------------------");
        alumnosAusentes.ForEach(x => listadoAlumnos.Add($"{x.Nombre} Ausente"));


        // Convertir el listado de alumnos en una cadena de texto
        string contenidoArchivo = string.Join(Environment.NewLine, listadoAlumnos);
        try
        {
            // Ruta del archivo de salida (Output.txt en el mismo directorio que el archivo de entrada)
            string rutaArchivoSalida = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\input\Output.txt");

            // Escribir el contenido en el archivo de salida
            File.WriteAllText(rutaArchivoSalida, contenidoArchivo);

            Console.WriteLine("El listado de alumnos se ha guardado correctamente en Output.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo de salida: {ex.Message}");
        }
    }

    private static List<Alumno> ValidateAlumnosPresnetes(List<Alumno> alumnosPresentes, List<Alumno> alumnosComision)
    {
        List<Alumno> AlumnosPresentesEnComision = new List<Alumno>();
        alumnosComision.ForEach(alumnoEnComision =>
        {
            if(alumnoEnComision.Nombre == "Alexis Perez Beltran")
            {
                var test = alumnosPresentes.Where(alumno => alumno.Nombre == "Alexis Perez Beltran").FirstOrDefault();
                Console.WriteLine(test);
            }
            if (alumnoEnComision.Nombre == "Carla Vergara Lizárraga")
            {
                var test = alumnosPresentes.Where(alumno => alumno.Nombre == "Carla Vergara Lizárraga").FirstOrDefault();
                Console.WriteLine(test);
            }
            if (alumnoEnComision.Nombre == "Albrech Bruno Arias")
            {
                var test = alumnosPresentes.Where(alumno => alumno.Nombre == "Albrech Bruno Arias").FirstOrDefault();
                Console.WriteLine(test);
            }
            


            var alumno = alumnosPresentes.Where(alumno => alumno.Nombre == alumnoEnComision.Nombre).FirstOrDefault();
            if(alumno != null)
            {
                AlumnosPresentesEnComision.Add(alumno);
            }
            else
            {
                var nombrePartes = alumnoEnComision.Nombre.Split(" ");
                alumno = alumnosPresentes.Where(alumno => alumno.Nombre == $"{nombrePartes[0]} {nombrePartes[1]}").FirstOrDefault();
                if (alumno != null)
                {
                    AlumnosPresentesEnComision.Add(alumno);
                }
                else
                {
                    alumnoEnComision.EstaPresente = false;
                    AlumnosPresentesEnComision.Add(alumnoEnComision);
                }
            }
        });
        return AlumnosPresentesEnComision;
    }

    private static List<Alumno> BuildStringToListAlumnos(string contenido)
    {
        List<Alumno> nombresAlumnosPresnetes = new List<Alumno>();
        string patron = @"\b\w+\s\w+\b";
        MatchCollection nombresValidos = Regex.Matches(contenido, patron);
        foreach (Match nombre in nombresValidos)
        {
            Alumno alumno = new Alumno() {Nombre= nombre.Value, EstaPresente=true };
            nombresAlumnosPresnetes.Add(alumno);
        }

        return nombresAlumnosPresnetes;
    }

    private static List<Alumno> BuildListadoAlumnos()
    {
        List<Alumno> CursoCoder = new List<Alumno>
        {
            new Alumno() { Nombre = "Adriel Bruno", EstaPresente = false },
            new Alumno() { Nombre = "Agustin Coronel", EstaPresente = false },
            new Alumno() { Nombre = "Agustin Meggiolaro", EstaPresente = false },
            new Alumno() { Nombre = "Agustín Ulises Grismado Coronel", EstaPresente = false },
            new Alumno() { Nombre = "Alan Gomez", EstaPresente = false },
            new Alumno() { Nombre = "Albrech Bruno Arias", EstaPresente = false },
            new Alumno() { Nombre = "Alejandro Maiolo", EstaPresente = false },
            new Alumno() { Nombre = "Alejo Palma", EstaPresente = false },
            new Alumno() { Nombre = "Alex Pastorek", EstaPresente = false },
            new Alumno() { Nombre = "Alexander Arriagada", EstaPresente = false },
            new Alumno() { Nombre = "Alexis Perez Beltran", EstaPresente = false },
            new Alumno() { Nombre = "Alvaro Villena", EstaPresente = false },
            new Alumno() { Nombre = "Ana Zeballos", EstaPresente = false },
            new Alumno() { Nombre = "Analia Puyol", EstaPresente = false },
            new Alumno() { Nombre = "Andres Camacho", EstaPresente = false },
            new Alumno() { Nombre = "Andrés Mangold", EstaPresente = false },
            new Alumno() { Nombre = "Anibal Ayala", EstaPresente = false },
            new Alumno() { Nombre = "Augusto Cuccione", EstaPresente = false },
            new Alumno() { Nombre = "Augusto Galarza", EstaPresente = false },
            new Alumno() { Nombre = "Benjamin Pipolo", EstaPresente = false },
            new Alumno() { Nombre = "Braian Evora", EstaPresente = false },
            new Alumno() { Nombre = "Braian Ledantes", EstaPresente = false },
            new Alumno() { Nombre = "Braulio Solari", EstaPresente = false },
            new Alumno() { Nombre = "Bruno Panozzo", EstaPresente = false },
            new Alumno() { Nombre = "Carina Diaz", EstaPresente = false },
            new Alumno() { Nombre = "Carla Vergara Lizárraga", EstaPresente = false },
            new Alumno() { Nombre = "Carlos Fernandez Paolini", EstaPresente = false },
            new Alumno() { Nombre = "Carolina Semprini", EstaPresente = false },
            new Alumno() { Nombre = "Ciro Giorgini", EstaPresente = false },
            new Alumno() { Nombre = "Claudio Fuentes", EstaPresente = false },
            new Alumno() { Nombre = "Conrado Levanti", EstaPresente = false }
        };
        return CursoCoder;
    }
}