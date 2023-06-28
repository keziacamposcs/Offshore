using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OffshoreTrack.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using OffshoreTrack.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class ContaController : Controller
{
    private readonly Contexto contexto;

    public ContaController(Contexto contexto)
    {
        this.contexto = contexto;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(Usuario modelo)
    {
        var senhaCriptografada = HashPassword(modelo.senha);
        //var usuario = contexto.Usuario.Include(u => u.Permissao).FirstOrDefault(u => u.usuario == modelo.usuario && u.senha == modelo.senha);
        var usuario = contexto.Usuario.Include(u => u.Permissao).FirstOrDefault(u => u.usuario == modelo.usuario && u.senha == senhaCriptografada);

        if (usuario != null)
        {
                var role = "Usuário";
                if (usuario.Permissao.permissao_admin.HasValue)
                {
                    role = usuario.Permissao.permissao_admin.Value ? "Admin" : "Usuário";
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.usuario),
                    //new Claim(ClaimTypes.Role, usuario.Permissao.permissao_admin ? "Admin" : "Usuário"),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("PodeCriar", usuario.Permissao.pode_criar.ToString()),
                    new Claim("PodeLer", usuario.Permissao.pode_ler.ToString()),
                    new Claim("PodeAtualizar", usuario.Permissao.pode_atualizar.ToString()),
                    new Claim("PodeDeletar", usuario.Permissao.pode_deletar.ToString()),
                    new Claim("PermissaoCertificado", usuario.Permissao.permissaoCertificado.ToString()),
                    new Claim("PermissaoCliente", usuario.Permissao.permissaoCliente.ToString()),
                    new Claim("PermissaoContrato", usuario.Permissao.permissaoContrato.ToString()),
                    new Claim("PermissaoCriticidade", usuario.Permissao.permissaoCriticidade.ToString()),
                    new Claim("PermissaoFornecedor", usuario.Permissao.permissaoFornecedor.ToString()),
                    new Claim("PermissaoLocal", usuario.Permissao.permissaoLocal.ToString()),
                    new Claim("PermissaoManutencao", usuario.Permissao.permissaoManutencao.ToString()),
                    new Claim("PermissaoMaterial", usuario.Permissao.permissaoMaterial.ToString()),
                    new Claim("PermissaoOrdemCompra", usuario.Permissao.permissaoOrdemCompra.ToString()),
                    new Claim("PermissaoParteSolta", usuario.Permissao.permissaoParteSolta.ToString()),
                    new Claim("PermissaoRateio", usuario.Permissao.permissaoRateio.ToString()),
                    new Claim("PermissaoSetor", usuario.Permissao.permissaoSetor.ToString()),
                    new Claim("PermissaoTipo", usuario.Permissao.permissaoTipo.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

            return RedirectToAction("Index", "Home");

        }
        else
        {
            ViewBag.Erro = "Usuário ou senha inválido.";
            return View();
        }
    }

public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Login", "Conta");
}


    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

}
