
using Business.Handlers.Dersliks.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Dersliks.Queries.GetDerslikQuery;
using Entities.Concrete;
using static Business.Handlers.Dersliks.Queries.GetDersliksQuery;
using static Business.Handlers.Dersliks.Commands.CreateDerslikCommand;
using Business.Handlers.Dersliks.Commands;
using Business.Constants;
using static Business.Handlers.Dersliks.Commands.UpdateDerslikCommand;
using static Business.Handlers.Dersliks.Commands.DeleteDerslikCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DerslikHandlerTests
    {
        Mock<IDerslikRepository> _derslikRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _derslikRepository = new Mock<IDerslikRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Derslik_GetQuery_Success()
        {
            //Arrange
            var query = new GetDerslikQuery();

            _derslikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Derslik, bool>>>())).ReturnsAsync(new Derslik()
//propertyler buraya yazılacak
//{																		
//DerslikId = 1,
//DerslikName = "Test"
//}
);

            var handler = new GetDerslikQueryHandler(_derslikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DerslikId.Should().Be(1);

        }

        [Test]
        public async Task Derslik_GetQueries_Success()
        {
            //Arrange
            var query = new GetDersliksQuery();

            _derslikRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Derslik, bool>>>()))
                        .ReturnsAsync(new List<Derslik> { new Derslik() { /*TODO:propertyler buraya yazılacak DerslikId = 1, DerslikName = "test"*/ } });

            var handler = new GetDersliksQueryHandler(_derslikRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Derslik>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task Derslik_CreateCommand_Success()
        {
            Derslik rt = null;
            //Arrange
            var command = new CreateDerslikCommand();
            //propertyler buraya yazılacak
            //command.DerslikName = "deneme";

            _derslikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Derslik, bool>>>()))
                        .ReturnsAsync(rt);

            _derslikRepository.Setup(x => x.Add(It.IsAny<Derslik>())).Returns(new Derslik());

            var handler = new CreateDerslikCommandHandler(_derslikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _derslikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Derslik_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDerslikCommand();
            //propertyler buraya yazılacak 
            //command.DerslikName = "test";

            _derslikRepository.Setup(x => x.Query())
                                           .Returns(new List<Derslik> { new Derslik() { /*TODO:propertyler buraya yazılacak DerslikId = 1, DerslikName = "test"*/ } }.AsQueryable());

            _derslikRepository.Setup(x => x.Add(It.IsAny<Derslik>())).Returns(new Derslik());

            var handler = new CreateDerslikCommandHandler(_derslikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Derslik_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDerslikCommand();
            //command.DerslikName = "test";

            _derslikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Derslik, bool>>>()))
                        .ReturnsAsync(new Derslik() { /*TODO:propertyler buraya yazılacak DerslikId = 1, DerslikName = "deneme"*/ });

            _derslikRepository.Setup(x => x.Update(It.IsAny<Derslik>())).Returns(new Derslik());

            var handler = new UpdateDerslikCommandHandler(_derslikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _derslikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Derslik_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDerslikCommand();

            _derslikRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Derslik, bool>>>()))
                        .ReturnsAsync(new Derslik() {});

            _derslikRepository.Setup(x => x.Delete(It.IsAny<Derslik>()));

            var handler = new DeleteDerslikCommandHandler(_derslikRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _derslikRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

