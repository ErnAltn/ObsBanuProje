
using Business.Handlers.Bolums.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Bolums.Queries.GetBolumQuery;
using Entities.Concrete;
using static Business.Handlers.Bolums.Queries.GetBolumsQuery;
using static Business.Handlers.Bolums.Commands.CreateBolumCommand;
using Business.Handlers.Bolums.Commands;
using Business.Constants;
using static Business.Handlers.Bolums.Commands.UpdateBolumCommand;
using static Business.Handlers.Bolums.Commands.DeleteBolumCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class BolumHandlerTests
    {
        Mock<IBolumRepository> _bolumRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _bolumRepository = new Mock<IBolumRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Bolum_GetQuery_Success()
        {
            //Arrange
            var query = new GetBolumQuery();

            _bolumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolum, bool>>>())).ReturnsAsync(new Bolum()
//propertyler buraya yazılacak
//{																		
//BolumId = 1,
//BolumName = "Test"
//}
);

            var handler = new GetBolumQueryHandler(_bolumRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.BolumId.Should().Be(1);

        }

        [Test]
        public async Task Bolum_GetQueries_Success()
        {
            //Arrange
            var query = new GetBolumsQuery();

            _bolumRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Bolum, bool>>>()))
                        .ReturnsAsync(new List<Bolum> { new Bolum() { /*TODO:propertyler buraya yazılacak BolumId = 1, BolumName = "test"*/ } });

            var handler = new GetBolumsQueryHandler(_bolumRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Bolum>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task Bolum_CreateCommand_Success()
        {
            Bolum rt = null;
            //Arrange
            var command = new CreateBolumCommand();
            //propertyler buraya yazılacak
            //command.BolumName = "deneme";

            _bolumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolum, bool>>>()))
                        .ReturnsAsync(rt);

            _bolumRepository.Setup(x => x.Add(It.IsAny<Bolum>())).Returns(new Bolum());

            var handler = new CreateBolumCommandHandler(_bolumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _bolumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Bolum_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateBolumCommand();
            //propertyler buraya yazılacak 
            //command.BolumName = "test";

            _bolumRepository.Setup(x => x.Query())
                                           .Returns(new List<Bolum> { new Bolum() { /*TODO:propertyler buraya yazılacak BolumId = 1, BolumName = "test"*/ } }.AsQueryable());

            _bolumRepository.Setup(x => x.Add(It.IsAny<Bolum>())).Returns(new Bolum());

            var handler = new CreateBolumCommandHandler(_bolumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Bolum_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateBolumCommand();
            //command.BolumName = "test";

            _bolumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolum, bool>>>()))
                        .ReturnsAsync(new Bolum() { /*TODO:propertyler buraya yazılacak BolumId = 1, BolumName = "deneme"*/ });

            _bolumRepository.Setup(x => x.Update(It.IsAny<Bolum>())).Returns(new Bolum());

            var handler = new UpdateBolumCommandHandler(_bolumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _bolumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Bolum_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteBolumCommand();

            _bolumRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Bolum, bool>>>()))
                        .ReturnsAsync(new Bolum() { /*TODO:propertyler buraya yazılacak BolumId = 1, BolumName = "deneme"*/});

            _bolumRepository.Setup(x => x.Delete(It.IsAny<Bolum>()));

            var handler = new DeleteBolumCommandHandler(_bolumRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _bolumRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

