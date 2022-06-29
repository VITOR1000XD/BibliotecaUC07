using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {

        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult editarUsuario(int id) 
        {
           return View(new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario UsuarioEdicao)
        { 
          UsuarioService us = new UsuarioService();
          us.editarUsuario(UsuarioEdicao);
          return RedirectToAction("ListaDeUsuarios","Usuario");
          
        }

        public IActionResult RegistrarUsuarios() 
        {
          Autenticacao.CheckLogin(this);
          Autenticacao.verificaSeUsuarioEAdmin(this);
          return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
           Autenticacao.CheckLogin(this);
           Autenticacao.verificaSeUsuarioEAdmin(this);

           novoUser.Senha = Criptografia.TextoCriptografado(novoUser.Senha);

           UsuarioService us = new UsuarioService();
           us.incluirUsuario(novoUser);

            return RedirectToAction("cadastroRealizado");
        }

        public IActionResult excluirUsuario(int id) 
        {
           return View(new UsuarioService().Listar(id));
        }
 
        [HttpPost]
        public IActionResult excluirUsuario(string decisao,int id)
        {
          if(decisao=="EXCLUIR")
          {
            ViewData["Mensagem"] ="Exclusão do usuario "+new UsuarioService().Listar(id).Nome+"realizada com sucesso";
            new UsuarioService().excluirUsuario(id);
            return View("ListaDeUsuarios", new UsuarioService().Listar());
          }
          else
          {
            ViewData["Mensagem"] = "Exclusão cancelada";
            return View("ListaDeUsuarios",new UsuarioService().Listar());
          }

        }

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
            
        }

        public IActionResult NeedAdmin()
        {
           Autenticacao.CheckLogin(this);
           return View();
        }

        public IActionResult Sair()
        {
          HttpContext.Session.Clear();
          return RedirectToAction("Index","Home");
        }
          
    
    }
}