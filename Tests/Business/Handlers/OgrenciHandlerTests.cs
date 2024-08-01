
using Business.Handlers.Ogrencis.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Ogrencis.Queries.GetOgrenciQuery;
using Entities.Concrete;
using static Business.Handlers.Ogrencis.Queries.GetOgrencisQuery;
using static Business.Handlers.Ogrencis.Commands.CreateOgrenciCommand;
using Business.Handlers.Ogrencis.Commands;
using Business.Constants;
using static Business.Handlers.Ogrencis.Commands.UpdateOgrenciCommand;
using static Business.Handlers.Ogrencis.Commands.DeleteOgrenciCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OgrenciHandlerTests
    {
        Mock<IOgrenciRepository> _ogrenciRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _ogrenciRepository = new Mock<IOgrenciRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Ogrenci_GetQuery_Success()
        {
            //Arrange
            var query = new GetOgrenciQuery();

            _ogrenciRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ogrenci, bool>>>())).ReturnsAsync(new Ogrenci()
            {																		
            Id = 1,
            Adi = "Eren"
            }
);

            var handler = new GetOgrenciQueryHandler(_ogrenciRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OgrenciId.Should().Be(1);

        }

        [Test]
        public async Task Ogrenci_GetQueries_Success()
        {
            //Arrange
            var query = new GetOgrencisQuery();

            _ogrenciRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Ogrenci, bool>>>()))
                        .ReturnsAsync(new List<Ogrenci> { new Ogrenci() { /*TODO:propertyler buraya yazılacak OgrenciId = 1, OgrenciName = "test"*/ } });

            var handler = new GetOgrencisQueryHandler(_ogrenciRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Ogrenci>)x.Data).Count.Should().BeGreaterThan(0);

        }

        [Test]
        public async Task Ogrenci_CreateCommand_Success()
        {
            Ogrenci rt = null;
            //Arrange
            var command = new CreateOgrenciCommand();
            //propertyler buraya yazılacak
            //command.OgrenciName = "deneme";

            _ogrenciRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ogrenci, bool>>>()))
                        .ReturnsAsync(rt);

            _ogrenciRepository.Setup(x => x.Add(It.IsAny<Ogrenci>())).Returns(new Ogrenci());

            var handler = new CreateOgrenciCommandHandler(_ogrenciRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ogrenciRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Ogrenci_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOgrenciCommand();
            //propertyler buraya yazılacak 
            //command.OgrenciName = "test";

            _ogrenciRepository.Setup(x => x.Query())
                                           .Returns(new List<Ogrenci> { new Ogrenci() { /*TODO:propertyler buraya yazılacak OgrenciId = 1, OgrenciName = "test"*/ } }.AsQueryable());

            _ogrenciRepository.Setup(x => x.Add(It.IsAny<Ogrenci>())).Returns(new Ogrenci());

            var handler = new CreateOgrenciCommandHandler(_ogrenciRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Ogrenci_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOgrenciCommand();
            //command.OgrenciName = "test";

            _ogrenciRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ogrenci, bool>>>()))
                        .ReturnsAsync(new Ogrenci() { /*TODO:propertyler buraya yazılacak OgrenciId = 1, OgrenciName = "deneme"*/ });

            _ogrenciRepository.Setup(x => x.Update(It.IsAny<Ogrenci>())).Returns(new Ogrenci());

            var handler = new UpdateOgrenciCommandHandler(_ogrenciRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ogrenciRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Ogrenci_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOgrenciCommand();

            _ogrenciRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Ogrenci, bool>>>()))
                        .ReturnsAsync(new Ogrenci() { /*TODO:propertyler buraya yazılacak OgrenciId = 1, OgrenciName = "deneme"*/});

            _ogrenciRepository.Setup(x => x.Delete(It.IsAny<Ogrenci>()));

            var handler = new DeleteOgrenciCommandHandler(_ogrenciRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _ogrenciRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

