using MemberService.Models;

namespace MemberService.Data
{
    public interface IMemberRepo
    {
        bool SaveChanges();

        IEnumerable<Member> GetAllMembers();
        Member? GetMemberById(int id);// By MemberId
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
    }
}