using Projeto.Application.Contracts;
using Projeto.Application.Models;

namespace Projeto.Application.UseCases.Modelo;

public sealed record ModeloResponse(Task<IEnumerable<ModeloModel>> Content) : IResponse;
