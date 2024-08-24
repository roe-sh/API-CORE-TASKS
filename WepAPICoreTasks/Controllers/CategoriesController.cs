using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepAPICoreTasks.Models;

namespace WepAPICoreTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        private readonly MyDbContext _db;

        public Categories(MyDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult Get()
        {

            var categories = _db.Categories.ToList();
            return Ok(categories);
        }


        [Route("api/categories/getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var categories = _db.Categories.Where(p => p.CategoryId == id).FirstOrDefault();

        //    return Ok(categories);
        //}
        [HttpGet("{id:min(5)}")]
        public IActionResult Get(int id)
        {
            var categories = _db.Categories.Where(p => p.CategoryId == id).FirstOrDefault();
            if (categories == null)
                return NotFound();
            return Ok(categories);
        }


        [HttpGet]
        [Route("/api/categories/categryId/{id1:int:min(5)}")]
        public IActionResult GetById(int id1)
        {

            var x = _db.Categories.Where(p => p.CategoryId == id1).FirstOrDefault();
            if (x == null)
                return BadRequest();
            return Ok(x);



        }




        [HttpGet("name")]
        public IActionResult GetName(string name)
        {
            var categories = _db.Categories.Where(p => p.CategoryName == name).FirstOrDefault();
            if (categories != null)
            {

                return Ok($"the category name is  {name}");

            }
            return NotFound();
        }

        [Route("/api/Categories/CategoryName/{name}")]
        [HttpGet]
        public IActionResult Getname(string name)
        {
            var categories = _db.Categories.Where(p => p.CategoryName == name).FirstOrDefault();
            if (categories != null)
            {

                return Ok($"the category name is  {name}");

            }
            return NotFound();
        }


        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var category = _db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
        //    if (category != null)
        //    {

        //        _db.Products.RemoveRange(category.Products);


        //        _db.Categories.Remove(category);
        //        _db.SaveChanges();
        //        return NoContent();
        //    }
        //    return NotFound();
        //}



        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var deleteproduct = _db.Products.Where(l => l.CategoryId == id).ToList();


            _db.Products.RemoveRange(deleteproduct);
            _db.SaveChanges();



            var deleteCategory = _db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (deleteCategory != null)
            {
                _db.Categories.Remove(deleteCategory);
                _db.SaveChanges();
                return NoContent();

            }
            return NotFound();
        }


        [Route("/api/categories/deletecategory/{id}")]
        [HttpDelete]
        public IActionResult delete(int id)
        {


            if (id <= 0) { return BadRequest(); }


            var x = _db.Products.Where(p => p.CategoryId == id).ToList();

            _db.Products.RemoveRange(x);
            _db.SaveChanges();


            var xdelete = _db.Categories.FirstOrDefault(p => p.CategoryId == id);
            if (xdelete != null)
            {
                _db.Categories.Remove(xdelete);
                _db.SaveChanges();
                return NoContent();
            }

            return NotFound();



        }
    }
}