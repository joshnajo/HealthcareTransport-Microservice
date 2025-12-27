using MemberService.Models;

namespace MemberService.Data
{
    public class MemberRepo : IMemberRepo
    {
        private readonly AppDbContext _context;

        public MemberRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Add(member);
        }

    
        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        public Member? GetMemberById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateMember(Member member)
        {
            // No implementation needed for EF Core as it tracks changes automatically
        }

         public void DeleteMember(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Remove(member);
        }
    }
}