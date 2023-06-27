using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Models;
using Microsoft.AspNetCore.Authorization;
namespace OffshoreTrack.Controllers;

using Microsoft.EntityFrameworkCore;
using OffshoreTrack.Data;

    
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Contexto contexto;

    public HomeController(ILogger<HomeController> logger, Contexto contexto)
    {
        _logger = logger;
        this.contexto = contexto;
    }

/*
    public async IActionResult Index()
    {
        return View();
    }
*/
public async Task<IActionResult> Index()
{
    var hoje = DateTime.Today;
    var limite = hoje.AddDays(30);
    var limiteOC = hoje.AddDays(10);

    var viewModel = new ViewModel
    {
        // Certificacoes para vencer
        certificacaoParaVencer = await contexto.Certificacao
            .Where(c => c.dataValidade >= hoje && c.dataValidade <= limite)
            .ToListAsync(),

        //Ultimos 10 itens em manutencao
        ultimasManutencoes = await contexto.Manutencao
            .OrderByDescending(m => m.data)
            .Take(10)
            .ToListAsync(),

        // Ordens de compra com data prevista para a partir de 10 dias
        ordemCompraAtrasado = await contexto.OrdemCompra
            .Where(c => c.data_prevista >= hoje && c.data_prevista <= limiteOC)
            .ToListAsync(),

        // Contratos para vencer
        contratosParaVencer = await contexto.Contrato
            .Where(c => c.dataFim >= hoje && c.dataFim <= limite)
            .ToListAsync(),

        // Ultimas OCs
        ultimasOC = await contexto.OrdemCompra
            .OrderByDescending(c => c.data_oc)
            .Take(10)
            .ToListAsync(),

    };
    
    return View(viewModel);
}




    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

