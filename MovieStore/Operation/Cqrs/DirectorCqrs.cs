using Base.Response;
using MediatR;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Cqrs
{
    public record CreateDirectorCommand(DirectorRequest model) : IRequest<ApiResponse<DirectorResponse>>;
    public record UpdateDirectorCommand(DirectorRequest model, int Id) : IRequest<ApiResponse>;
    public record DeleteDirectorCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllDirectorQuery() : IRequest<ApiResponse<List<DirectorResponse>>>;

}
