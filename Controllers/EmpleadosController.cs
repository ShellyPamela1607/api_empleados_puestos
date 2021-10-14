using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_empresa.Models;
namespace web_api_empresa.Controllers{
[Route("api/[controller]")]
    public class EmpleadosController : Controller {
        private Conexion dbConexion;
        public EmpleadosController(){ dbConexion = Conectar.Create();        }
        // GET api/empleados
        [HttpGet]
        public ActionResult Get() {return Ok(dbConexion.Empleados.ToArray());}
        // GET api/empleados/1
        [HttpGet("{id}")]
         public async Task<ActionResult> Get(int id) {
             var empleados = await dbConexion.Empleados.FindAsync(id);
            if (empleados != null) {
                return Ok(empleados);
            } else {
                return NotFound();
            }
        }
        // POST api/actors
        //{"nit":"cf","nombres":"Miriam Lorena","apellidos":"Cardona Paiz","direccion":"Guatemala","telefono":"5555","fecha_nacimiento":"1990-01-01"}
         [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleados empleados){
            if (ModelState.IsValid){
             dbConexion.Empleados.Add(empleados);
             await dbConexion.SaveChangesAsync();
             return Ok();
             //return Ok(clientes); retorna el registro ingresado
             //return Created("api/clientes",clientes); retorna los registros
             }else{
                 return BadRequest();
             }
             
        }


    // Update
    // PUT api/clientes/3
    //{"id_cliente":3,"nit":"cf","nombres":"Miriam","apellidos":"Paiz","direccion":"Guatemala","telefono":"5555","fecha_nacimiento":"1990-01-01"}
    public async Task<ActionResult> Put([FromBody] Empleados empleados){
        var v_empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleado == empleados.id_empleado);
        if (v_empleados != null && ModelState.IsValid) {
            dbConexion.Entry(v_empleados).CurrentValues.SetValues(empleados);
            await dbConexion.SaveChangesAsync();
            //return Created("api/clientes",clientes);
                return Ok();
            } else {
                return BadRequest();
            }
    }
//DELETE api/clientes/3
[HttpDelete("{id}")]
public async Task<ActionResult> Delete(int id) {
    var empleados = dbConexion.Empleados.SingleOrDefault(a => a.id_empleado == id);
    if(empleados!= null) {
        dbConexion.Empleados.Remove(empleados);
        await dbConexion.SaveChangesAsync();
                return Ok();
        } 
        else {    return NotFound();
        }
}

}

}