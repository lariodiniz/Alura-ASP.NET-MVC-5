using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            

            return View(produtos);
        }

        public ActionResult Form()
        {
            CategoriasDAO dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.Categorias = categorias;
            ViewBag.Produto = new Produto();

            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            ViewBag.Produto = produto;

            int categoriaId = 1;
            if (produto.CategoriaId.Equals(categoriaId) && produto.Preco < 100.0)
            {
                ModelState.AddModelError("produto.Invalido", "informatica com preço abaixo de 100 reais");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                
                dao.Adiciona(produto);

                return RedirectToAction("Index", "Produto");
            }
            else
            {
                CategoriasDAO categoriasdao = new CategoriasDAO();
                ViewBag.Categorias = categoriasdao.Lista();
                return View("Form");
            }
            
        }

        public ActionResult Visualiza(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;

            return View();
        }
    }
}