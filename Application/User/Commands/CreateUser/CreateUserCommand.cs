using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public record CreateUserCommand(UserModel user) : IRequest;

    public class CreateUserCommandHandler : AsyncRequestHandler<CreateUserCommand>
    {
        private readonly IDataAccess _dataAccess;

        public CreateUserCommandHandler(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        protected override Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserModel user = request.user;

            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("INSERT INTO User(");
                stringBuilder.Append("[Id],[CompanyId],[FirstName],[LastName],[EmailAddress],[PhoneNo],[AuthorityId],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy])");
                stringBuilder.Append("VALUES (" + user.Id + "," + user.CompanyId + "," + user.FirstName + "," + user.LastName + "," + user.EmailAddress);
                stringBuilder.Append("," + user.PhoneNumber + "," + user.AuthorityId + "," + user.CreatedDate + "," + user.CreatedBy + "," + user.ModifiedBy + "," + user.ModifiedDate);
                string sql = stringBuilder.ToString();

                _dataAccess.SaveData(sql, "JupiterDb");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred trying to create new User", ex);
            }
        }
    }
}
