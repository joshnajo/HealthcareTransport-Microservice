using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TripService.Data;
using TripService.Dtos;
using TripService.Models;

namespace TripService.Controllers
{
    [Route("api/t/members/{memberId}/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepo _tripRepo;
        private readonly IMapper _mapper;

        public TripsController(ITripRepo tripRepo, IMapper mapper)
        {
            _tripRepo = tripRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all trips for a specific member
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        /// GET /api/t/members/{memberId}/trips
        [HttpGet]
        public ActionResult<IEnumerable<TripReadDto>> GetAllTripsForMember(string memberId)
        {
            Console.WriteLine($"Getting all trips for member {memberId} from TripService");
            if (!_tripRepo.MemberExists(memberId))
            {
                return NotFound($"Member {memberId} not found");
            }
            var _trips = _tripRepo.GetAllTripsForMember(memberId);
            if (_trips == null || !_trips.Any())
            {
                return NotFound($"No trips found for member {memberId}");
            }

            // Map trips to TripReadDto and return the result
            return Ok(_mapper.Map<IEnumerable<TripReadDto>>(_trips));
        }

        /// <summary>
        /// Get a specific trip by tripId for a member
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="tripId"></param>
        /// <returns></returns>
        /// GET /api/t/members/{memberId}/trips/{tripId}
        [HttpGet("{tripId}", Name = "GetTripForMember")]
        public ActionResult<TripReadDto> GetTripForMember(string memberId, int tripId)
        {
            Console.WriteLine($"Getting trip {tripId} for member {memberId} from TripService");
            if (!_tripRepo.MemberExists(memberId))
            {
                return NotFound($"Member {memberId} not found");
            }
            var _trip = _tripRepo.GetTripForMember(memberId, tripId);
            if(_trip == null)
            {
                return NotFound($"Trip with Id {tripId} not found for member {memberId}");
            }

            return Ok(_mapper.Map<TripReadDto>(_trip));
        }

        [HttpPost]
        public ActionResult<TripReadDto> CreateTripForMember(string memberId, TripCreateDto tripCreateDto)
        {
            Console.WriteLine($"Creating a new trip for member {memberId} in TripService");
            if (!_tripRepo.MemberExists(memberId))
            {
                return NotFound($"Member {memberId} not found");
            }
            var _trip = _mapper.Map<Trip>(tripCreateDto);
            _tripRepo.CreateTrip(memberId, _trip);

            // model is being created in db and now _trip TripReadDto as an id
            _tripRepo.SaveChanges();

            var _tripReadDto = _mapper.Map<TripReadDto>(_trip);
            
            return CreatedAtRoute(
                nameof(GetTripForMember), 
                new {memberId = memberId, tripId = _tripReadDto.Id}, 
                _tripReadDto);
        }
    }
}