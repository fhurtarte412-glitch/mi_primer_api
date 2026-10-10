using mi_primer_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace mi_primer_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : Controller
    {
        private readonly DbEmpresaContext _context;
        public ClientesController(DbEmpresaContext ctx)
        {
            _context = ctx;
        }
        [HttpGet("ListarUsuarios")]
        public ActionResult<List<Cliente>> ListarClientes()
        {
            return _context.Clientes.ToList();
        }

        [HttpGet("obtenerUsuarios/{id}")]
        public ActionResult<Cliente> obtenerUsuario(int id)
        {
            return _context.Clientes.FirstOrDefault(e => e.IdClientes == id);
        }

        [HttpGet("Insertar Cliente")]
        public IActionResult Crear(string nombre, string apellido, string telefono)
        {
            Cliente nuevoCliente = new Cliente
            {
                Nombre = nombre,
                Apellido = apellido,
                Telefono = telefono
            };

            _context.Clientes.Add(nuevoCliente);
            _context.SaveChanges();

            return Ok(new { mensaje = "Cliente creado con éxito", cliente = nuevoCliente });
        }

        [HttpGet("Actualizar/{id}")]
        public IActionResult Actualizar(int id, string nombre, string apellido, string telefono)
        {
            var clienteActual = _context.Clientes.Find(id);

            if (clienteActual == null)
            {
                return NotFound(new { mensaje = "El cliente no existe." });
            }

            clienteActual.Nombre = nombre;
            clienteActual.Apellido = apellido;
            clienteActual.Telefono = telefono;

            _context.Clientes.Update(clienteActual);
            _context.SaveChanges();

            return Ok(new { mensaje = "Cliente actualizado con éxito", clienteActual });
        }
    }
}
