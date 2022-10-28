using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Projeto.Persistence;
using System.Net;
using Projeto.Base.Common.Helpers;
using Xunit;

namespace Projeto.Web.Api.Tests.Unit;

public sealed class ModeloControllerTest
{
    [Fact]
    public async Task Get_SeTesteExecutadoComSucesso_EntaoRetornaStatus200()
    {
        //Arrange
        var modeloRepository = new ModeloRepository();
        var expected = await modeloRepository.Listar();
        await using var apiFactory = new WebApplicationFactory<Program>();

        using var client = apiFactory.CreateClient();

        //Act
        var response = await client.GetAsync("/Modelo/Teste");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = JsonConvert.DeserializeObject<IEnumerable<object>>(await response.Content.ReadAsStringAsync())!;
        Assert.True(result.Compare(expected));
    }
}