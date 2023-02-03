using FinalProject.Application.Common.Results;

namespace FinalProject.Application.Users.Queries
{
    public class UnregisteredUserResult
    {
        public string[] Heads { get; set; } = new string[] {
            "Id",
            "Name",
            "Surname",
            "CompanyName",
            "Email",
            "Role"
        };

        public List<NotRegisteredUserResult> Data { get; set; }
    }
}
