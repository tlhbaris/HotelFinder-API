using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelservice;
        public HotelsController(IHotelService hotelService)
        {
            _hotelservice = hotelService;
        }

        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelservice.GetAllHotels();
            return Ok(hotels); // 200 + data    
        }
        /// <summary>
        /// Get Hotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")] //api/hotels/GetHotelById/2
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelservice.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);//200 + data
            }
            return NotFound(); // 404
        }

        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel =  await _hotelservice.GetHotelByName(name);
            if (hotel != null)
            {
                return Ok(hotel);//200 + data
            }
            return NotFound();
        }
        /// <summary>
        /// Create Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            var createdHotel = await _hotelservice.CreateHotel(hotel);
            return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);//200 + data

        }
        /// <summary>
        /// Update Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            if (await _hotelservice.GetHotelById(hotel.Id) != null)
            {
                return Ok(await _hotelservice.UpdateHotel(hotel));
            }
            return NotFound();

        }
        /// <summary>
        /// Delete Hotel
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelservice.GetHotelById(id) != null)
            {
                await _hotelservice.DeleteHotel(id);

                return Ok(); //200
            }
            return NotFound();
        }



    }
}
