using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_empresa.Models;
namespace web_api_empresa.Controllers{
[Route("api/[controller]")]
    public class PuestosController : Controller {
        private Conexion dbConexion;
        public PuestosController(){ dbConexion = Conectar.Create();        }
        // GET api/empleados
        [HttpGet]
        public ActionResult Get() {return Ok(dbConexion.Puestos.ToArray());}
        // GET api/empleados/1
        [HttpGet("{id}")]
         public async Task<ActionResult> Get(int id) {
             var puestos = await dbConexion.Puestos.FindAsync(id);
            if (puestos != null) {
                return Ok(puestos);
            } else {
                return NotFound();
            }
        }
        // POST api/actors
        //{"nit":"cf","nombres":"Miriam Lorena","apellidos":"Cardona Paiz","direccion":"Guatemala","telefono":"5555","fecha_nacimiento":"1990-01-01"}
         [HttpPost]
        public async Task<ActionResult> Post([FromBody] Puestos puestos){
            if (ModelState.IsValid){
             dbConexion.Puestos.Add(puestos);
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
    public async Task<ActionResult> Put([FromBody] Puestos puestos){
        var v_puestos = dbConexion.Puestos.SingleOrDefault(a => a.id_puesto == puestos.id_puesto);
        if (v_puestos != null && ModelState.IsValid) {
            dbConexion.Entry(v_puestos).CurrentValues.SetValues(puestos);
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
    var puestos = dbConexion.Puestos.SingleOrDefault(a => a.id_puesto == id);
    if(puestos!= null) {
        dbConexion.Puestos.Remove(puestos);
        await dbConexion.SaveChangesAsync();
                return Ok();
        } 
        else {    return NotFound();
        }
}

}

}