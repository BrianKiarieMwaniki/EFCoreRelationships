using EFCoreRelationships.DTOs;
using EFCoreRelationships.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
           return await _context.Characters
                .Where(c => c.UserId == userId)
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .ToListAsync();
        }
        
        
        [HttpPost]
        public async Task<ActionResult<List<Character>>> Post(Character character)
        {
           
           _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return await Get(character.UserId);
        }

        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> Post(Weapon weapon)
        {
            var character = await _context.Characters.FindAsync(weapon.Id);
            if (character is null) return NotFound();
            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            return character;
        }

        [HttpPost("skill")]
        public async Task<ActionResult<Character>> Post(AddCharacterSkillDTO request)
        {
            var character = await _context.Characters
                    .Where(c => c.Id == request.CharacterId)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync();

            if (character is null) return NotFound();

            var skill = await _context.Skills.FindAsync(request.SkillId);
            if (skill is null) return NotFound();

            character?.Skills?.Add(skill);

            await _context.SaveChangesAsync();

            return Ok(character);

        }

    }
}
