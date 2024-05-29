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
    public class TestConsumApa
    {
        private ApplicationDbContext _context;
        private ConsumApasController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.ConsumApa.AddRange(new List<ConsumApa>
            {
                new ConsumApa { Id = 1, ConsumApaRece = 100, ConsumApaCalda = 50, Luna = 1, An = 2023, ApartamentId = 1 },
                new ConsumApa { Id = 2, ConsumApaRece = 120, ConsumApaCalda = 60, Luna = 2, An = 2023, ApartamentId = 2 }
            });
            _context.SaveChanges();

            _controller = new ConsumApasController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenConsumApaNotFound()
        {
            // Act
            var result = await _controller.Details(999);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>(), "Expected NotFoundResult when no ConsumApa is found");
        }

        [Test]
        public void Create_ReturnsViewResult()
        {
            // Act
            IActionResult result = _controller.Create();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>(), "Expected a ViewResult.");
        }


        [Test]
        public async Task Create_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var newConsumApa = new ConsumApa
            {
                Id = 3,
                ConsumApaRece = 130,
                ConsumApaCalda = 70,
                Luna = 3,
                An = 2023,
                ApartamentId = 3
            };

            // Act
            var result = await _controller.Create(newConsumApa);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");
            Assert.That(_context.ConsumApa.Count(), Is.EqualTo(3), "Expected the ConsumApa count to be 3");
        }

        [Test]
        public async Task Edit_Get_ReturnsViewResult_WithConsumApa()
        {
            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.That(viewResult, Is.Not.Null, "Expected ViewResult");
            var model = viewResult.Model as ConsumApa;
            Assert.That(model, Is.Not.Null, "Expected ConsumApa as model");
            Assert.That(model.Id, Is.EqualTo(1), "Expected ConsumApaId to be 1");
        }

        [Test]
        public async Task Edit_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var consumApaToUpdate = _context.ConsumApa.First();
            consumApaToUpdate.ConsumApaRece = 140;  // Update the cold water consumption

            // Act
            var result = await _controller.Edit(consumApaToUpdate.Id, consumApaToUpdate);

            // Assert
            Assert.That(result, Is.InstanceOf<RedirectToActionResult>(), "Expected RedirectToActionResult");
            var redirectResult = result as RedirectToActionResult;
            Assert.That(redirectResult.ActionName, Is.EqualTo("Index"), "Expected redirection to Index");

            var updatedConsumApa = _context.ConsumApa.Find(consumApaToUpdate.Id);
            Assert.That(updatedConsumApa.ConsumApaRece, Is.EqualTo(140), "Expected the ConsumApaRece to be updated");
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
            Assert.That(_context.ConsumApa.Count(), Is.EqualTo(1), "Expected the ConsumApa count to be reduced to 1");
        }
    }
}
