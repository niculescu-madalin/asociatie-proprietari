using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using asociatie_proprietari.Controllers;
using asociatie_proprietari.Data;
using asociatie_proprietari.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests
{
    public class TestAngajat
    {
        private ApplicationDbContext _context;
        private AngajatsController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Angajat.AddRange(new List<Angajat>
            {
                new Angajat { AngajatId = 1, Nume = "John", Prenume = "Doe", Telefon = "1234567890", Email = "john.doe@example.com", Functie = "Manager", Salariu = 5000, Bonus = 500 },
                new Angajat { AngajatId = 2, Nume = "Jane", Prenume = "Smith", Telefon = "0987654321", Email = "jane.smith@example.com", Functie = "Engineer", Salariu = 4000, Bonus = 400 }
            });
            _context.SaveChanges();

            _controller = new AngajatsController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Index_ReturnsViewResult_WithListOfAngajats()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as List<Angajat>;
            Assert.That(model, Is.Not.Null, "Expected List<Angajat> as model");
            Assert.That(model.Count, Is.EqualTo(2), "Expected two Angajats in the model");
        }

        [Test]
        public async Task Details_ReturnsViewResult_WithAngajat()
        {
            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as Angajat;
            Assert.That(model, Is.Not.Null, "Expected Angajat as model");
            Assert.That(model.AngajatId, Is.EqualTo(1), "Expected AngajatId to be 1");
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenAngajatNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
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
            var newAngajat = new Angajat
            {
                AngajatId = 3,
                Nume = "Michael",
                Prenume = "Johnson",
                Telefon = "5555555555",
                Email = "michael.johnson@example.com",
                Functie = "Analyst",
                Salariu = 3000,
                Bonus = 300
            };

            // Act
            var result = await _controller.Create(newAngajat);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");
            Assert.That(_context.Angajat.Count(), Is.EqualTo(3), "Expected the Angajat count to be 3");
        }

        [Test]
        public async Task Edit_Get_ReturnsViewResult_WithAngajat()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as Angajat;
            Assert.That(model, Is.Not.Null, "Expected Angajat as model");
            Assert.That(model.AngajatId, Is.EqualTo(1), "Expected AngajatId to be 1");
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
        }

        [Test]
        public async Task Edit_Get_ReturnsNotFound_WhenAngajatNotFound()
        {
            // Act
            var result = await _controller.Edit(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var angajatToUpdate = _context.Angajat.First();
            angajatToUpdate.Nume = "Updated Name";

            // Act
            var result = await _controller.Edit(angajatToUpdate.AngajatId, angajatToUpdate);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");

            var updatedAngajat = _context.Angajat.Find(angajatToUpdate.AngajatId);
            Assert.That(updatedAngajat.Nume, Is.EqualTo("Updated Name"), "Expected the Nume to be updated");
        }

        [Test]
        public async Task Edit_Post_ReturnsNotFound_WhenIdMismatch()
        {
            // Arrange
            var angajatToUpdate = _context.Angajat.First();

            // Act
            var result = await _controller.Edit(999, angajatToUpdate);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
        }

        [Test]
        public async Task Delete_Get_ReturnsViewResult_WithAngajat()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as Angajat;
            Assert.That(model, Is.Not.Null, "Expected Angajat as model");
            Assert.That(model.AngajatId, Is.EqualTo(1), "Expected AngajatId to be 1");
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Delete(null);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
        }

        [Test]
        public async Task Delete_Get_ReturnsNotFound_WhenAngajatNotFound()
        {
            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult");
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
            Assert.That(_context.Angajat.Count(), Is.EqualTo(1), "Expected the Angajat count to be 1");
        }

    }
}
