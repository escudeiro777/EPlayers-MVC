using System;
using System.IO;
using EPlayersMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayersMVC.Controllers
{
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        Equipe equipeModel = new Equipe();

        [Route("Listar")]
        public IActionResult Index()
        {

            ViewBag.Equipes = equipeModel.LerTodas();
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.NomeEquipe = form["Nome"];
            // novaEquipe.Imagem = form["Imagem"];]

            // upload inicio

            if (form.Files.Count > 0)
            {
                var imageFile = form.Files[0];
                var pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", pathFolder, imageFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                novaEquipe.Imagem = imageFile.FileName;
            }


            else
            {
                novaEquipe.Imagem = "padrao.png";
            }

            // upload final

            equipeModel.Criar(novaEquipe);
            ViewBag.Equipes = equipeModel.LerTodas();
            return LocalRedirect("~/Equipe/Listar");
        }


        public IActionResult Excluir(int id)
        {
            equipeModel.Deletar(id);
            ViewBag.Equipes = equipeModel.LerTodas();
            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Deletar(int id)
        {
            equipeModel.Deletar(id);
            ViewBag.Equipes= equipeModel.LerTodas();
            return LocalRedirect("~/Excluir/Listar");
        }
    }
}