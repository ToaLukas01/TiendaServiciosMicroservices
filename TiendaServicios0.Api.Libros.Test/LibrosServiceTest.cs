using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios0.Api.Libro.Aplicacion;
using TiendaServicios0.Api.Libro.Contexto;
using TiendaServicios0.Api.Libro.Models;
using Xunit;

namespace TiendaServicios0.Api.Libros.Test
{
    public class LibrosServiceTest
    {
        // crearemos una clase que utiliza la libreria "GenFu" para simular datos falsos para los Test
        private IEnumerable<LibreriaMaterial> ObtenerDataTest()
        {
            // aqui indicamos que los datos que se generen se basaran en la estructura de clase que le hemos pasado
            A.Configure<LibreriaMaterial>()
                .Fill(l => l.Titulo).AsArticleTitle()
                .Fill(l => l.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(10);
            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
            //return (IEnumerable<LibroDto>)lista;
        }


        private Mock<ContextoLibreria> CrearContexto()
        {
            // creamos una variable que represente los datos de prueba que hemos creado con otro medoto
            var dataPrueba = ObtenerDataTest().AsQueryable();

            // creamos un "Mock" que represente un modelo de base de datos, y le indicamos en que clase modelo debe basarse
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            // crearemos una instancia del contexto
            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.libreriaMaterial).Returns(dbSet.Object);
            return contexto;
        }

        [Fact]
        public async void GetLibroID()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });
            var mockMapper = mapConfig.CreateMapper();

            var request = new FiltrarLibros.DetalleLibro();
            request.LibroId = Guid.Empty;

            var manejador = new FiltrarLibros.ManejadorDetalleLibro(mockContexto.Object, mockMapper);

            var libroID = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libroID);
            Assert.True(libroID.LibreriaMaterialId == Guid.Empty);
        }




        [Fact] // esta notacion "Fact" combierte este metodo a uno de tipo testing
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();

            // que metodo dentro de mi microservice Libros
            // se esta encargando de realizar la consulta de libros a la base de datos ?

            // lo que debemos hacer:
            // 1- Emular a la instancia de EntityFrameworkCore (Contexto de la base de datos)
            //  para emular las acciones y eventos de un objeto en un hambiente de unit test
            //  utilizamos obejtos de tipo "mock"(objeto que representa cualquier elemento del codigo)
            //  (debemos instalar su libreria "moq" en los paquetes NuGet para poder implmentarlos)
            //var mockContexto = new Mock<ContextoLibreria>();
            var mockContexto = CrearContexto();

            // 2- Emular al "IMapper"
            //var mockMapper = new Mock<IMapper>();
            var mapConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingTest());
            });
            var mockMapper = mapConfig.CreateMapper();

            // 3- Instanciar a la clase "Manejador" del controller que queremos testear
            //   y pasarle los Mocks que creamos
            ConsultarLibros.ManejadorConsultarLibreria manejador = new ConsultarLibros.ManejadorConsultarLibreria(mockContexto.Object, mockMapper);

            ConsultarLibros.ConsultarLibreria request = new ConsultarLibros.ConsultarLibreria();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            // este ultimo metodo realiza toda la evaluacion
            Assert.True(lista.Any());

        }


        [Fact]
        public async void CrearLibro()
        {
            // para este metodo crearemos una base de datos fake en memoria
            // (osea que solo existira en momento de ejecucion)
            // para realizar todas las pruebas de insercion de creacion de instancias de los modelos en la base de datos
            
            // creamso un objeto que represente la configuracion de la base de datos local
            var optionsMemoria = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseLocalLibros")
                .Options;

            var localMemoria = new ContextoLibreria(optionsMemoria);

            var requiest = new NuevoLibro.CrearLibro();
            requiest.Titulo = "Libro Fake";
            requiest.AutorLibro = Guid.Empty;
            requiest.FechaPublicacion = DateTime.Now;

            var manejador = new NuevoLibro.ManejadorNuevoLibro(localMemoria);

            var newLibro = await manejador.Handle(requiest, new System.Threading.CancellationToken());

            Assert.True(newLibro != null);
        }

    }
}
