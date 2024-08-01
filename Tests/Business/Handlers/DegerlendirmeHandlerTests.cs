
using Business.Handlers.Degerlendirmes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Degerlendirmes.Queries.GetDegerlendirmeQuery;
using Entities.Concrete;
using static Business.Handlers.Degerlendirmes.Queries.GetDegerlendirmesQuery;
using static Business.Handlers.Degerlendirmes.Commands.CreateDegerlendirmeCommand;
using Business.Handlers.Degerlendirmes.Commands;
using Business.Constants;
using static Business.Handlers.Degerlendirmes.Commands.UpdateDegerlendirmeCommand;
using static Business.Handlers.Degerlendirmes.Commands.DeleteDegerlendirmeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DegerlendirmeHandlerTests
    {
        Mock<IDegerlendirmeRepository> _degerlendirmeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _degerlendirmeRepository = new Mock<IDegerlendirmeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Degerlendirme_GetQuery_Success()
        {
            //Arrange
            var query = new GetDegerlendirmeQuery();

            _degerlendirmeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Degerlendirme, bool>>>())).ReturnsAsync(new Degerlendirme()
//propertyler buraya yazılacak
//{																		
//DegerlendirmeId = 1,
//DegerlendirmeName = "Test"
//}
);

            var handler = new GetDegerlendirmeQueryHandler(_degerlendirmeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DegerlendirmeId.Should().Be(1);

        }

        [Test]
        public async Task Degerlendirme_GetQueries_Success()
        {
            //Arrange
            var query = new GetDegerlendirmesQuery();

            _degerlendirmeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Degerlendirme, bool>>>()))
                        .ReturnsAsync(new List<Degerlendirme> { new Degerlendirme() { /*TODO:propertyler buraya yazılacak DegerlendirmeId = 1, DegerlendirmeName = "test"*/ } });

            var handler = new GetDegerlendirmesQueryHandler(_degerlendirmeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Degerlendirme>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task Degerlendirme_CreateCommand_Success()
        {
            Degerlendirme rt = null;
            //Arrange
            var command = new CreateDegerlendirmeCommand();
            //propertyler buraya yazılacak
            //command.DegerlendirmeName = "deneme";

            _degerlendirmeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Degerlendirme, bool>>>()))
                        .ReturnsAsync(rt);

            _degerlendirmeRepository.Setup(x => x.Add(It.IsAny<Degerlendirme>())).Returns(new Degerlendirme());

            var handler = new CreateDegerlendirmeCommandHandler(_degerlendirmeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _degerlendirmeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Degerlendirme_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDegerlendirmeCommand();
            //propertyler buraya yazılacak 
            //command.DegerlendirmeName = "test";

            _degerlendirmeRepository.Setup(x => x.Query())
                                           .Returns(new List<Degerlendirme> { new Degerlendirme() { /*TODO:propertyler buraya yazılacak DegerlendirmeId = 1, DegerlendirmeName = "test"*/ } }.AsQueryable());

            _degerlendirmeRepository.Setup(x => x.Add(It.IsAny<Degerlendirme>())).Returns(new Degerlendirme());

            var handler = new CreateDegerlendirmeCommandHandler(_degerlendirmeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Degerlendirme_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDegerlendirmeCommand();
            //command.DegerlendirmeName = "test";

            _degerlendirmeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Degerlendirme, bool>>>()))
                        .ReturnsAsync(new Degerlendirme() { /*TODO:propertyler buraya yazılacak DegerlendirmeId = 1, DegerlendirmeName = "deneme"*/ });

            _degerlendirmeRepository.Setup(x => x.Update(It.IsAny<Degerlendirme>())).Returns(new Degerlendirme());

            var handler = new UpdateDegerlendirmeCommandHandler(_degerlendirmeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _degerlendirmeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Degerlendirme_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDegerlendirmeCommand();

            _degerlendirmeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Degerlendirme, bool>>>()))
                        .ReturnsAsync(new Degerlendirme() { /*TODO:propertyler buraya yazılacak DegerlendirmeId = 1, DegerlendirmeName = "deneme"*/});

            _degerlendirmeRepository.Setup(x => x.Delete(It.IsAny<Degerlendirme>()));

            var handler = new DeleteDegerlendirmeCommandHandler(_degerlendirmeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _degerlendirmeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

