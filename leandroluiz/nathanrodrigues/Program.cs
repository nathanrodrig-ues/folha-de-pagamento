using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using leandroluiz.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapPost("/api/folha/cadastrar/", ([FromBody] Folha folha, [FromServices] AppDataContext ctx) =>
{
    if (string.IsNullOrWhiteSpace(folha.Nome))
    {
        return Results.BadRequest("ERROR: VAZIO");
    }

    if(folha.Mes == 0 || folha.Mes >= 13){return Results.BadRequest("ERROR: MES INVALIDO");}
    if(folha.Ano <= 1999 ){return Results.BadRequest("ERROR: ANO INVALIDO");}


    Folha? cadastroCpf = ctx.Folhas.FirstOrDefault(f => f.Cpf == folha.Cpf);
    if (cadastroCpf is not null) { return Results.Conflict("ERROR: CPF DUPLICADO"); }

    /*Folha? cadastroMes = ctx.Folhas.FirstOrDefault(f => f.Mes == folha.Mes);
    if (cadastroMes is not null) { return Results.Conflict("ERROR: MES DUPLICADO"); }

    Folha? cadastrAno = ctx.Folhas.FirstOrDefault(f => f.Ano == folha.Ano);
    if (cadastrAno is not null) { return Results.Conflict("ERROR: ANO DUPLICADO"); }*/

    double valorSalarioBruto = folha.HorasTrabalhadas * folha.ValorHora;
    if (valorSalarioBruto <= 1903.98){folha.ImpostoRenda = 0;}
    else if (valorSalarioBruto <= 2826.65){folha.ImpostoRenda = (valorSalarioBruto * 0.075) - 142.80;}
    else if (valorSalarioBruto <= 3751.05){folha.ImpostoRenda = (valorSalarioBruto * 0.15) - 354.80;}
    else if (valorSalarioBruto <= 4664.68){folha.ImpostoRenda = (valorSalarioBruto * 0.225) - 636.13;}
    else{folha.ImpostoRenda = (valorSalarioBruto * 0.275) - 869.36;}

    if( valorSalarioBruto <= 1693.72){ folha.Inss = valorSalarioBruto * 0.08;}
    else if( valorSalarioBruto <= 2822.90){folha.Inss = valorSalarioBruto * 0.09;}
    else if( valorSalarioBruto <= 5645.80){folha.Inss = valorSalarioBruto * 0.11;}
    else{folha.Inss = 621.03;}

    folha.Fgts = valorSalarioBruto * 0.08;

    folha.SalarioLiquido = valorSalarioBruto - folha.ImpostoRenda - folha.Inss;

    ctx.Folhas.Add(folha);
    ctx.SaveChanges();
    return Results.Created($"/folha/{folha.Id}", folha);
});


app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) =>
{
    return Results.Ok(ctx.Folhas.ToList());
});


app.MapGet("/api/folha/buscar/{Cpf}/{Mes}/{Ano}", ([FromRoute] string Cpf, int Mes, int Ano, [FromServices] AppDataContext ctx ) =>
{
    Folha? buscaCpf = ctx.Folhas.FirstOrDefault(f => f.Cpf == Cpf);
    if (buscaCpf is not null) { return Results.Ok(buscaCpf);}

    Folha? buscaMes = ctx.Folhas.FirstOrDefault(f => f.Mes == Mes);
    if (buscaMes is not null) { return Results.Ok(buscaMes);}

    Folha? buscaAno = ctx.Folhas.FirstOrDefault(f => f.Ano == Ano);
    if (buscaAno is not null) { return Results.Ok(buscaAno);}

    return Results.NotFound("ERROR: NAO EXISTE");
});


app.MapDelete("/api/folha/remover/{Cpf}/{Mes}/{Ano}", ([FromRoute] string Cpf, int Mes, int Ano, [FromServices] AppDataContext ctx ) =>
{
    Folha? removerCpf = ctx.Folhas.FirstOrDefault(f => f.Cpf == Cpf);
    if (removerCpf is null) { return Results.NotFound("ERROR");}

    Folha? removerMes = ctx.Folhas.FirstOrDefault(f => f.Mes == Mes);
    if (removerMes is null) { return Results.NotFound("ERROR");}

    Folha? removerAno = ctx.Folhas.FirstOrDefault(f => f.Ano == Ano);
    if (removerAno is null) { return Results.NotFound("ERROR");}

    ctx.Folhas.Remove(removerCpf);
    ctx.SaveChanges();
    return Results.Ok("SUCESSO: REMOVIDO");
});


app.MapGet("/api/folha/total-liquido", ([FromServices] AppDataContext ctx) =>
{
    if (!ctx.Folhas.Any()){return Results.NotFound("ERRO: SALARIO LIQUIDO VAZIO");}

    double resultado = ctx.Folhas.Sum(f => f.SalarioLiquido);
    return Results.Ok($"Total Liquido: {resultado:F2}");
});



app.Run();
