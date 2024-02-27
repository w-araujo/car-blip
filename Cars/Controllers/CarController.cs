using Cars.Interfaces;
using Cars.Models;
using Microsoft.AspNetCore.Mvc;
using Cars.Database;

namespace Cars.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly DbConnection _dbConnection;

        public CarController(ILogger<CarController> logger, DbConnection dbConnection)
        {
            _logger = logger;
            _dbConnection = dbConnection;
        }

        [HttpPost]
        public IActionResult CreateCar([FromBody] Car newCar)
        {
            try
            {
                _dbConnection.Car.Add(newCar);
                _dbConnection.SaveChanges();
                return Ok(newCar);
            } catch (Exception ex)
            {
                return BadRequest($"Erro ao criar o carro: {ex.Message}");
            }

        }

        [HttpGet(Name = "Car")]
        public IActionResult GetCars()
        {
            try
            {
                var cars = _dbConnection.Set<Car>().ToList();
                Console.WriteLine(_logger);
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar os carros: {ex.Message}");
            }
        }

        [HttpGet("brand/{brand}", Name = "GetCarsByBrand")]
        public IActionResult GetCarsByBrand(string brand)
        {
            try
            {
                var cars = _dbConnection.Set<Car>().Where(c => c.Brand == brand).ToList();
                if (cars.Count == 0)
                {
                    return NotFound($"Carros da marca: {brand} não foram encontrados.");
                }

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar os carros: {ex.Message}");
            }
        }

        [HttpGet("year/{year}", Name = "GetCarsByYear")]
        public IActionResult GetCarsByYear(int year)
        {
            try
            {
                var cars = _dbConnection.Set<Car>().Where(c => c.Year.Year == year).ToList();
                if (cars.Count == 0)
                {
                    return NotFound($"Carros do ano: {year} não foram encontrados.");
                }

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar os carros: {ex.Message}");
            }
        }

        [HttpGet("price/{price}", Name = "GetCarsByPrice")]
        public IActionResult GetCars(decimal price)
        {
            try
            {
                var cars = _dbConnection.Set<Car>().Where(c => c.Price == price).ToList();
                if (cars.Count == 0)
                {
                    return NotFound($"Carros com o preço: R${price} não foram encontrados.");
                }

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar os carros: {ex.Message}");
            }
        }

        [HttpPut("{id}", Name = "UpdatedCar")]
        public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
        {
            try
            {
                var existingCar = _dbConnection.Set<Car>().Find(id);

                if (existingCar == null)
                {
                    return NotFound($"Carro com ID {id} não encontrado.");
                }

                existingCar.Brand = updatedCar.Brand;
                existingCar.Year = updatedCar.Year;
                existingCar.Model = updatedCar.Model;
                existingCar.Price = updatedCar.Price;
                existingCar.Image = updatedCar.Image;
                existingCar.UpdatedAt = updatedCar.UpdatedAt;

                _dbConnection.SaveChanges();

                return Ok(existingCar);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o carro: {ex.Message}");

            }

        }

        [HttpDelete("{id}", Name = "DeletedCar")]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                var carToDelete = _dbConnection.Set<Car>().Find(id);

                if (carToDelete == null)
                {
                    return NotFound($"Carro com ID {id} não encontrado.");
                }

                _dbConnection.Set<Car>().Remove(carToDelete);
                _dbConnection.SaveChanges();

                return Ok($"Carro com ID {id} removido com sucesso.");
            }

            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir o carro: {ex.Message}");
            }


        }
    }
}
