
using Business.Handlers.DersProgramis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.DersProgramis.Queries.GetDersProgramiQuery;
using Entities.Concrete;
using static Business.Handlers.DersProgramis.Queries.GetDersProgramisQuery;
using static Business.Handlers.DersProgramis.Commands.CreateDersProgramiCommand;
using Business.Handlers.DersProgramis.Commands;
using Business.Constants;
using static Business.Handlers.DersProgramis.Commands.UpdateDersProgramiCommand;
using static Business.Handlers.DersProgramis.Commands.DeleteDersProgramiCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DersProgramiHandlerTests
    {
        Mock<IDersProgramiRepository> _dersProgramiRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _dersProgramiRepository = new Mock<IDersProgramiRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DersProgrami_GetQuery_Success()
        {
            //Arrange
            var query = new GetDersProgramiQuery();

            _dersProgramiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersProgrami, bool>>>())).ReturnsAsync(new DersProgrami()
//propertyler buraya yazılacak
//{																		
//DersProgramiId = 1,
//DersProgramiName = "Test"
//}
);

            var handler = new GetDersProgramiQueryHandler(_dersProgramiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DersProgramiId.Should().Be(1);

        }

        [Test]
        public async Task DersProgrami_GetQueries_Success()
        {
            //Arrange
            var query = new GetDersProgramisQuery();

            _dersProgramiRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DersProgrami, bool>>>()))
                        .ReturnsAsync(new List<DersProgrami> { new DersProgrami() { /*TODO:propertyler buraya yazılacak DersProgramiId = 1, DersProgramiName = "test"*/ } });

            var handler = new GetDersProgramisQueryHandler(_dersProgramiRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DersProgrami>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task DersProgrami_CreateCommand_Success()
        {
            DersProgrami rt = null;
            //Arrange
            var command = new CreateDersProgramiCommand();
            //propertyler buraya yazılacak
            //command.DersProgramiName = "deneme";

            _dersProgramiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersProgrami, bool>>>()))
                        .ReturnsAsync(rt);

            _dersProgramiRepository.Setup(x => x.Add(It.IsAny<DersProgrami>())).Returns(new DersProgrami());

            var handler = new CreateDersProgramiCommandHandler(_dersProgramiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersProgramiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DersProgrami_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDersProgramiCommand();
            //propertyler buraya yazılacak 
            //command.DersProgramiName = "test";

            _dersProgramiRepository.Setup(x => x.Query())
                                           .Returns(new List<DersProgrami> { new DersProgrami() { /*TODO:propertyler buraya yazılacak DersProgramiId = 1, DersProgramiName = "test"*/ } }.AsQueryable());

            _dersProgramiRepository.Setup(x => x.Add(It.IsAny<DersProgrami>())).Returns(new DersProgrami());

            var handler = new CreateDersProgramiCommandHandler(_dersProgramiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DersProgrami_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDersProgramiCommand();
            //command.DersProgramiName = "test";

            _dersProgramiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersProgrami, bool>>>()))
                        .ReturnsAsync(new DersProgrami() { /*TODO:propertyler buraya yazılacak DersProgramiId = 1, DersProgramiName = "deneme"*/ });

            _dersProgramiRepository.Setup(x => x.Update(It.IsAny<DersProgrami>())).Returns(new DersProgrami());

            var handler = new UpdateDersProgramiCommandHandler(_dersProgramiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersProgramiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DersProgrami_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDersProgramiCommand();

            _dersProgramiRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DersProgrami, bool>>>()))
                        .ReturnsAsync(new DersProgrami() { /*TODO:propertyler buraya yazılacak DersProgramiId = 1, DersProgramiName = "deneme"*/});

            _dersProgramiRepository.Setup(x => x.Delete(It.IsAny<DersProgrami>()));

            var handler = new DeleteDersProgramiCommandHandler(_dersProgramiRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _dersProgramiRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

