
using Business.Handlers.Mufredats.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Mufredats.Queries.GetMufredatQuery;
using Entities.Concrete;
using static Business.Handlers.Mufredats.Queries.GetMufredatsQuery;
using static Business.Handlers.Mufredats.Commands.CreateMufredatCommand;
using Business.Handlers.Mufredats.Commands;
using Business.Constants;
using static Business.Handlers.Mufredats.Commands.UpdateMufredatCommand;
using static Business.Handlers.Mufredats.Commands.DeleteMufredatCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MufredatHandlerTests
    {
        Mock<IMufredatRepository> _mufredatRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _mufredatRepository = new Mock<IMufredatRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Mufredat_GetQuery_Success()
        {
            //Arrange
            var query = new GetMufredatQuery();

            _mufredatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Mufredat, bool>>>())).ReturnsAsync(new Mufredat()
//propertyler buraya yazılacak
//{																		
//MufredatId = 1,
//MufredatName = "Test"
//}
);

            var handler = new GetMufredatQueryHandler(_mufredatRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.MufredatId.Should().Be(1);

        }

        [Test]
        public async Task Mufredat_GetQueries_Success()
        {
            //Arrange
            var query = new GetMufredatsQuery();

            _mufredatRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Mufredat, bool>>>()))
                        .ReturnsAsync(new List<Mufredat> { new Mufredat() { /*TODO:propertyler buraya yazılacak MufredatId = 1, MufredatName = "test"*/ } });

            var handler = new GetMufredatsQueryHandler(_mufredatRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Mufredat>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task Mufredat_CreateCommand_Success()
        {
            Mufredat rt = null;
            //Arrange
            var command = new CreateMufredatCommand();
            //propertyler buraya yazılacak
            //command.MufredatName = "deneme";

            _mufredatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Mufredat, bool>>>()))
                        .ReturnsAsync(rt);

            _mufredatRepository.Setup(x => x.Add(It.IsAny<Mufredat>())).Returns(new Mufredat());

            var handler = new CreateMufredatCommandHandler(_mufredatRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _mufredatRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Mufredat_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateMufredatCommand();
            //propertyler buraya yazılacak 
            //command.MufredatName = "test";

            _mufredatRepository.Setup(x => x.Query())
                                           .Returns(new List<Mufredat> { new Mufredat() { /*TODO:propertyler buraya yazılacak MufredatId = 1, MufredatName = "test"*/ } }.AsQueryable());

            _mufredatRepository.Setup(x => x.Add(It.IsAny<Mufredat>())).Returns(new Mufredat());

            var handler = new CreateMufredatCommandHandler(_mufredatRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Mufredat_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateMufredatCommand();
            //command.MufredatName = "test";

            _mufredatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Mufredat, bool>>>()))
                        .ReturnsAsync(new Mufredat() { /*TODO:propertyler buraya yazılacak MufredatId = 1, MufredatName = "deneme"*/ });

            _mufredatRepository.Setup(x => x.Update(It.IsAny<Mufredat>())).Returns(new Mufredat());

            var handler = new UpdateMufredatCommandHandler(_mufredatRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _mufredatRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Mufredat_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMufredatCommand();

            _mufredatRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Mufredat, bool>>>()))
                        .ReturnsAsync(new Mufredat() { /*TODO:propertyler buraya yazılacak MufredatId = 1, MufredatName = "deneme"*/});

            _mufredatRepository.Setup(x => x.Delete(It.IsAny<Mufredat>()));

            var handler = new DeleteMufredatCommandHandler(_mufredatRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _mufredatRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

