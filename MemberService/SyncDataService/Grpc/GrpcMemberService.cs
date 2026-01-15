using AutoMapper;
using Grpc.Core;
using MemberService.Data;

namespace MemberService.SyncDataService.Grpc
{
    public class GrpcMemberService : GrpcMember.GrpcMemberBase
    {
        private readonly IMemberRepo _memberRepo;
        private readonly IMapper _mapper;

        public GrpcMemberService(IMemberRepo memberRepo, IMapper mapper)
        {
            _memberRepo = memberRepo;
            _mapper = mapper;
        }

        public override Task<MemberResponse> GetAllMembers(GetAllRequest request, ServerCallContext context)
        {

            var response = new MemberResponse();
            var members = _memberRepo.GetAllMembers();
            
            foreach(var member in members)
            {
                response.Member.Add(_mapper.Map<GrpcMemberModel>(member));
            }

            return Task.FromResult(response);
        }
    }
}
