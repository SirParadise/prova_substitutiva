using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back.Data;
using back.Models;

namespace back.Controllers;

[Route("back/imc")]
[ApiController]
public class ImcController : ControllerBase
{
    private readonly AppDataContext _context;

    public ImcController(AppDataContext context) =>
        _context = context;

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        List<Imc> imcs = _context.Imcs.Include(x => x.Aluno).ToList();

        return Ok(imcs);
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Imc imc)
    {
        Aluno? aluno = _context.Alunos.Find(imc.AlunoId);
        
        imc.Aluno = aluno;
        imc.CalculoImc = imc.Peso / (imc.Altura * imc.Altura);

        if (imc.CalculoImc < 18.5) {
            imc.Classificacao = "MAGREZA";
        } else if (imc.CalculoImc > 18.5 && imc.CalculoImc < 24.9) {
            imc.Classificacao = "NORMAL";
        } else if (imc.CalculoImc > 25 && imc.CalculoImc < 29.9) {
            imc.Classificacao = "SOBREPESO";
        } else if (imc.CalculoImc > 30 && imc.CalculoImc < 39.9) {
            imc.Classificacao = "OBESIDADE";
        } else {
            imc.Classificacao = "OBESIDADE GRAVE";
        }
        
        _context.Imcs.Add(imc);
        _context.SaveChanges();
        return Created("", imc);
    }

    
    [HttpPatch]
    [Route("alterar/{id}")]
    public IActionResult Alterar([FromRoute] int id, [FromBody] Imc imcAlterado)
    {
        Imc imc = _context.Imcs.Find(id);

        imc.Peso = imcAlterado.Peso;
        imc.Altura = imcAlterado.Altura;
        imc.Aluno = imcAlterado.Aluno;

        _context.Imcs.Update(imc);
        _context.SaveChanges();
        return Ok(imc);
    }
}
