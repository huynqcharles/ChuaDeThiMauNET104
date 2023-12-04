using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChuaDeThiMau.Context;
using ChuaDeThiMau.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ChuaDeThiMau.Controllers
{
    public class NhanvienController : Controller
    {
        private readonly MyDbContext _dbContext;

        public NhanvienController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var products = _dbContext.NhanViens.ToList();
            return View(products);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(NhanVien nhanVien)
        {
            _dbContext.Add(nhanVien);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var nhanvien = _dbContext.NhanViens.Find(id);
            return View(nhanvien);
        }
        
        [HttpPost]
        public IActionResult Edit(NhanVien nhanVien)
        {
            var currentNhanVien = _dbContext.NhanViens.AsNoTracking().FirstOrDefault(nv => nv.Id == nhanVien.Id);
            var oldNhanVienJSON = JsonConvert.SerializeObject(currentNhanVien);
            HttpContext.Session.SetString("oldNhanVienJSON", oldNhanVienJSON);
            
            _dbContext.Update(nhanVien);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var nhanvien = _dbContext.NhanViens.Find(id);
            _dbContext.Remove(nhanvien);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Rollback(Guid id)
        {
            var oldNhanVienJSON = HttpContext.Session.GetString("oldNhanVienJSON");
            if (oldNhanVienJSON != null)
            {
                var oldNhanVien = JsonConvert.DeserializeObject<NhanVien>(oldNhanVienJSON);
            
                _dbContext.Update(oldNhanVien);
                _dbContext.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}