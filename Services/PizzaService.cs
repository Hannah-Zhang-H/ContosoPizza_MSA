using ContosoPizza.Models;
namespace ContosoPizza.Services;

public static class PizzaService
{
  static List<Pizza> Pizzas { get; }
  static int nextId = 3;
  // constructor
  static PizzaService()
  {
    Pizzas = new List<Pizza>
    {
      new Pizza {Id = 1, Name = "Classic Italian", IsGlutenFree = false},
      new Pizza {Id = 2, Name= "Veggie",  IsGlutenFree = true}
    };
  }



  // 获取所有pizza列表
  public static List<Pizza> GetAll() => Pizzas;
  // 获取指定id的pizza
  public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

  // 向所有pizza列表添加一个pizza
  public static void Add(Pizza pizza)
  {
    pizza.Id = nextId++;
    Pizzas.Add(pizza);
  }


  // 删除一个pizza
  public static void Delete(int id)
  {
    var pizza = Get(id);
    if (pizza is null)
      return;

    Pizzas.Remove((Pizza)pizza);
  }

  // 修改某个pizza
  public static void Update(Pizza pizza)
  {
    var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
    if (index == -1)
    {
      return;
    }
    Pizzas[index] = pizza;
  }


}