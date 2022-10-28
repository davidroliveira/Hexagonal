using Projeto.Application.Base;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.Modelo;

public sealed record ModeloResponse(Task<IEnumerable<ModeloModel>> Content) : BaseResponse;
