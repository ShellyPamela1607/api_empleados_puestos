using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_api_empresa.Models;
namespace web_api_empresa.Controllers{
[Route("api/[controller]")]
    public class ClientesController : Controller {
        private Conexion dbConexion;
        public ClientesController(){ dbConexion = Conectar.Create();        }
        // GET api/clientes
        [HttpGet]
        public ActionResult Get() {return Ok(dbConexion.Clientes.ToArray());}
        // GET api/clientes/1
        [HttpGet("{id}")]
         public async Task<ActionResult> Get(int id) {
             var clientes = await dbConexion.Clientes.FindAsync(id);
            if (clientes != null) {
                return Ok(clientes);
            } else {
                return NotFound();
            }
        }
        // POST api/actors
        //{"nit":"cf","nombres":"Miriam Lorena","apellidos":"Cardona Paiz","direccion":"Guatemala","telefono":"5555","fecha_nacimiento":"1990-01-01"}
         [HttpPost]
        public async Task<ActionResult> Post([FromBody] Clientes clientes){
            if (ModelState.IsValid){
             dbConexion.Clientes.Add(clientes);
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
    public async Task<ActionResult> Put([FromBody] Clientes clientes){
        var v_clientes = dbConexion.Clientes.SingleOrDefault(a => a.id_cliente == clientes.id_cliente);
        if (v_clientes != null && ModelState.IsValid) {
            dbConexion.Entry(v_clientes).CurrentValues.SetValues(clientes);
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
    var clientes = dbConexion.Clientes.SingleOrDefault(a => a.id_cliente == id);
    if(clientes!= null) {
        dbConexion.Clientes.Remove(clientes);
        await dbConexion.SaveChangesAsync();
                return Ok();
        } 
        else {    return NotFound();
        }
}

}

}