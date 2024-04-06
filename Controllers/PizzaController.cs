using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

//this controller handles requests to https://localhost:{PORT}/pizza.
[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
  public PizzaController()
  {

  }


  // GET all action
  [HttpGet]
  public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();


  // GET by Id action
  [HttpGet("{id}")]
  public ActionResult<Pizza> Get(int id)
  {
    var pizza = PizzaService.Get(id);
    if (pizza == null) return NotFound();  // 则返回 HTTP 404 Not Found 响应。
    return Ok(pizza);  // 如果成功获取到 pizza，就返回 HTTP 200 OK 响应，并将 pizza 对象作为响应的内容返回。
  }

  // POST action
  [HttpPost]
  /*
  IActionResult lets the client know if the request succeeded 
  and provides the ID of the newly created pizza. 

  IActionResult uses standard HTTP status codes, 
  so it can easily integrate with clients regardless of the language or platform they're running on.
  */
  public IActionResult Create(Pizza pizza)
  {
    PizzaService.Add(pizza);
    /*
        这行代码是ASP.NET Core Web API中的一个方法调用，具体含义如下：

        CreatedAtAction: 这是一个ASP.NET Core Web API的方法，用于创建一个HTTP 201 Created响应，表示已成功创建了资源。它允许您指定一个控制器的操作来获取新创建资源的位置。

        nameof(Get): nameof操作符用于获取指定方法的名称，这里Get应该是控制器中用于获取资源的方法的名称。

        new { id = pizza.Id }: 这部分是一个匿名对象，用于指定新创建资源的位置。在这里，id是资源的标识符，pizza.Id是新创建的资源的ID。

        pizza: 这是新创建的资源对象，将在响应主体中返回给客户端。

        因此，整个语句的含义是将新创建的 pizza 资源插入到内存缓存中，并返回一个HTTP 201 Created响应，其中包含了新创建资源的位置信息。
    */
    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza); //将请求主体的Pizza对象插入内存缓存中
  }

  // PUT action
  [HttpPut("{id}")]
  public IActionResult Update(int id, Pizza pizza)
  {
    if (id != pizza.Id)
      return BadRequest();

    var existingPizza = PizzaService.Get(id);
    if (existingPizza is null) // 新写法
    {
      return NotFound();
    }

    PizzaService.Update(pizza);
    return NoContent();
  }

  // DELETE action
  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    // This code will delete the pizza and return a result
    var pizza = PizzaService.Get(id);
    if (pizza is null) return NotFound();

    PizzaService.Delete(id);
    return NoContent();
  }
}