using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using asociatie_proprietari.Controllers;
using asociatie_proprietari.Data;
using asociatie_proprietari.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace UnitTests
{
    public class TestFactura
    {
        private ApplicationDbContext _context;
        private FacturasController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            // Seed the database with test data
            _context.Factura.AddRange(new List<Factura>
            {
                new Factura { Id = 1, SumaDePlata = 100, DataScadenta = new DateTime(2023, 10, 1), status = "da" },
                new Factura { Id = 2, SumaDePlata = 200, DataScadenta = new DateTime(2023, 11, 1), status = "nu" }
            });
            _context.SaveChanges();

            _controller = new FacturasController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewResult_WithListOfFacturas()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as List<Factura>;
            Assert.That(model, Is.Not.Null, "Expected List<Factura> as model");
            Assert.That(model.Count, Is.EqualTo(2), "Expected two Facturas in the model");
        }

        [Test]
        public async Task Details_ReturnsViewResult_WithFactura()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as Factura;
            Assert.That(model, Is.Not.Null, "Expected Factura as model");
            Assert.That(model.Id, Is.EqualTo(1), "Expected FacturaId to be 1");
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenFacturaNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult when no factura is found");
        }

        [Test]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>(), "Expected ViewResult");
        }

        [Test]
        public async Task Create_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var newFactura = new Factura
            {
                Id = 3,
                SumaDePlata = 150,
                DataScadenta = new DateTime(2023, 12, 1),
                status = "nu"
            };

            // Act
            var result = await _controller.Create(newFactura);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");
            Assert.That(_context.Factura.Count(), Is.EqualTo(3), "Expected the Factura count to be 3");
        }

        [Test]
        public async Task Edit_Get_ReturnsViewResult_WithFactura()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as Factura;
            Assert.That(model, Is.Not.Null, "Expected Factura as model");
            Assert.That(model.Id, Is.EqualTo(1), "Expected FacturaId to be 1");
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var facturaToUpdate = _context.Factura.First();
            facturaToUpdate.SumaDePlata = 250;

            // Act
            var result = await _controller.Edit(facturaToUpdate.Id, facturaToUpdate);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");

            var updatedFactura = _context.Factura.Find(facturaToUpdate.Id);
            Assert.That(updatedFactura.SumaDePlata, Is.EqualTo(250), "Expected the Amount to be updated");
        }

        [Test]
        public async Task Delete_Get_ReturnsViewResult_WithFactura()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as Factura;
            Assert.That(model, Is.Not.Null, "Expected Factura as model");
            Assert.That(model.Id, Is.EqualTo(1), "Expected FacturaId to be 1");
        }

        [Test]
        public async Task Delete_Post_RedirectsToIndex_OnSuccess()
        {
            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");
            Assert.That(_context.Factura.Count(), Is.EqualTo(1), "Expected the Factura count to be reduced to 1");
        }
    }
}
